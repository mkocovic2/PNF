using Photon.Pun;
using UnityEngine;
using System.Collections;

public class Grappling : MonoBehaviourPunCallbacks
{
    public LayerMask whatIsGrappleable;
    public Transform gunTip, camera, player;
    public float maxDistance = 500f;

    public LineRenderer lr { get; private set; }
    public Vector3 grapplePoint { get; private set; }
    public bool IsGrappling => joint != null;

    private SpringJoint joint;
    private PhotonView PV;
    private Coroutine grappleCoroutine;
    private bool isPlayerActivated = true; 
    private bool isGrappling = false; 

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        PV = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (!isPlayerActivated) 
        {
            StopGrapple();
            return;
        }

        if (PV.IsMine)
        {
            if (Input.GetMouseButtonDown(0) && !IsGrappling)
            {
                StartGrapple();
            }
            else if (Input.GetMouseButtonUp(0) && IsGrappling)
            {
                StopGrapple();
            }
        }
    }

    private void LateUpdate()
    {
        DrawRope();
    }

    public void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatIsGrappleable))
        {
            grapplePoint = hit.point;

            PV.RPC("StartGrappleRPC", RpcTarget.All, grapplePoint);
        }
    }

    [PunRPC]
    private void StartGrappleRPC(Vector3 grapplePoint)
    {
        this.grapplePoint = grapplePoint;
        joint = player.gameObject.AddComponent<SpringJoint>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = grapplePoint;

        float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

        joint.maxDistance = distanceFromPoint * 0.8f;
        joint.minDistance = distanceFromPoint * 0f;

        joint.spring = 25f;
        joint.damper = 7f;
        joint.massScale = 45f;

        lr.positionCount = 2;
        currentGrapplePosition = gunTip.position;

        // Start the grapple timer coroutine
        grappleCoroutine = StartCoroutine(GrappleTimer(50f));

        // Set the new grappling state
        isGrappling = true;
    }

    public void StopGrapple()
    {
        PV.RPC("StopGrappleRPC", RpcTarget.All);
    }

    [PunRPC]
    private void StopGrappleRPC()
    {
        lr.positionCount = 0;
        Destroy(joint);

        if (grappleCoroutine != null)
        {
            StopCoroutine(grappleCoroutine);
            grappleCoroutine = null;
        }

        isGrappling = false;
    }

    private Vector3 currentGrapplePosition;

    private void DrawRope()
    {
        if (!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);

        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, currentGrapplePosition);
    }

    private IEnumerator GrappleTimer(float duration)
    {
        yield return new WaitForSeconds(duration);

        if (IsGrappling)
        {
            StopGrapple();
        }
    }

    public void SetPlayerActivation(bool activate)
    {
        isPlayerActivated = activate;

        if (!activate)
        {
            StopGrapple();
        }
        else
        {
            // If the player is being reactivated, reset their physics state
            ResetPlayerPhysics();
        }
    }

    // Method to reset the player's physics state when respawning
    private void ResetPlayerPhysics()
    {
        // Reset the player's position to a spawn point (adjust this as needed)
        player.position = Vector3.zero;

        // Reset the player's velocity
        Rigidbody playerRigidbody = player.GetComponent<Rigidbody>();
        if (playerRigidbody != null)
        {
            playerRigidbody.velocity = Vector3.zero;
        }

        StopGrapple();
    }
    private void OnDisable()
    {
        // Reset the grappling hook state when the player is disabled
        if (IsGrappling)
        {
            StopGrapple();
        }
    }
}
