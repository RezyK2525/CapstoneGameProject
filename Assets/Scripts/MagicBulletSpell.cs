using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBulletSpell : MonoBehaviour
{

    public ForceMode forceMode;

    //public GameObject fieryEffect;
    //public GameObject smokeEffect;
    //public GameObject explodeEffect;

    //public GameObject explosion;

    public LayerMask whatIsEnemies, whatIsPlayer;

    //private float spellPowerModifier = Mathf.Ceil(GameManager.instance.player.spellPower * 0.2f);
    public float MagicBulletDamage;
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
        
        // Helps the enemy shoot at the player more accurately 
        //transform.LookAt(player);
        // Might have to change for the player
        
        Vector3 dir = direction.normalized;
        rgbd.AddForce(dir * magnitude, forceMode);
    }

    public void OnCollisionEnter(Collision col)
    {
        if (col.collider.CompareTag("Player") && explodeOnTouch) ExplodePlayer();
        if (col.collider.CompareTag("Terrain") && explodeOnTouch) Explode();
        if (col.collider.CompareTag("Enemy") && explodeOnTouch)
        {
            GameManager.instance.enemyAI = col.collider.GetComponent<EnemyAI>();
            ExplodeEnemy();
        }
    }

    private void Update()
    {
        maxLifetime -= Time.deltaTime;
        if (maxLifetime <= 0) Explode();
    }

    private void ExplodePlayer()
    {

        //Debug.Log("EXPLODE Player");
        //if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);

        Collider[] players = Physics.OverlapSphere(transform.position, explosionRange, whatIsPlayer);
        for (int i = 0; i < players.Length; i++)
        {
            //get component of enemy and call take damage
            
            //example
            players[i].GetComponent<Fighter>().PlayerReceiveMagicDamage(MagicBulletDamage);


        }
        
        Invoke("Delay", 0.05f);
    }
    
    private void ExplodeEnemy()
    {
        //Debug.Log("EXPLODE ENEMY");

        //if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);
        
        Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRange, whatIsEnemies);
        for (int p = 0; p < enemies.Length; p++)
        {
            //get component of enemy and call take damage
            //example
            enemies[p].GetComponent<Fighter>().ReceiveMagicDamage(MagicBulletDamage);


        }
        
        Invoke("Delay", 0.05f);
    }
    
    private void Explode()
    {
        //Debug.Log("EXPLODE");
        //if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);
        
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

    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
