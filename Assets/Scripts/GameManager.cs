using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            Destroy(inventoryUI.gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            //Destroy(weapon.gameObject);
            return;
        }

        //PlayerPrefs.DeleteAll();;

        instance = this;
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);

    }

    //resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    //references
    public Player player;

    public Weapon weapon;

    public FloatingTextManager floatingTextManager;
    public InventoryUI inventoryUI;


    //logic
    public int money;
    public int experience;

    //floating text
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration){

            floatingTextManager.Show(msg,fontSize,color,position,motion,duration);

    }


    //SAVE STATE
    /*
    money
    inventory
    */

    public void SaveState()
    {

        string s = money.ToString() + "|";
        // s += inventory.ToString();

        PlayerPrefs.SetString("SaveState", s);
        Debug.Log("SaveState");

        int slotNumber = int.Parse(PlayerPrefs.GetString("SaveSlotNumber"));
        PlayerData pd = new PlayerData(s);
        SaveSystem.SavePlayer(slotNumber, pd);
    }

    // changes to this function should be considered in the PlayerData class as well, because that is the class that get saved to a file
    public void LoadState(Scene s, LoadSceneMode mode)
    {
        if(!PlayerPrefs.HasKey("SaveState"))
            return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        Debug.Log(PlayerPrefs.GetString("SaveState"));
        //Change Character
        money = int.Parse(data[0]);

       
        //change inventory
        // inventoryUI = (int.Parse(data[1]));

        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
            
        Debug.Log("LoadState");

    }
}