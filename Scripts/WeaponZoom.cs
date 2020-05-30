using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] Camera FPSCamera;
    [SerializeField] RigidbodyFirstPersonController FPSController;
    [SerializeField] float zoomedOut = 60f;
    [SerializeField] float zoomedIn = 30f;
    [SerializeField] float zoomedOutSens = 2f;
    [SerializeField] float zoomedInSens = 1f;

    bool isZoomedIn = false;
    // Start is called before the first frame update
    private void OnDisable()
    {
        ZoomOut();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if(isZoomedIn == false)
            {
                ZoomIn();
            }
            else
            {
                ZoomOut();
            }
        }

    }

    private void ZoomOut()
    {
        isZoomedIn = false;
        FPSCamera.fieldOfView = zoomedOut;
        FPSController.mouseLook.XSensitivity = zoomedOutSens;
        FPSController.mouseLook.YSensitivity = zoomedOutSens;
    }

    private void ZoomIn()
    {
        isZoomedIn = true;
        FPSCamera.fieldOfView = zoomedIn;
        FPSController.mouseLook.XSensitivity = zoomedInSens;
        FPSController.mouseLook.YSensitivity = zoomedInSens;
    }
}
