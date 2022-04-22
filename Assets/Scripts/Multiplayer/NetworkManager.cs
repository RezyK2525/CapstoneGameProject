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
    internal const int MAX_PLAYERS = 5;

    // references
    public List<GameObject> items;
    public List<GameObject> enemies;
    public List<GameObject> players;

    public bool isMultiplayer;
    public int playerCount = 0;

    private void Start()
    {
        Debug.Log("NetworkManager waking");
        instance = this;
        if (players == null) {
            players = new List<GameObject>();
        }
        isMultiplayer = PlayerPrefs.GetInt("isMultiplayer") == 1;
        

        if (!isMultiplayer || PhotonNetwork.IsMasterClient) {
            InstantiateEntities();
        }
        DontDestroyOnLoad(this.gameObject);
    }
    private void Update()
    {
        BetterPlayerMovement[] existingPlayers = FindObjectsOfType<BetterPlayerMovement>();
        if (existingPlayers.Length < playerCount)
        {
            // remove the left player
            for (int i = 0; i < playerCount; i++)
            {
                bool playerStillConnected = false;
                foreach (BetterPlayerMovement pl in existingPlayers)
                {
                    if (pl.gameObject == players[i])
                    {
                        playerStillConnected = true;
                        break;
                    }
                }
                if (!playerStillConnected)
                {
                    players.RemoveAt(i);
                    playerCount--;
                    break;
                }
            }
        }
    }
    public void InstantiateEntities()
    {
        if (isMultiplayer)
        {
            // items
            foreach (GameObject item in items)
            {
                GameObject itemObj = Resources.Load<GameObject>(item.name);
                PhotonNetwork.Instantiate(item.name, itemObj.transform.position, itemObj.transform.rotation);
            }
            foreach (GameObject enemy in enemies)
            {
                GameObject enemyObj = Resources.Load<GameObject>(enemy.name);
                PhotonNetwork.Instantiate(enemy.name, enemyObj.transform.position, enemyObj.transform.rotation);
            }
        }
        else
        {
            // Instantiate(ItemsPrefab);
            // Instantiate(EnemiesPrefab);
            foreach (GameObject item in items)
                Instantiate(item);
            foreach (GameObject enemy in enemies)
                Instantiate(enemy);
        }
    }
    public void CloseRoom()
    {
        GetComponent<PhotonView>().RPC("ForceQuit", RpcTarget.Others);
    }
    [PunRPC]
    public void ForceQuit()
    {
        GameManager.instance.ExitGame();
    }
    public void SetParent(GameObject g)
    {
        Debug.Log("parenting " + g.GetPhotonView().ViewID);
        int[] parameters = { g.GetPhotonView().ViewID, GameManager.instance.player.gameObject.GetPhotonView().ViewID };
        GetComponent<PhotonView>().RPC("SetParentMaster", RpcTarget.All, parameters);
    }
    [PunRPC]
    public void SetParentMaster(int[] parameters)
    {
        Debug.Log("parent master " + parameters[0]);
        //PhotonNetwork.Destroy(PhotonView.Find(gID).gameObject);
        PhotonView.Find(parameters[0]).gameObject.transform.parent = PhotonView.Find(parameters[1]).gameObject.GetComponent<EquipmentManager>().weaponHolder.transform;
    }

    public void Destroy(GameObject g)
    {
        Debug.Log("destroy " + g.GetPhotonView().ViewID);
        GetComponent<PhotonView>().RPC("DestroyMaster", RpcTarget.MasterClient, g.GetPhotonView().ViewID);
    }
    [PunRPC]
    public void DestroyMaster(int gID)
    {
        Debug.Log("destroy master " + gID);
        PhotonNetwork.Destroy(PhotonView.Find(gID).gameObject);
    }

    /*public GameObject Instantiate(string prefabName, Vector3 pos, Quaternion rot)
    {
        //Debug.Log("instantiate " + g.GetPhotonView().ViewID);
        object[] parameters = { prefabName, pos, rot };
        //return GetComponent<PhotonView>().RPC("InstantiateMaster", RpcTarget.MasterClient, parameters);
    }
    [PunRPC]
    public GameObject InstantiateMaster(object[] parameters)
    {
        Debug.Log("instantiate master " + parameters[0]);
        return PhotonNetwork.Instantiate((string)parameters[0], (Vector3)parameters[1], (Quaternion)parameters[2]);
    }*/

    public void AddPlayer(int pID)
    {
        Debug.Log("add player " + pID);
        GetComponent<PhotonView>().RPC("AddPlayerMaster", RpcTarget.MasterClient, pID);
    }
    [PunRPC]
    public void AddPlayerMaster(int newPlayerID)
    {
        if (instance == null)
        {
            players = new List<GameObject>();
        }
        if (!isMultiplayer || PhotonNetwork.IsMasterClient)
        {
            GameObject newPlayer = PhotonView.Find(newPlayerID).gameObject;
           // Debug.Log("adding player master");
            // Debug.Log(instance);
            // Debug.Log(players);
            // Debug.Log(playerCount);
            players.Add(newPlayer);
            newPlayer.name = "Player" + playerCount;
            playerCount++;
            //Debug.Log("OnPhotonPlayerConnected() " + other.name);
            
            int[] pIDs = new int[players.Count];
            for (int i = 0; i < players.Count; i++)
                pIDs[i] = players[i].GetPhotonView().ViewID;
            // Debug.Log("update pList client please for " + players.Count);
            GetComponent<PhotonView>().RPC("UpdatePlayerListClient", RpcTarget.All, pIDs);
            
        }
    }
    [PunRPC]
    public void UpdatePlayerListClient(int[] pIDs)
    {
        if (instance == null)
        {
            Debug.Log("null instance");
            players = new List<GameObject>();
        }
        if (isMultiplayer && !PhotonNetwork.IsMasterClient)
        {
            Debug.Log("update pList client " + pIDs.Length);
            for (int i = 0; i < pIDs.Length; i++)
            {
                bool playerAdded = false;
                foreach (GameObject p in players)
                {
                    if (p.GetPhotonView().ViewID == pIDs[i]) {
                        playerAdded = true;
                        break;
                    }
                }
                if (!playerAdded)
                {
                    Debug.Log("adding player " + pIDs[i]);
                    GameObject newPlayer = PhotonView.Find(pIDs[i]).gameObject;
                    players.Add(newPlayer);
                    newPlayer.name = "Player" + i;
                    playerCount++;
                }
            }
        }
    }

}

