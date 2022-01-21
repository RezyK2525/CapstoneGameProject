using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;
using TMPro;
using UnityEngine.SceneManagement;

public class menuController : MonoBehaviour
{
    [Header("Levels To Load")]
    public string _newGameLevel;
    public Sprite slotSprite;
    public Sprite emptySlotSprite;
    public int numSaveSlots = 3;
    public GameObject newGameBtn;
    public GameObject loadGameBtn;
    public GameObject copyBtn;
    public GameObject deleteBtn;

    private int currentSlot = 0;
    private string levelToLoad;
    // [SerializeField] private GameObject noSavedGameDialog = null;


    public void setSlots()
    {
        for (int i = 1; i <= numSaveSlots; i++)
        {
            if ( SaveSystem.saveExists(i) )
            {
                Debug.Log("Save state found for slot " + i);
                GameObject.Find("Slot " + i + " Image").GetComponent<Image>().sprite = slotSprite;
            }
            else
            {
                Debug.Log("No save state found for slot " + i);
                GameObject.Find("Slot " + i + " Image").GetComponent<Image>().sprite = emptySlotSprite;

            }

        }
    }

    public void NewGame()
    {
        PlayerPrefs.SetString("SaveSlotNumber", ""+currentSlot);
        SceneManager.LoadScene(_newGameLevel);
    }
    public void LoadGame()
    {
        PlayerData data = SaveSystem.loadgameState(currentSlot);
        string s = data.money.ToString() + "|";
        // s += data.weapon.ToString();

        PlayerPrefs.SetString("SaveState", s);
        SceneManager.LoadScene("Start");
        // SceneManager.LoadScene("stage" + data.stageNumber + "-" + data.sceneNumber); TO BE USED WHEN STAGES ARE NAME CORRECTLY
    }

    public void DeleteGame()
    {
        SaveSystem.DeleteSave(currentSlot);
    }
    public void CopyGame(int targetSlotNum)
    {
        if ( !SaveSystem.saveExists(targetSlotNum))
        {
            SaveSystem.SavePlayer(targetSlotNum, SaveSystem.loadgameState(currentSlot));
        }
    }
    public void updateSaveSlotDialog(int slotNumber)
    {
        currentSlot = slotNumber;
        // text
        GameObject.Find("Save Slot Text").GetComponent<TextMeshProUGUI>().text = "Slot " + slotNumber;

        // load vs new game
        if (SaveSystem.saveExists(slotNumber))
        {
            Debug.Log("Save state found for slot " + slotNumber);
            newGameBtn.SetActive(false);
            loadGameBtn.SetActive(true);
            copyBtn.SetActive(true);
            deleteBtn.SetActive(true);
        }
        else
        {
            Debug.Log("No save state found for slot " + slotNumber);
            newGameBtn.SetActive(true);
            loadGameBtn.SetActive(false);
            copyBtn.SetActive(false);
            deleteBtn.SetActive(false);
        }
    }

    public void setCopySaveSlots()
    {

        for (int i = 1; i <= numSaveSlots; i++)
        {
            if (SaveSystem.saveExists(i) || i == currentSlot)
            {
                GameObject.Find("Copy Slot " + i + " Image").GetComponent<Image>().color = new Color(90.0f/255,70.0f/255,45.0f/255,1);
                // remove clickability
                GameObject.Find("Copy Slot " + i).GetComponent<Button>().interactable = false;
            }
            else
            {
                GameObject.Find("Copy Slot " + i).GetComponent<Button>().interactable = true;
                GameObject.Find("Copy Slot " + i + " Image").GetComponent<Image>().color = new Color(180.0f/255,140.0f/255,90.0f/255,1);
                GameObject.Find("Copy Slot " + i + " Image").GetComponent<Image>().sprite = emptySlotSprite;
            }

        }

    }

    public void ExitButton()
    {
        Application.Quit();
    }

}
