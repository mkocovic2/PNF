using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class mainGun : MonoBehaviourPun
{
    public Transform parentTransform;
    public GameObject prefabToInstantiate1;
    public GameObject prefabToInstantiate2;

    void Start()
    {
        InstantiateObjects();    
    }
    public void InstantiateObjects()
    {
        // Instantiate Prefab1 over the network
        GameObject newObject1 = PhotonNetwork.Instantiate(prefabToInstantiate1.name, parentTransform.position, Quaternion.identity);

        // Set the parent for Prefab1
        if (parentTransform != null)
            newObject1.transform.SetParent(parentTransform);

        // Instantiate Prefab2 over the network
        GameObject newObject2 = PhotonNetwork.Instantiate(prefabToInstantiate2.name, parentTransform.position, Quaternion.identity);

        // Set the parent for Prefab2
        if (parentTransform != null)
            newObject2.transform.SetParent(parentTransform);
    }
}