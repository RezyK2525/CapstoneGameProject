using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SpellUser : MonoBehaviour
{
    
    //ref to cam
    public Camera fpCam;
    //public Transform attackPoint;

    //public GameObject fireBall;
    
    //Spell array
    [SerializeField] private GameObject[] spellPrefab;

    [Serializable]
    public class FireBallSettings
    {
        public float fireForce, upwardForce;
        
        public Transform attackPointFireball;
        
        public float manaCostFireball = 20;
        
        public int timeBetweenCastsFireball;
        
    }

    public FireBallSettings fireBallSettings = new FireBallSettings();
    

    public float timeBetweenManaRegen;

    bool readyToCast, cast;

    private bool notFull = true;

    public bool allowInvoke = true;

    private void Awake()
    {
        readyToCast = true;
    }

    private void Update()
    {

        if (GameManager.instance.spellController.spellSettings.isFireball)
        {
            GameManager.instance.spellController.spellSettings.isFireball = false;
            Debug.Log("IS FIREBALL IS TRUE");
            MyInput();
        }
        

        
        
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

        GameManager.instance.player.manaBar.SetValue(GameManager.instance.player.mana);

        ////////GameManager.instance.player.manaBar.SetHealth(GameManager.instance.player.mana);

        notFull = true;
    }

    private void MyInput()
    {
        cast = true;
        Debug.Log("Inside MyInput");

        if (cast && readyToCast)
        {
            Debug.Log("cast and ready to cast");
            if (GameManager.instance.player.mana >= fireBallSettings.manaCostFireball)
            {

                Debug.Log("call cast fireball");
                GameManager.instance.player.mana -= fireBallSettings.manaCostFireball;
                GameManager.instance.player.manaBar.SetValue(GameManager.instance.player.mana);
                CastFireball(); 


                ////////GameManager.instance.player.manaBar.SetHealth(GameManager.instance.player.mana);

            }
            else
            {
                Debug.Log("Not Enough Mana To Cost");
            }
            
        }


    }

    private void CastFireball()
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
        Vector3 direction = targetPoint - fireBallSettings.attackPointFireball.position;
        
        Rigidbody rb = Instantiate(spellPrefab[0], fireBallSettings.attackPointFireball.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.transform.forward = direction.normalized;
        
        rb.AddForce(direction.normalized * fireBallSettings.fireForce, ForceMode.Impulse);
        rb.AddForce(fpCam.transform.up * fireBallSettings.upwardForce, ForceMode.Impulse);

        //invoke resetCast
        if (allowInvoke)
        {
            Invoke("ResetCast",fireBallSettings.timeBetweenCastsFireball);
            allowInvoke = false;
            
        }

    }

    private void ResetCast()
    {
        
        readyToCast = true;
        allowInvoke = true;
    }
    
    
}

