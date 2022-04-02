
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour
{


    // Instance
    public static GameManager instance;
    public NetworkManager networkManager;
    private bool NetManInstantiated = false;
    public GameObject networkManagerPrefab;

    // references
    public BetterPlayerMovement player;
    //public InventoryUI inventoryUI;
    [FormerlySerializedAs("enemyAiMovement")] public EnemyAI enemyAI;
    public bool isPaused;
    public PotionController potionController;
    public SpellController spellController;
    public HUDController hud;
    public Hotbar hotbar;
    public Camera cam;

    public bool isMultiplayer;

    private void Awake()
    {
        Debug.Log("GameManager waking");


        if (instance != null)
        {

            Destroy(gameObject);
            //Destroy(inventoryUI.gameObject);
            Destroy(player.gameObject);
            //Destroy(floatingTextManager.gameObject);

            return;
        }
        instance = this;

        isMultiplayer = PlayerPrefs.GetInt("isMultiplayer") == 1;
        if (isMultiplayer){
            if (PhotonNetwork.IsMasterClient)
                PhotonNetwork.Instantiate(networkManagerPrefab.name, Vector3.zero, Quaternion.identity);
        }
        else
        {
            Instantiate(networkManagerPrefab);
        }
        networkManager = FindObjectOfType<NetworkManager>();
        Debug.Log(networkManager);

        SceneManager.sceneLoaded += LoadState;
        isPaused = false;

        //networkManager.AddPlayer(player.gameObject);
        // Debug.Log(player);
        NetManInstantiated = true;

        DontDestroyOnLoad(gameObject);

    }
    private void Update()
    {
        if (networkManager == null)
        {
            Debug.Log("resetting networkManager");
            networkManager = FindObjectOfType<NetworkManager>();
        }
    }


    //floating text
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        //floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    // SAVE STATE
    /*
    money
    */

    public void SaveState()
    {
        //string s = money.ToString() + "|";
        // s += inventory.ToString();

        //PlayerPrefs.SetString("SaveState", s);
        Debug.Log("SaveState");

        int slotNumber = int.Parse(PlayerPrefs.GetString("SaveSlotNumber"));
        //PlayerData pd = new PlayerData(s);
        //SaveSystem.SavePlayer(slotNumber, pd);

    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {
         if(!PlayerPrefs.HasKey("SaveState"))
            return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');
        Debug.Log(PlayerPrefs.GetString("SaveState"));
        
        //Change Character
        //money = int.Parse(data[0]);

        //change inventory
        // inventoryUI = (int.Parse(data[1]));

        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
            
        Debug.Log("LoadState");
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
        GameManager.instance.player.cursorState();
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        GameManager.instance.player.cursorState();
    }
    public void ExitGame()
    {
        //Destroy(GameObject.Find("DontDestroyOnLoad"));
        if (isMultiplayer)
        {
            PhotonNetwork.Disconnect();
        }
        SceneManager.LoadScene("MainMenu");

    }
}

