using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterCar : MonoBehaviour
{
    public GameObject carCamera;
    public GameObject FSPlayer;
    public GameObject exitTrigger;
    public GameObject standardCar;
    public bool isBoxTriggered;
    bool hasEntered = false;
   // int hasEnteredNum = 0;
    [SerializeField] CarjackingSimulation CS;
    //[SerializeField] Canvas gameOverCanvas;

    private void OnTriggerEnter(Collider other)
    {
        isBoxTriggered = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isBoxTriggered = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        //gameOverCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isBoxTriggered == true)
        {
            if (Input.GetButtonDown("Enter"))
            {


                carCamera.SetActive(true);
                FSPlayer.SetActive(false);
                if (!hasEntered)
                {
                    CS.SetCurrentState(1);
                    hasEntered = true;

                }

                //meObject.GetComponent<Script>().enabled = true;
                standardCar.GetComponent<UnityStandardAssets.Vehicles.Car.CarController>().enabled = true;
                standardCar.GetComponent<UnityStandardAssets.Vehicles.Car.CarUserControl>().enabled = true;
                exitTrigger.SetActive(true);
            }
        }
                
    }
}