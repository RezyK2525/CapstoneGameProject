using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{


    public static GameManager instance;

    private void Awake()
    {

        if (GameManager.instance != null){
            Destroy(gameObject);
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

        //logic
        public int money;
        public int experience;

        //floating text
        public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration){

               floatingTextManager.Show(msg,fontSize,color,position,motion,duration);

        }


        //Upgrade Weapon
        public bool TryUpgradeWeapon()
        {
            // is the weapon max level
            if(weaponPrices.Count <= weapon.weaponLevel)
            {
                return false;
            }
            if(money >= weaponPrices[weapon.weaponLevel])
            {
                money -= weaponPrices[weapon.weaponLevel];
                weapon.UpgradeWeapon();
                return true;
            }
            return false;
        }



        //exp system
        public int GetCurrentLevel()
        {

            int r = 0;
            int add = 0;
            while(experience >= add){
                add += xpTable[r];
                r++;

                if(r == xpTable.Count)//max level
                {
                return r;
                }
            }
            return r;
        }

        public int GetXpToLevel(int level)
        {
            int r = 0;
            int xp = 0;
            while(r < level){

                xp += xpTable[r];
                r++;

            }
            return xp;
        }


        public void GrantXp(int xp)
        {
            int currLevel = GetCurrentLevel();
            experience += xp;
            if (currLevel < GetCurrentLevel())
            {
                OnLevelUp();
            }
        }

        public void OnLevelUp()
        {
            player.OnLevelUp();
        }




        //SAVE STATE
        /*
        prefered character
        money
        experience
        weapon level
        */

        public void SaveState()
        {

        string s = " ";

        s += "0" + "|";
        s += money.ToString() + "|";
        s += experience.ToString() + '|';
        s += weapon.weaponLevel.ToString();

        PlayerPrefs.SetString("SaveState", s);
        Debug.Log("SaveState");

        }


        public void LoadState(Scene s, LoadSceneMode mode)
        {
            if(!PlayerPrefs.HasKey("SaveState")){
            return;
            }


            string[] data = PlayerPrefs.GetString("SaveState").Split('|');

            //Change Character
            money = int.Parse(data[1]);

            //Experience
            experience = int.Parse(data[2]);
            if(GetCurrentLevel() != 1){
                player.SetLevel(GetCurrentLevel());
            }

            //change weaponLevel
            weapon.SetWeaponLevel(int.Parse(data[3]));

            player.transform.position = GameObject.Find("SpawnPoint").transform.position;
            
            Debug.Log("LoadState");

        }




    }

