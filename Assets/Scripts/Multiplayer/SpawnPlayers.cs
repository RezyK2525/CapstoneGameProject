using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityStandardAssets.Characters.FirstPerson;
using System;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;

    public Vector3 spawnPoint;
    private bool spawnSuccessful;

    public void Start()
    //public void SpawnPlayersNowPlz(bool isMultiplayer)
    {
        // if (isMultiplayer)
        if (GameManager.instance.isMultiplayer)
        {
            PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint, Quaternion.identity);
            Debug.Log("spawning players");
            GameManager.instance.player = GameObject.Find("Player(Clone)").GetComponent<BetterPlayerMovement>();
            try
            {
                // GameManager.instance.networkManager.AddPlayer(GameManager.instance.player.gameObject);
                NetworkManager.instance.AddPlayer(GameManager.instance.player.gameObject.GetPhotonView().ViewID);
                GameManager.instance.hud.SetMax();
                spawnSuccessful = true;
            } catch (Exception) {
                spawnSuccessful = false;
            }
        }
        else
        {
            Instantiate(playerPrefab, spawnPoint, Quaternion.identity);
            Debug.Log("spawning player");
        }
    }
    public void Update()
    {
        if (GameManager.instance.isMultiplayer && !spawnSuccessful)
        {
            try
            {
                // GameManager.instance.networkManager.AddPlayer(GameManager.instance.player.gameObject);
                NetworkManager.instance.AddPlayer(GameManager.instance.player.gameObject.GetPhotonView().ViewID);
                GameManager.instance.hud.SetMax(); 
                spawnSuccessful = true;
            }
            catch (Exception e ) {
                Debug.LogError(e);
            }
        }
    }



}
