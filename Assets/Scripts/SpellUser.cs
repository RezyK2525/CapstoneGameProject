using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SpellUser : MonoBehaviour
{
    
public static Vector3 xyz = new Vector3(0, 90, 0);
Quaternion newRotation = Quaternion.Euler(xyz);

    
    
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
    
    [Serializable]
    public class GatesSettings
    {
        public float gatesForce, upwardForce;

        public Transform[] WeaponGate = new Transform[9];
        
        public Transform[] GoldenGate = new Transform[9];

        internal Rigidbody[] rb = new Rigidbody[9];

        internal GameObject[] go = new GameObject[9];

        
        
        public float manaCostGates = 75;
        
        public int timeBetweenCastsGates;

        public Vector3[] gateDirections = new Vector3[9];

    }

    public FireBallSettings fireBallSettings = new FireBallSettings();
    public GatesSettings gatesSettings = new GatesSettings();
    

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
            MyInputFireball();
        }
        
        if (GameManager.instance.spellController.spellSettings.isGates)
        {
            GameManager.instance.spellController.spellSettings.isGates = false;
            Debug.Log("IS Gates IS TRUE");
            MyInputGates();
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

    private void MyInputFireball()
    {
        cast = true;
        Debug.Log("Inside MyInputFireball");

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
    
    private void MyInputGates()
    {
        cast = true;
        Debug.Log("Inside MyInputGates");

        if (cast && readyToCast)
        {
            Debug.Log("cast and ready to cast");
            if (GameManager.instance.player.mana >= gatesSettings.manaCostGates)
            {

                Debug.Log("call cast Gates");
                GameManager.instance.player.mana -= gatesSettings.manaCostGates;
                GameManager.instance.player.manaBar.SetValue(GameManager.instance.player.mana);
                CastGates(); 


                ////////GameManager.instance.player.manaBar.SetHealth(GameManager.instance.player.mana);

            }
            else
            {
                Debug.Log("Not Enough Mana To Cast");
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
    
    private void CastGates()
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

        //Vector3 targetLookAt = targetPoint - (90, 0, -90);
        //calculate directions

        for (int i = 0; i < 9; i++)
        {
            gatesSettings.gateDirections[i] = targetPoint - gatesSettings.WeaponGate[i].position;
            gatesSettings.rb[i] = Instantiate(spellPrefab[1], gatesSettings.WeaponGate[i].position, Quaternion.identity).transform.GetComponentInChildren<Rigidbody>();
            gatesSettings.go[i] = Instantiate(spellPrefab[2], gatesSettings.GoldenGate[i].position, Quaternion.identity);
            
            
            gatesSettings.rb[i].GetComponentInChildren<Rigidbody>().transform.rotation = Quaternion.LookRotation(gatesSettings.gateDirections[i]);
            gatesSettings.rb[i].GetComponentInChildren<Rigidbody>().transform.Rotate(90, 0, 0);
            gatesSettings.rb[i].transform.GetComponentInChildren<Rigidbody>().AddForce(gatesSettings.gateDirections[i].normalized * gatesSettings.gatesForce, ForceMode.Impulse);
            gatesSettings.rb[i].transform.GetComponentInChildren<Rigidbody>().AddForce(fpCam.transform.up * gatesSettings.upwardForce, ForceMode.Impulse);

        }

        //Vector3 direction = targetPoint - gatesSettings.attackPointGates.position;
        
        //Rigidbody rb = Instantiate(spellPrefab[1], fireBallSettings.attackPointFireball.position, Quaternion.identity).GetComponent<Rigidbody>();
        //rb.transform.forward = direction.normalized;
        
        //rb.AddForce(direction.normalized * fireBallSettings.fireForce, ForceMode.Impulse);
        //rb.AddForce(fpCam.transform.up * fireBallSettings.upwardForce, ForceMode.Impulse);
        
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

