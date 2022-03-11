using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;

    public Vector3 spawnPoint;

    private void Start1()
    {
        if (GameManager.instance.isMultiplayer)
        {
            PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint, Quaternion.identity);
            Debug.Log("spawning players");
        }
        else
        {
            Instantiate(playerPrefab, spawnPoint, Quaternion.identity);
            Debug.Log("spawning player");
        }
    }

    public void SpawnPlayersNowPlz(bool isMultiplayer)
    {
        if (isMultiplayer)
        {
            PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint, Quaternion.identity);
            Debug.Log("spawning players");
        }
        else
        {
            Instantiate(playerPrefab, spawnPoint, Quaternion.identity);
            Debug.Log("spawning player");
        }
    }


}
