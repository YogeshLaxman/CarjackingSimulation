using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 30f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    [SerializeField] WeaponSwitcher ws;
    [SerializeField] float timeBetweenShots = 0.5f;
    [SerializeField] CarjackingSimulation CS;
    [SerializeField] EnemyAI EAI;


    bool playerHasChosen = false;
    bool canShoot = true;

    private void OnEnable()
    {
        canShoot = true;
    }
    IEnumerator Shoot()
    {
        Debug.Log("Inside Shoot");
        canShoot = false;
        if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            PlayMuzzleFlash();
            ProcessRaycast();
            ammoSlot.ReduceCurrentAmmo(ammoType);
        }
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    private void PlayMuzzleFlash()
    {   
        if(ws.GetCurrentWeapon() == 1)
        {
            muzzleFlash.Play();
        }
        
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        // if we hit something then
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            Debug.Log("Pointing to: " + hit.transform.name);
            //TODO: add some visual effect for players

            //Hit Impact VFX
            //TODO: if gun then only
            if (ws.GetCurrentWeapon() == 1)
            {
                CreateHitImpact(hit);
                EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
                if (target == null) return;
                target.TakeDamage(damage);
            }
           
        }
        else
        {
            //Do nothing
            return;
        }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
       GameObject hitImpact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(hitImpact, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot == true)
        {
            if(EAI.GetPlayerNeedsToChoose() && !playerHasChosen)
            {
                
                Debug.Log("Set player choice*****************************************************************************");
                CS.SetPlayerChoice(ws.GetCurrentWeapon());
                if(ws.GetCurrentWeapon() == 0)
                {
                    MakeKeyDisappear();
                }
                playerHasChosen = true;
                EAI.SetPlayerNeedsToChoose(false);
            }
            StartCoroutine(Shoot());
        }
    }

    private void MakeKeyDisappear()
    {
        //TODO:
        Debug.Log("Make key disappear");
    }
}
