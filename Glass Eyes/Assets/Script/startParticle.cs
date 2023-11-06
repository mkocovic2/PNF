using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startParticle : MonoBehaviour
{
    public ParticleSystem laserParticle;
    public static startParticle instance;

    public void Awake()
    {
        instance = this;
    }
    public void activateParticle()
    {
        laserParticle.Play();
    }
    public void stopParticle()
    {
        laserParticle.Stop();
    }

}
