using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform car;
    [SerializeField] Transform getawayCar;
    [SerializeField] float chaseRange = 10f;
    [SerializeField] float turnSpeed = 5f;
    [SerializeField] CarjackingSimulation CS;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Canvas enemyGotAwayCanvas;
    NavMeshAgent navMeshAgent;
    EnemyHealth health;
   
    bool isProvoked = false;
    bool playerNeedsToChoose = false;

    float distanceToTarget = Mathf.Infinity;
    float distanceToCar = Mathf.Infinity;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        navMeshAgent = GetComponent<NavMeshAgent>();
        health = GetComponent<EnemyHealth>();
        enemyGotAwayCanvas.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        
        if (CS.GetCurrentState() == 4)
        {
            //Move towards car disable car trigger for
            Debug.Log("Inside Enemy AI");
            if (health.IsDead())
            {
                enabled = false;
                navMeshAgent.enabled = false;
            }
            distanceToCar = Vector3.Distance(car.position, transform.position);
            Debug.Log(" Distance to Car:" +distanceToCar);
            if (distanceToCar > navMeshAgent.stoppingDistance)
            {
                // Go to the car
                GetComponent<Animator>().SetBool("attack", false);
                GetComponent<Animator>().SetBool("move", true);
                navMeshAgent.SetDestination(car.position);
                Debug.Log(" < condition ******************** " + distanceToCar + " " + navMeshAgent.stoppingDistance);
                
            }
            else if (distanceToCar <= navMeshAgent.stoppingDistance)
            {
                //TODO: give control to car AI and disable enemy
                Debug.Log(" < condition ********************");
                // enemyPrefab.SetActive(false);
                enemyGotAwayCanvas.enabled = true;
                Time.timeScale = 0;
                FindObjectOfType<WeaponSwitcher>().enabled = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

            }
        }
        else if (CS.GetCurrentState() == 5)
        {   
            if (health.IsDead())
            {
                enabled = false;
                navMeshAgent.enabled = false;
            }
            distanceToCar = Vector3.Distance(getawayCar.position, transform.position);
            if (distanceToCar > navMeshAgent.stoppingDistance)
            {
                // Go to the car
                GetComponent<Animator>().SetBool("attack", false);
                GetComponent<Animator>().SetBool("move", true);
                navMeshAgent.SetDestination(getawayCar.position);
                Debug.Log(" < condition ******************** " + distanceToCar + " " + navMeshAgent.stoppingDistance);

            }
            else if (distanceToCar <= navMeshAgent.stoppingDistance)
            {
                //TODO: give control to car AI and disable enemy
                Debug.Log(" < condition ********************");
                // enemyPrefab.SetActive(false);
                enemyGotAwayCanvas.enabled = true;
                Time.timeScale = 0;
                FindObjectOfType<WeaponSwitcher>().enabled = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

            }
        }
        else
        {
            if (health.IsDead())
            {
                enabled = false;
                navMeshAgent.enabled = false;

            }
            distanceToTarget = Vector3.Distance(target.position, transform.position);
            if (isProvoked && health.IsDead() == false)
            {
                EngageTarget();
            }
            else if (distanceToTarget < chaseRange)
            {
                isProvoked = true;

            }
        }
        
        
    }

    private void EngageTarget()
    {
        Debug.Log("Stoping Distance: " + navMeshAgent.stoppingDistance + " distanceToTarget: " + distanceToTarget);
        FaceTarget();
       if (distanceToTarget > navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        if (distanceToTarget <= navMeshAgent.stoppingDistance && CS.GetPlayerChoice() == -1 )
        {
            WaitForPlayerToChoose();
        }
        if (distanceToTarget <= navMeshAgent.stoppingDistance && CS.GetPlayerChoice() == 2)
        {
            AttackTarget();
        }

        //TODO: change attack condition

        //if (distanceToTarget <= navMeshAgent.stoppingDistance)
        //{
        //    AttackTarget();
        //}
    }

    private void WaitForPlayerToChoose()
    {
        Debug.Log("WaitForPlayerToChoose");
        GetComponent<Animator>().SetBool("move", false);
        GetComponent<Animator>().SetBool("idle",true);
        playerNeedsToChoose = true;

    }

    public bool GetPlayerNeedsToChoose()
    {
        return playerNeedsToChoose;
    }
    public void SetPlayerNeedsToChoose(bool arg)
    {
       playerNeedsToChoose = arg;
    }




    private void AttackTarget()
    {
        FaceTarget();
        GetComponent<Animator>().SetBool("idle", false);
        GetComponent<Animator>().SetBool("attack", true);
    }

    private void ChaseTarget()
    {
        GetComponent<Animator>().SetBool("attack", false);
        GetComponent<Animator>().SetBool("move",true);
        navMeshAgent.SetDestination(target.position);
    }

    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    public void OnDamageTaken()
    {
        isProvoked = true;
    }
}
