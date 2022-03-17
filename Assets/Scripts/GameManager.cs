
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
    private NetworkManager networkManager;
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


        if (instance != null)
        {

            Destroy(gameObject);
            //Destroy(inventoryUI.gameObject);
            Destroy(player.gameObject);
            //Destroy(floatingTextManager.gameObject);

            return;
        }

        isMultiplayer = PlayerPrefs.GetInt("isMultiplayer") == 1;
        if (isMultiplayer){
            if (PhotonNetwork.IsMasterClient)
                PhotonNetwork.Instantiate(networkManagerPrefab.name, Vector3.zero, Quaternion.identity);
            else
                ;
        }
        else
        {
            Instantiate(networkManagerPrefab);
            
        }
        networkManager = FindObjectOfType<NetworkManager>();

        GameObject.Find("SpawnPlayers").GetComponent<SpawnPlayers>().SpawnPlayersNowPlz(isMultiplayer);
        instance = this;
        player = FindObjectOfType<BetterPlayerMovement>();
        Debug.Log("GameManager waking");
        Debug.Log(player);
        
        SceneManager.sceneLoaded += LoadState;
        isPaused = false;
        DontDestroyOnLoad(gameObject);

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
        SceneManager.LoadScene("MainMenu");

    }
}

