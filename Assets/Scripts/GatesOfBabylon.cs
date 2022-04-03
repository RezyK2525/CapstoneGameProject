using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;



//Explosion Range code needs work
[RequireComponent(typeof(Rigidbody))]
public class GatesOfBabylon : MonoBehaviour
{
    public bool pushOnAwake = true;
    public Vector3 startDirection;
    public float startMagnitude;
    public ForceMode forceMode;
    public Transform player;
    public Vector3 result = Vector3.forward * 5;
    public GameObject fieryEffect;
    public GameObject smokeEffect;
    //public GameObject explodeEffect;

    public GameObject explosion;

    public LayerMask whatIsEnemies, whatIsPlayer;

    //private float spellPowerModifier = Mathf.Ceil(GameManager.instance.player.spellPower * 0.2f);
    public float gateDamage;
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
        rgbd.Sleep();
        /*
        if (fieryEffect != null)
        {
            StopParticleSystem(fieryEffect);
        }
        if (smokeEffect != null)
        {
            StopParticleSystem(smokeEffect);
        }
        */
        /*
        if (explodeEffect != null)
            explodeEffect.SetActive(false);
*/
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

        if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);

        Collider[] players = Physics.OverlapSphere(transform.position, explosionRange, whatIsPlayer);
        Debug.Log("ExplodeEnemy");
        for (int i = 0; i < players.Length; i++)
        {
            //get component of enemy and call take damage
            
            //example
            Debug.Log("Enemy hit" + i);
            players[i].GetComponent<Fighter>().PlayerReceiveMagicDamage(gateDamage);


        }
        
        Invoke("Delay", 0.05f);
    }
    
    private void ExplodeEnemy()
    {

        if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);

        Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRange, whatIsEnemies);
        for (int p = 0; p < enemies.Length; p++)
        {
            //get component of enemy and call take damage

            

            //example
            enemies[p].GetComponent<Fighter>().ReceiveMagicDamage(gateDamage);


        }
        
        Invoke("Delay", 0.05f);
    }
    
    private void Explode()
    {

        if (explosion != null)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
        }
        
        Invoke("Delay", 0.05f);
    }
    
    
    

    private void Delay()
    {
        if (GameManager.instance.isMultiplayer)
        {
            PhotonNetwork.Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

/*
    public void StopParticleSystem(GameObject g)
    {
        ParticleSystem[] par;
        par = g.GetComponentsInChildren<ParticleSystem>();
        foreach(ParticleSystem p in par)
        {
            p.Stop();
        }
    }
    */

/*
    public void OnEnable()
    {
        if (fieryEffect != null)
            fieryEffect.SetActive(true);
        if (smokeEffect != null)
            smokeEffect.SetActive(true);

    }
*/

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
        
        Gizmos.color = Color.black;
        Gizmos.DrawLine(this.transform.position, this.transform.position + result);
    }
}
