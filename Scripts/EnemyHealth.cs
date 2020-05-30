using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;
    [SerializeField] Canvas enemyDeadCanvas;
    [SerializeField] CarjackingSimulation CS;
    bool isDead = false;

    public bool IsDead()
    {
        return isDead;
    }
    //create a method that reduces hitPoints by damage caused

        public void TakeDamage(float damage)
    {
        BroadcastMessage("OnDamageTaken");
        hitPoints -= damage;
        if(hitPoints <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead)
        {
            return;
        }

        isDead = true;
        GetComponent<Animator>().SetTrigger("die");
        CS.SetCurrentState(6);
        StartCoroutine(ProcessEnemyDeath());
    }
    IEnumerator ProcessEnemyDeath()
    {
        yield return new WaitForSeconds(2f);
        enemyDeadCanvas.enabled = true;
        Time.timeScale = 0;
        FindObjectOfType<WeaponSwitcher>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        enemyDeadCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
