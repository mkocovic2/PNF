using UnityEngine;
using Photon.Pun;

public class GunSynchronizer : MonoBehaviour, IPunObservable
{
    public PhotonView PV;
    private Quaternion syncedRotation;

    public Transform cameraHolder;

    private void Start()
    {
        if (PV == null)
        {
            PV = GetComponent<PhotonView>();
        }

        if (!PV.IsMine)
        {
            enabled = false;
        }
    }

    private void Update()
    {
        if (PV.IsMine)
        {
            Quaternion currentRotation = cameraHolder.rotation;

            PV.RPC("SyncRotation", RpcTarget.Others, currentRotation);
        }
    }

    [PunRPC]
    private void SyncRotation(Quaternion newRotation)
    {
        syncedRotation = newRotation;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(cameraHolder.rotation);
        }
        else
        {
            syncedRotation = (Quaternion)stream.ReceiveNext();
        }
    }

    private void FixedUpdate()
    {
        if (!PV.IsMine)
        {
            transform.rotation = syncedRotation;
        }
    }
}
