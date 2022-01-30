using System;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Fireball : MonoBehaviour {
    public bool pushOnAwake = true;
    public Vector3 startDirection;
    public float startMagnitude;
    public ForceMode forceMode;

    public GameObject fieryEffect;
    public GameObject smokeEffect;
    //public GameObject explodeEffect;

    public GameObject explosion;

    public LayerMask whatIsEnemies, whatIsPlayer;

    public int fireballDamage;
    public float explosionRange;
    public float maxLifetime;
    public bool explodeOnTouch = true;

    protected Rigidbody rgbd;

    public void Awake()
    {
        rgbd = GetComponent<Rigidbody>();
    }
/*
    public void Start()
    {
        if (pushOnAwake)
        {
            Push(startDirection, startMagnitude);
        }
    }
*/
    public void Push(Vector3 direction, float magnitude)
    {
        Vector3 dir = direction.normalized;
        rgbd.AddForce(dir * magnitude, forceMode);
    }

    public void OnCollisionEnter(Collision col)
    {
        rgbd.Sleep();
        if (fieryEffect != null)
        {
            StopParticleSystem(fieryEffect);
        }
        if (smokeEffect != null)
        {
            StopParticleSystem(smokeEffect);
        }
        /*
        if (explodeEffect != null)
            explodeEffect.SetActive(false);
*/
        if (col.collider.CompareTag("Player") && explodeOnTouch) ExplodePlayer();
        if (col.collider.CompareTag("Terrain") && explodeOnTouch) Explode();
        if (col.collider.CompareTag("Enemy") && explodeOnTouch) ExplodeEnemy();
    }

    private void Update()
    {
        maxLifetime -= Time.deltaTime;
        if (maxLifetime <= 0) Explode();
    }

    private void ExplodePlayer()
    {

        if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);

        Collider[] players = Physics.OverlapSphere(transform.position, explosionRange, whatIsPlayer);
        for (int i = 0; i < players.Length; i++)
        {
            //get component of enemy and call take damage
            
            //example
            players[i].GetComponent<Fighter>().PlayerReceiveMagicDamage(fireballDamage);


        }
        
        Invoke("Delay", 0.05f);
    }
    
    private void ExplodeEnemy()
    {

        if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);

        Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRange, whatIsEnemies);
        for (int i = 0; i < enemies.Length; i++)
        {
            //get component of enemy and call take damage
            
            //example
            enemies[i].GetComponent<Fighter>().ReceiveMagicDamage(fireballDamage);


        }
        
        Invoke("Delay", 0.05f);
    }
    
    private void Explode()
    {

        if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);
        
        Invoke("Delay", 0.05f);
    }
    
    
    

    private void Delay()
    {
        Destroy(gameObject);
    }


    public void StopParticleSystem(GameObject g)
    {
        ParticleSystem[] par;
        par = g.GetComponentsInChildren<ParticleSystem>();
        foreach(ParticleSystem p in par)
        {
            p.Stop();
        }
    }

    public void OnEnable()
    {
        if (fieryEffect != null)
            fieryEffect.SetActive(true);
        if (smokeEffect != null)
            smokeEffect.SetActive(true);
        /*
        if (explodeEffect != null)
            explodeEffect.SetActive(false);
            */
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}


