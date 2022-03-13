using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
using Photon.Pun;

public class NetworkManager : MonoBehaviourPunCallbacks
{
 
    // Instance
    public static NetworkManager instance;

    public GameObject ItemsPrefab;
    public GameObject EnemiesPrefab;

    // references
    public GameObject[] players;

    public GameObject[] enemies;
    public GameObject[] items;

    public bool isMultiplayer;


    private void Start()
    {

        /*if (instance != null)
        {
            Destroy(this.gameObject);
            foreach (Player player in players)
            {
                Destroy(player.gameObject);
            }
            return;
        }*/
        Debug.Log("NetworkManager waking");
        isMultiplayer = PlayerPrefs.GetInt("isMultiplayer") == 1;

        //GameObject.Find("SpawnPlayers").GetComponent<SpawnPlayers>().SpawnPlayersNowPlz(isMultiplayer);
        instance = this;
        //players = FindObjectsOfType<Player>();
        
        // items 
       /* int itemCount = GameObject.Find("Items").transform.childCount;
        items = new GameObject[itemCount];
        for (int i = 0; i < itemCount; i++)
            items[i] = GameObject.Find("Items").transform.GetChild(i).gameObject;

        //enemies
        int enemyCount = GameObject.Find("Enemies").transform.childCount;
        enemies = new GameObject[enemyCount];
        for (int i = 0; i < enemyCount; i++)
            enemies[i] = GameObject.Find("Enemies").transform.GetChild(i).gameObject;*/

        if (PhotonNetwork.IsMasterClient)
            InstantiateEntities();

            DontDestroyOnLoad(this.gameObject);


    }

    public void InstantiateEntities()
    {
        PhotonNetwork.Instantiate(ItemsPrefab.name, ItemsPrefab.transform.position, Quaternion.identity);

    }

    void Update()
    {
        
    }

 /*   void OnPhotonPlayerConnected()
    {
        Debug.Log("OnPhotonPlayerConnected() " + other.name); // not seen if you're the player connecting
    }
*/

}

