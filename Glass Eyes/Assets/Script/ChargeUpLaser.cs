using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ChargeUpLaser : MonoBehaviour
{
    public LineRenderer laserBeam;
    public ParticleSystem chargeUpParticles; 
    public float maxLaserDistance = 100.0f;
    public float maxChargeTime = 2.0f; 
    public float maxChargeScale = 3.0f; 
    public float laserDuration = 1.0f; 

    private bool isCharging = false;
    private bool isFiring = false;
    private float currentChargeTime = 0.0f;
    private float currentLaserDuration = 0.0f;
    private Vector3 initialScale;
    private GameObject chargeParticlesContainer; 

    private void Start()
    {
        initialScale = transform.localScale;
        laserBeam.enabled = false;

        chargeParticlesContainer = new GameObject("ChargeParticlesContainer");
        chargeParticlesContainer.transform.parent = transform; // Make it a child of the main GameObject
        chargeParticlesContainer.transform.localPosition = Vector3.zero;
        chargeParticlesContainer.transform.localRotation = Quaternion.identity;

        ParticleSystem.MainModule mainModule = chargeUpParticles.main;
    }

    private void Update()
    {
        if (isCharging)
        {
            currentChargeTime += Time.deltaTime;
            float chargePercentage = Mathf.Clamp01(currentChargeTime / maxChargeTime);
            float currentScale = chargePercentage * maxChargeScale;
            transform.localScale = initialScale * currentScale;

            if (currentChargeTime >= maxChargeTime)
            {
                isFiring = true;
                FireLaser();
            }
        }

        if (isFiring)
        {
            currentLaserDuration += Time.deltaTime;
            if (currentLaserDuration >= laserDuration)
            {
                StopFiringLaser();
            }
        }

        if (Input.GetMouseButton(1) && !isFiring)
        {
            StartCharging();
        }

        chargeParticlesContainer.transform.position = transform.position;
        chargeParticlesContainer.transform.rotation = Quaternion.identity;
    }

    private void StartCharging()
    {
        isCharging = true;
        currentChargeTime = 0.0f;
        transform.localScale = initialScale; // Reset the scale when starting to charge

        startParticle.instance.activateParticle();

        Debug.Log("Charging up the laser...");
    }

    private void FireLaser()
    {
        laserBeam.enabled = true;
        laserBeam.SetPosition(0, transform.position);
        startParticle.instance.stopParticle();

        RaycastHit hit;
        Vector3 endPosition = transform.position + transform.forward * maxLaserDistance;

        if (Physics.Raycast(transform.position, transform.forward, out hit, maxLaserDistance))
        {
            endPosition = hit.point;
            PhotonView photonView = hit.collider.GetComponent<PhotonView>();
            if (photonView != null && photonView.CompareTag("Player"))
            {
                photonView.RPC("takeDamageFunction", RpcTarget.All, 100);
            }
        }

        laserBeam.SetPosition(1, endPosition);
    }


    private void StopFiringLaser()
    {
        isCharging = false;
        isFiring = false;
        currentLaserDuration = 0.0f;
        laserBeam.enabled = false;
        Debug.Log("Laser duration expired. Charge up to fire again.");
    }
}
