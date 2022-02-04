using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour
{
    // Instance
    public static GameManager instance;

    // references
    public Player player;

    public EnemyAiMovement enemyAiMovement;
    //public Weapon weapon;
    //public FloatingTextManager floatingTextManager;
    //public InventoryUI inventoryUI;
    //public PotionController potionController;
    //public Equipment equipment;

    //resources
    //public List<Sprite> playerSprites;
    //public List<Sprite> weaponSprites;

    //logic Moved Into Player??? 
    //public int money;

    private void Awake()
    {


        if (GameManager.instance != null)
        {

            Destroy(gameObject);
            //Destroy(inventoryUI.gameObject);
            Destroy(player.gameObject);
            //Destroy(floatingTextManager.gameObject);
     
            return;
        }

        instance = this;
        SceneManager.sceneLoaded += LoadState;
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




}

