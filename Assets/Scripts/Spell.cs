using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    
    //ref to cam
    public Camera fpCam;
    public Transform attackPoint;

    public GameObject fireBall;

    public float fireForce, upwardForce;

    public float timeBetweenCasts;

    public float timeBetweenManaRegen;

    bool readyToCast, cast;

    private bool notFull = true;

    public float manaCost = 20;

    public bool allowInvoke = true;

    private void Awake()
    {
        readyToCast = true;
    }

    private void Update()
    {
        MyInput();
        if (GameManager.instance.player.mana < GameManager.instance.player.maxMana)
        {
            
            notFullMana();
        }
    }

    private void notFullMana()
    {
        if (notFull)
        {
            Invoke("RestoreMana", timeBetweenManaRegen);
            notFull = false;

        }
        
    }

    private void RestoreMana()
    {
        GameManager.instance.player.mana += GameManager.instance.player.manaRegenRate;
        GameManager.instance.player.manaBar.SetHealth(GameManager.instance.player.mana);
        notFull = true;
    }

    private void MyInput()
    {
        cast = Input.GetKeyDown(KeyCode.F);

        if (cast && readyToCast)
        {
            if (GameManager.instance.player.mana >= manaCost)
            {
                GameManager.instance.player.mana -= manaCost;
                GameManager.instance.player.manaBar.SetHealth(GameManager.instance.player.mana);
                Cast(); 
            }
            else
            {
                Debug.Log("Not Enough Mana To Cost");
            }
            
        }


    }

    private void Cast()
    {
        readyToCast = false;

        //Find the exact hit position using a raycast
        Ray ray = fpCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // ray through middle of screen
        RaycastHit hit;

        //check if ray hits something
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit)){
            targetPoint = hit.point;
        }
        else {
            targetPoint = ray.GetPoint(75);
        }
        
        //calculate direction
        Vector3 direction = targetPoint - attackPoint.position;
        
        Rigidbody rb = Instantiate(fireBall, attackPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.transform.forward = direction.normalized;
        
        rb.AddForce(direction.normalized * fireForce, ForceMode.Impulse);
        rb.AddForce(fpCam.transform.up * upwardForce, ForceMode.Impulse);

        //invoke resetCast
        if (allowInvoke)
        {
            Invoke("ResetCast",timeBetweenCasts);
            allowInvoke = false;
        }

    }

    private void ResetCast()
    {
        readyToCast = true;
        allowInvoke = true;
    }
    
    
}
