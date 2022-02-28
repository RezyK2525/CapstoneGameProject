using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    //public override void OnConnectedToMaster()
    public void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public void OnJoinedLobby()
    // public override void OnJoinedLobby()
    {
        SceneManager.LoadScene("MainMenu");

    }
}
