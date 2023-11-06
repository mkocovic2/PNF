using Photon.Pun;
using System.Collections;
using UnityEngine;

public class BulletTracerEffect : MonoBehaviourPunCallbacks
{
    public float fireRate = 0.2f;
    public float bulletSpeed = 100f;
    public int maxTracers = 10;
    public LineRenderer tracerLineRenderer;
    public float laserThickness = 0.1f;
    public int laserDamage;
    public GameObject crosshair;
    public GameObject hitMarker;
    public GameObject muzzleFlashEffect;

    private float nextFireTime;
    private LineRenderer[] tracerLines;
    private int currentTracerIndex = 0;
    [SerializeField] PhotonView PV;

    private void Start()
    {
        if (!PV.IsMine)
            return;

        tracerLines = new LineRenderer[maxTracers];
        for (int i = 0; i < maxTracers; i++)
        {
            GameObject tracerObject = new GameObject("Tracer_" + i);
            tracerLines[i] = tracerObject.AddComponent<LineRenderer>();
            tracerLines[i].startWidth = laserThickness;
            tracerLines[i].endWidth = laserThickness;
            tracerLines[i].enabled = false;
        }

        if (tracerLineRenderer != null)
        {
            tracerLineRenderer.startWidth = laserThickness;
            tracerLineRenderer.endWidth = laserThickness;
            tracerLineRenderer.enabled = false;
        }
    }

    private void Update()
    {
        if (!PV.IsMine)
            return;

        if (Input.GetButton("Fire2") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            photonView.RPC("ShootBullet", RpcTarget.All); 
        }
    }

    [PunRPC] 
    private void ShootBullet()
    {
        //ShowMuzzleFlash();
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.red, 2f);
            photonView.RPC("ShowTracerEffect", RpcTarget.All, transform.position, hit.point); 
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("Player Hit");
                PhotonView enemyPV = hit.collider.gameObject.GetComponent<PhotonView>();
                enemyPV.RPC("takeDamageFunction", RpcTarget.All, 5); 

                if (PV.IsMine)
                {
                    ShowHitMarker();
                    DisableCrosshair();
                }
            }
        }
        else
        {
            Vector3 targetPosition = transform.position + transform.forward * 1000f;
            photonView.RPC("ShowTracerEffect", RpcTarget.All, transform.position, targetPosition); 
        }
    }

    [PunRPC] 
    private void ShowTracerEffect(Vector3 startPos, Vector3 endPos)
    {
        LineRenderer currentTracer = GetNextTracerLine();
        currentTracer.positionCount = 2;
        currentTracer.SetPosition(0, startPos);
        currentTracer.SetPosition(1, endPos);
        currentTracer.enabled = true;

        StartCoroutine(DisableTracerAfterDelay(currentTracer));
    }

    [PunRPC] 
    private void DisableTracerRPC()
    {
        foreach (LineRenderer tracer in tracerLines)
        {
            tracer.enabled = false;
        }

        if (tracerLineRenderer != null)
        {
            tracerLineRenderer.enabled = false;
        }
    }

    private IEnumerator DisableTracerAfterDelay(LineRenderer tracer)
    {
        float delay = 0.09f;
        yield return new WaitForSeconds(delay);

        tracer.enabled = false;

        photonView.RPC("DisableTracerRPC", RpcTarget.All);
    }

    private LineRenderer GetNextTracerLine()
    {
        if (tracerLineRenderer != null)
        {
            return tracerLineRenderer;
        }

        LineRenderer currentTracer = tracerLines[currentTracerIndex];
        currentTracerIndex++;
        if (currentTracerIndex >= maxTracers)
        {
            currentTracerIndex = 0;
        }
        return currentTracer;
    }

    private void ShowHitMarker()
    {
        if (hitMarker != null)
        {
            hitMarker.SetActive(true);
            Invoke("HideHitMarker", 0.1f); 
        }
    }

    private void HideHitMarker()
    {
        if (hitMarker != null)
            hitMarker.SetActive(false);
    }

    private void DisableCrosshair()
    {
        if (crosshair != null)
        {
            crosshair.SetActive(false);
            Invoke("EnableCrosshair", 0.1f); 
        }
    }

    private void EnableCrosshair()
    {
        if (crosshair != null)
            crosshair.SetActive(true);
    }

    private void ShowMuzzleFlash()
    {
        if (muzzleFlashEffect != null)
        {
            muzzleFlashEffect.SetActive(true);
            StartCoroutine(DisableMuzzleFlashAfterDelay());
        }
    }

    private IEnumerator DisableMuzzleFlashAfterDelay()
    {
        yield return new WaitForSeconds(0.1f); 

        if (muzzleFlashEffect != null)
        {
            muzzleFlashEffect.SetActive(false);
        }
    }
}
