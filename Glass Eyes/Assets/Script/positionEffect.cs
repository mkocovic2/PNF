using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class positionEffect : MonoBehaviour
{
    public Transform newParticlePosition;
    void Start()
    {
        transform.position = newParticlePosition.position;
    }
}
