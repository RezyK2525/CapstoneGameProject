using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;

   
    public Vector3 spawnPoint;

    private void Start()
    {
        if (GameManager.instance.isMultiplayer)
        {
            PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint, Quaternion.identity); 

        }
    }


}
