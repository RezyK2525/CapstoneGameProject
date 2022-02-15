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
    //public InventoryUI inventoryUI;
    public EnemyAiMovement enemyAiMovement;
    public bool isPaused;


    //public Weapon weapon;
    //public FloatingTextManager floatingTextManager;
    //public PotionController potionController;
    //public Equipment equipment
    //resources
    //public List<Sprite> playerSprites;
    //public List<Sprite> weaponSprites;
    //logic Moved Into Player??? 
    //public int money;

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

        instance = this;
        player = FindObjectOfType<Player>();
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
        GameManager.instance.player.mouseLook.SetCursorLock(false);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        GameManager.instance.player.mouseLook.SetCursorLock(true);
    }
    public void ExitGame()
    {
         SceneManager.LoadScene("MainMenu");

    }
}

