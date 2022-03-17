using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class Shop : MonoBehaviour
{
    #region Singleton
    public static Shop instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Shop found!");
            return;
        }
        instance = this;
    }

    #endregion

    /*
     *  Add ShopItem Tabs for each Equipment / items
     * 
     * 
     */


    [SerializeField] List<Item> ShopItemsList;
    [SerializeField] Transform ShopScrollView;
    //[SerializeField] Animator NoCoinsAnim;
    [SerializeField] Text CoinsText;
    GameObject ItemTemplate;
    GameObject g;
    Button buyBtn;
    public GameObject mainHUD;
    

    void Start()
    {
        ItemTemplate = ShopScrollView.GetChild(0).gameObject;

        for (int i = 0; i < ShopItemsList.Count; i++)
        {
            g = Instantiate(ItemTemplate, ShopScrollView);
            g.transform.GetChild(0).GetComponent<Image>().sprite = ShopItemsList[i].icon;
            g.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = ShopItemsList[i].value.ToString();
            buyBtn = g.transform.GetChild(2).GetComponent<Button>();

            if (ShopItemsList[i].isPurchased)
            {
                disableBtn();
            }

            buyBtn.interactable = !ShopItemsList[i].isPurchased;
            buyBtn.AddEventListener(i, OnBuyItemClick);
        }


        Destroy(ItemTemplate);
        setCoinsUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseShop();
        }
    }

    public void OnBuyItemClick(int itemIndex)
    {
        Debug.Log(itemIndex);

     
        if (GameManager.instance.player.money >= ShopItemsList[itemIndex].value)
        {
            // Purchase Item
            GameManager.instance.player.money -= ShopItemsList[itemIndex].value;
            //ShopItemsList[itemIndex].isPurchased = true;      // Chech if purchased

            // disable the buy button  -- DONT DELETE THIS ---- //
            buyBtn = ShopScrollView.GetChild(itemIndex).GetChild(2).GetComponent<Button>();
            disableBtn();

            // Destroy the whole card object
            //Destroy(ShopScrollView.GetChild(itemIndex).gameObject);

            // Change UI Text for coins
            setCoinsUI();
            Inventory.instance.Add(ShopItemsList[itemIndex]);
            Debug.Log("Item successfully purchased");
        }
        else
        {
            //NoCoinsAnim.SetTrigger("NoCoins");
            Debug.Log("Not enough coins.");
        }
    }

    void disableBtn()
    {
        buyBtn.interactable = false;
        buyBtn.transform.GetChild(0).GetComponent<Text>().text = "SOLD";
    }

    public void setCoinsUI()
    {
        CoinsText.text = GameManager.instance.player.money.ToString();
    }

    public void OpenShop ()
    {
        gameObject.SetActive(true);
        setCoinsUI();

        // Bring cursor back
        GameManager.instance.player.cursorState();

        // Pause game
        Time.timeScale = 0;

        mainHUD.SetActive(!mainHUD.activeSelf);
        
    }

    public void CloseShop()
    {
        gameObject.SetActive(false);

        // Bring cursor back
        GameManager.instance.player.cursorState();

        // Resume game
        Time.timeScale = 1;

        mainHUD.SetActive(!mainHUD.activeSelf);
    }

}