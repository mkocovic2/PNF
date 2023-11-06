using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerGrapple : MonoBehaviour
{
    public Transform grapplePosition;
    void Update()
    {
        transform.position = grapplePosition.position;
    }
}
