using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyAI : Fighter
{
    
    //ALL ENEMY SETTINGS
    public Vector3 walkPoint;
    private bool walkPointSet;
    public float walkPointRange;
    public StatusBar healthBar;
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public float timeBetweenAttacks;
    private bool alreadyAttacked;
    public float sightRange, attackRange;
    internal bool playerInSightRange, playerInAttackRange;
    
    [Serializable]
    public class EnemyType
    {
        public bool isRanged;
        public bool isMeelee;
        public bool isFollower;
    }
    
    [Serializable]
    public class RangedEnemySettings
    {
        
    public float projectileSpeed;
    public float projectileUpwardDirection;
    public Transform attackPoint;
    
    //Attacking
    public GameObject projectile;
    }

    [Serializable]
    public class MeeleeEnemySettings
    {
        public Animator anim;
    }
    
    [Serializable]
    public class FollowerEnemySettings
    {
        public float alertRange;
    }

    public EnemyType enemyType = new EnemyType();
    public RangedEnemySettings rangedEnemySettings = new RangedEnemySettings();
    public MeeleeEnemySettings meeleeEnemySettings = new MeeleeEnemySettings();
    public FollowerEnemySettings followerEnemySettings = new FollowerEnemySettings();



    private void Awake()
    {
        //player = GameObject.Find("PlayerObj").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        Debug.Log("enemy hp at start " + maxHP);
        Debug.Log("enemy HP at start" + hp);
        //enemyHp = enemyMaxHp;
        healthBar.SetMax(maxHP);
        healthBar.healthBarUI.SetActive(false);
        player = GameManager.instance.player.transform;
    }

    private void Update()
    {
        float teamDistance;
        float distance = Vector3.Distance(player.position, transform.position);
        if (enemyType.isFollower)
        {
            teamDistance = Vector3.Distance(GameManager.instance.enemyAI.transform.position, transform.position);
            if (distance <= sightRange || (teamDistance <= followerEnemySettings.alertRange && GameManager.instance.enemyAI.playerInSightRange && enemyType.isFollower))
            {
                playerInSightRange = true;
            }
            else
            {
                playerInSightRange = false;
            }
        }
        if (distance <= sightRange || GameManager.instance.enemyAI.playerInSightRange)
        {
            playerInSightRange = true;
        }
        else
        {
            playerInSightRange = false;
        }

        if (distance <= attackRange)
        {
            playerInAttackRange = true;
        }
        else
        {
            playerInAttackRange = false;
        }
        
        
        
        
        //check for sight and attack range
        //playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        //playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange && enemyType.isRanged) AttackPlayerRanged();
        if (playerInSightRange && playerInAttackRange && enemyType.isMeelee) AttackPlayerMeelee();

    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        
        //Walkpoint Reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }


    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }
    
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    
    private void AttackPlayerRanged()
    {
        
        //Make sure enemy doesnt move
        agent.SetDestination(transform.position);

        Vector3 playerPosition = new Vector3(player.position.x, this.transform.position.y, player.position.z);
        transform.LookAt(playerPosition);

        if (!alreadyAttacked)
        {
            
            //Attack Code HERE
            Rigidbody rb = Instantiate(rangedEnemySettings.projectile, rangedEnemySettings.attackPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            //rb.transform.forward;
            rb.AddForce(transform.forward * rangedEnemySettings.projectileSpeed, ForceMode.Impulse);
            rb.AddForce(transform.up * rangedEnemySettings.projectileUpwardDirection, ForceMode.Impulse);
            
            
            //
            
            
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

    }
    
    private void AttackPlayerMeelee()
    {
        
        //Make sure enemy doesnt move
        agent.SetDestination(transform.position);
        

        Vector3 playerPosition = new Vector3(player.position.x, this.transform.position.y, player.position.z);
        transform.LookAt(playerPosition);

        if (!alreadyAttacked)
        {
            
            //Attack Code HERE   PUT IN MELEE ANIMATION FOR DAMAGE

            
            
            //
            
            
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }




protected override void Death()
    {
        Destroy(gameObject);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);

        if (enemyType.isFollower)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, followerEnemySettings.alertRange);
        }
        
        
    }
    
    

    
    
    
}
