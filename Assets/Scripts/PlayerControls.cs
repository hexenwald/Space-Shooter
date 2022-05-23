using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("General Setup Settings")]
    [Tooltip("How fast the ship moves up and down")] [SerializeField] float horizontalSpeed = 20f;
    [SerializeField] float verticalSpeed = 20f;
    [SerializeField] float xRange = 18f;
    [SerializeField] float yRange = 8f;

    [Header("Laser gun array")]
    [Tooltip("Add all player lasers here")] [SerializeField] GameObject[] lasers;

    [Header(" Screen position based on tuning")]
    [SerializeField] float positionPitchFactor = -1.03f;
    [SerializeField] float positionYawFactor = 1.03f;

    [Header("Player input based tuning")]
    [SerializeField] float controlPitchFactor = -15f;
    
    [SerializeField] float controlRollFactor = 15f;

    float xThrow, yThrow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessRotation();
        ProcessTranslation();
        ProccesFiring();
    }

    void ProcessRotation()
    {   
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControl = yThrow * controlPitchFactor;
        float yawDueToPosition = transform.localPosition.x * positionYawFactor;

        float pitch = pitchDueToPosition + pitchDueToControl;
        float yaw = yawDueToPosition;
        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * horizontalSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXpos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yThrow * Time.deltaTime * verticalSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXpos, clampedYPos, transform.localPosition.z);
    }

    void ProccesFiring()
    {
        if (Input.GetButton("Fire1"))
        {
            SetLasersActive(true);
        }
        else
        {
            SetLasersActive(false);
        }
    }

    void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }
}