using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject golem;
    public int maxEnemies;
    public float spawnDelay; 
    private int enemyCount;
    private int xPos;
    private int zPos;
    
    

    void Start()
    {
        StartCoroutine(EnemyDrop());
    }

    IEnumerator EnemyDrop()
    {
        while(enemyCount < maxEnemies)
        {
            xPos = Random.Range(220, 290);
            zPos = Random.Range(-50, 112);

            // Isntantiate mini golems
            Instantiate(golem, new Vector3(xPos, -4, zPos), Quaternion.identity);
            yield return new WaitForSeconds(spawnDelay);
            enemyCount += 1;
        }
    }
}
