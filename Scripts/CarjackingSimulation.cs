using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static System.Net.Mime.MediaTypeNames;
using UnityEngine.UI;

public class CarjackingSimulation : MonoBehaviour
{
    [SerializeField] Text bottomTextComponent;
    [SerializeField] Text topTextComponent;
    [SerializeField] Text leftTextComponent;
    [SerializeField] Text rightTextComponent;
    [SerializeField] int currentState;
    [SerializeField] int playerChoice;
    //[SerializeField] AudioClip case0;
    [SerializeField] [Range (0,1)]float soundVolume = 0.7f;
    [SerializeField] AudioClip[] narratorSounds = new AudioClip[7];
    bool[] narratorPlayedClipMap = new bool[7];
    //[SerializeField] Transform key;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 7; i++)
        {
            narratorPlayedClipMap[i] = false;
        }
        playerChoice = -1;
        currentState = 0;
        bottomTextComponent.text = ("Press E to enter your car");
        topTextComponent.text = ("Welcome to Carjacking Simulation!");
        leftTextComponent.text =  ("Use W,A,S,D to move" +
            "\nMouse to look around");
        rightTextComponent.text = (" ");

       
        PlayAudioNumber(0);
    }


    public int GetCurrentState()
    {
        return currentState;
    }
    public void SetCurrentState(int stateValue)
    {
        currentState = stateValue;
    }
    public int GetPlayerChoice()
    {
        return playerChoice;
    }
    public void SetPlayerChoice(int stateValue)
    {
        playerChoice = stateValue;
    }
    // Update is called once per frame
    void Update()
    {
        ManageStates();
        CheckForPlayerChoice();
    }

    private void CheckForPlayerChoice()
    {
        if(playerChoice != -1)
        {
            // set state of the game
            if(playerChoice == 2)
            {
                currentState = 3;
            }
            if (playerChoice == 0)
            {
                currentState = 4;
            }
            if (playerChoice == 1)
            {
                currentState = 5;
            }
        }
    }

    private void ManageStates()
    {
        PlayAudioNumber(currentState);
        switch (currentState)
        {
            case 0:
                topTextComponent.text = "Welcome to Carjacking Simulation!";
                bottomTextComponent.text = "Press E to enter your car";
                leftTextComponent.text = ("Use W,A,S,D to move" +
           "\nMouse to look around");
                rightTextComponent.text = (" ");
                break;
            case 1:
                topTextComponent.text = " Park the car in the parking area straight ahead";
                bottomTextComponent.text = " Press E to exit the car after parking";
                leftTextComponent.text = (" ");
                rightTextComponent.text = (" ");
                break;
            case 2:
                topTextComponent.text = "You are being threatened by a Car Jacker";
                leftTextComponent.text = "Key : Give keys to Carjacker \n Gun: Use your gun \n Phone : Call 911 for help ";
                rightTextComponent.text = "Use mouse scroll wheel OR \n Press 1 or 2 or 3  on keyboard to select items from your inventory";
                bottomTextComponent.text = "Choose key or phone or gun and press LEFT MOUSE BUTTON ";
                break;
            case 3:
                topTextComponent.text = "You chose to call 911 and call for help";
                leftTextComponent.text = " ";
                //TODO: unhandled reaching to the car should stop simulation with you escaped message!
                rightTextComponent.text = "Attack using gun or run away in your car or by foot";
                bottomTextComponent.text = " The carjacker thinks that you are a threat and is attacking you!";
                break;
            case 4:
                //TO DO: disable the key object disable car trigger for player, CJ will run to the car and run away
               // key.position.y = -100f;
                topTextComponent.text = "You chose to give your keys to the Carjacker";
                leftTextComponent.text = " ";
                rightTextComponent.text = " ";
                bottomTextComponent.text = " The carjacker is going to take your car! Call 911 on your phone after he is inside the car";
                break;
            case 5:
                //TO DO: disable the key object disable car trigger for player, CJ will run to the car and run away
                topTextComponent.text = "You scared the Carjacker away with your gun";
                leftTextComponent.text = " ";
                rightTextComponent.text = " ";
                bottomTextComponent.text = "The carjacker is trying to get way! Call 911 on your phone ";
                break;
            case 6:
                topTextComponent.text = "You shot the Carjacker!";
                leftTextComponent.text = " ";
                rightTextComponent.text = " ";
                bottomTextComponent.text = "Violence is not always necessary! Call 911 ";
                break;
            default:
                topTextComponent.text = "Unhandled State";
                bottomTextComponent.text = "Unhandled State";
                break;

        }
    }

    private void PlayAudioNumber(int num)
    {
        AudioClip clip = narratorSounds[num];
        if (clip == null) return;
        if (!narratorPlayedClipMap[num])
        {
            narratorPlayedClipMap[num] = true;
            GetComponent<AudioSource>().PlayOneShot(clip, 1);
        }
    }
}
