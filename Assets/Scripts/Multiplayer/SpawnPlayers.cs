using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityStandardAssets.Characters.FirstPerson;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;

    public Vector3 spawnPoint;
 

    public void Start()
    //public void SpawnPlayersNowPlz(bool isMultiplayer)
    {
        // if (isMultiplayer)
        if (GameManager.instance.isMultiplayer)
        {
            PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint, Quaternion.identity);
            Debug.Log("spawning players");
            GameManager.instance.player = GameObject.Find("Player(Clone)").GetComponent<BetterPlayerMovement>();
            GameManager.instance.networkManager.AddPlayer(GameManager.instance.player.gameObject);
            GameManager.instance.hud.SetMax();
        }
        else
        {
            Instantiate(playerPrefab, spawnPoint, Quaternion.identity);
            Debug.Log("spawning player");
    
        }
    }

    

}
