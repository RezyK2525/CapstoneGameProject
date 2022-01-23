using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;
using TMPro;
using UnityEngine.SceneManagement;

public class menuController : MonoBehaviour
{
    [Header("Volume Settings")]
    [SerializeField] private float defaultVolume = 1.0f;
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;
    private float tempVolume = 1.0f;

    [Header("Gameplay Settings")]
    [SerializeField] private float defaultSensitivity = 1.0f;
    [SerializeField] private TMP_Text sensitivityTextValue = null;
    [SerializeField] private Slider sensitivitySlider = null;
    private float tempSensitivity = 1.0f;
    public float mainSensitivity = 0.2f;
    
    [Header("Confirmation")]
    [SerializeField] private GameObject confirmationPrompt = null;


    [Header("Sprites and Buttons")]
    public Sprite slotSprite;
    public Sprite emptySlotSprite;
    public GameObject newGameBtn;
    public GameObject loadGameBtn;
    public GameObject copyBtn;
    public GameObject deleteBtn;

    [Header("Save Slots")]
    public int numSaveSlots = 3;
    private int currentSlot = 0;

    [Header("Levels To Load")]
    public string _newGameLevel;
    private string levelToLoad;
    // [SerializeField] private GameObject noSavedGameDialog = null;


    // game save
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
        SceneManager.LoadScene("Map" + data.stageNumber + "_" + data.stageNumber);
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


    // main menu menu
    public void ExitButton()
    {
        Application.Quit();
    }

    // graphics menu


    // volume menu
    public void SetVolume(float volume)
    {
        tempVolume = volume;
        volumeTextValue.text = volume.ToString("0.0");
    }
    public void volumeApply()
    {
        AudioListener.volume = tempVolume;
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        // show prompt
        StartCoroutine(ConfirmationBox());
    }
    public void SetVolumeSliderToCurrentVal()
    {
        volumeSlider.value = AudioListener.volume;
    }


    // gameplay menu
    public void SetSensitivity(float sensitivity)
    {
        tempSensitivity = sensitivity;
        sensitivityTextValue.text = sensitivity.ToString("0.00");
    }
    public void GameplayApply()
    {
        mainSensitivity = tempSensitivity;
        PlayerPrefs.SetFloat("masterSensitivity", mainSensitivity);
        // show prompt
        StartCoroutine(ConfirmationBox());
    }
    public void SetSensitivitySliderToCurrentVal()
    {
        sensitivitySlider.value = mainSensitivity;
    }



    public void ResetButton(string MenuType)
    {
        if (MenuType == "Sound")
        {
            volumeSlider.value = defaultVolume;
            SetVolume(defaultVolume);
            volumeApply();
        }
        else if (MenuType == "Gameplay")
        {
            sensitivityTextValue.text = defaultSensitivity.ToString("0.00");
            sensitivitySlider.value = defaultSensitivity;
            mainSensitivity = defaultSensitivity;
            GameplayApply();
        }
        else if (MenuType == "Graphics")
        {

        }

    }
    public IEnumerator ConfirmationBox()
    {
        confirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(1);
        confirmationPrompt.SetActive(false);
    }

}
