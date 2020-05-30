using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitCar : MonoBehaviour
{
    public GameObject carCamera;
    public GameObject FSPlayer;
    public GameObject exitTrigger;
    public GameObject standardCar;
    public GameObject exitPlace;
    [SerializeField] CarjackingSimulation CS;
    bool hasExited = false;
    [SerializeField] Transform target;
    [SerializeField] float turnSpeed = 5f;


    [SerializeField] Transform characterCamera;
    [SerializeField] Transform character;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Enter"))
        {
            if (!hasExited)
            {
                CS.SetCurrentState(2);
                hasExited = true;
            }

            FSPlayer.transform.position = exitPlace.transform.position;
            carCamera.SetActive(false);

            //StartCoroutine(FaceTarget());

            //Vector3 direction = (target.position - FSPlayer.transform.position).normalized;
            //Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            //FSPlayer.transform.rotation = lookRotation;
            FSPlayer.SetActive(true);







            standardCar.GetComponent<UnityStandardAssets.Vehicles.Car.CarController>().enabled = false;
            standardCar.GetComponent<UnityStandardAssets.Vehicles.Car.CarUserControl>().enabled = false;
            exitTrigger.SetActive(false);
        }

        IEnumerator FaceTarget()
        {
            Cursor.lockState = CursorLockMode.Locked;

            //Vector3 direction = (target.position - transform.position).normalized;
            //Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            //transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
            //Debug.Log("Wait 1 sec for turning.....................................................................");

            Quaternion m_CharacterTargetRot = Quaternion.Euler(0f, 90, 0f);
            Quaternion m_CameraTargetRot = Quaternion.Euler(-90, 0f, 0f);

            character.localRotation = m_CharacterTargetRot;
            characterCamera.localRotation = m_CameraTargetRot;

            yield return new WaitForSeconds(5f);
            Cursor.lockState = CursorLockMode.Confined;

        }
    }
}
