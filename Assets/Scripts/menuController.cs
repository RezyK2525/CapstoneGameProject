using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;
using TMPro;
using UnityEngine.SceneManagement;

public class menuController : MonoBehaviour
{
    [Header("Gameplay Settings")]
    [SerializeField] private float defaultBrightness = 1.0f;
    [SerializeField] private bool defaultFullscreen = true;
    [SerializeField] private TMP_Text brightnessTextValue = null;
    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private Toggle fullscreenToggle = null;
    private float tempBrightness = 1.0f;
    public float mainBrightness = 0.2f;

    [Header("Volume Settings")]
    [SerializeField] private float defaultVolume = 1.0f;
    [SerializeField] private TMP_Text masterVolumeTextValue = null;
    [SerializeField] private Slider masterVolumeSlider = null;
    [SerializeField] private TMP_Text musicVolumeTextValue = null;
    [SerializeField] private Slider musicVolumeSlider = null;
    [SerializeField] private TMP_Text effectsVolumeTextValue = null;
    [SerializeField] private Slider effectsVolumeSlider = null; 
    private float[] tempVolume = new float[]{ 1.0f, 1.0f, 1.0f };

    [Header("Gameplay Settings")]
    [SerializeField] private float defaultSensitivity = 1.0f;
    [SerializeField] private TMP_Text sensitivityTextValue = null;
    [SerializeField] private Slider sensitivitySlider = null;
    private float tempSensitivity = 1.0f;
    public float mainSensitivity = 0.2f;

    [Header("Control Scheme")]

    
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
    public void SetBrightness(float brightness)
    {
        tempBrightness = brightness;
        brightnessTextValue.text = brightness.ToString("0.00");
    }
    public void GraphicsApply()
    {
        if (fullscreenToggle.isOn)
        {
            PlayerPrefs.SetInt("masterInvertY", 1);
            // set fullscreen on

        }
        else
        {
            PlayerPrefs.SetInt("masterInvertY", 0);
            // set fullscreen false
        }
        mainBrightness = tempBrightness;
        PlayerPrefs.SetFloat("brightness", tempBrightness);
        // show prompt
        StartCoroutine(ConfirmationBox());
    }
    public void SetGraphicsSettingsToCurrentVal()
    {
        brightnessSlider.value = mainBrightness;
        fullscreenToggle.isOn = false;
    }

    // volume menu
    public void SetMasterVolume(float volume)
    {
        tempVolume[0] = volume;
        masterVolumeTextValue.text = volume.ToString("0.0");
    }
    public void SetMusicVolume(float volume)
    {
        tempVolume[1] = volume;
        musicVolumeTextValue.text = volume.ToString("0.0");
    }
    public void SetEffectsVolume(float volume)
    {
        tempVolume[2] = volume;
        effectsVolumeTextValue.text = volume.ToString("0.0");
    }
    public void volumeApply()
    {

        AudioListener.volume = tempVolume[0];
        PlayerPrefs.SetFloat("masterVolume", tempVolume[0]);
        PlayerPrefs.SetFloat("musicVolume", tempVolume[1]);
        PlayerPrefs.SetFloat("effectsVolume", tempVolume[2]);
        // show prompt
        StartCoroutine(ConfirmationBox());
    }
    public void SetVolumeSliderToCurrentVal()
    {
        masterVolumeSlider.value = AudioListener.volume;
        //musicVolumeSlider.value = AudioListener.volume;
        //effectsVolumeSlider.value = AudioListener.volume;
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

    // control menu
    public void ControlsApply()
    {
        string controlScheme = "wsad il";   // up down left right attack inventory ability 
        mainSensitivity = tempSensitivity;
        PlayerPrefs.SetString("controlScheme", controlScheme);
        // show prompt
        StartCoroutine(ConfirmationBox());
    }
    public void SetControlsToCurrentVal()
    {
        
        //sensitivitySlider.value = mainSensitivity;
    }



    public void ResetButton(string MenuType)
    {
        if (MenuType == "Sound")
        {
            masterVolumeSlider.value = defaultVolume;
            musicVolumeSlider.value = defaultVolume;
            effectsVolumeSlider.value = defaultVolume;
            SetMasterVolume(defaultVolume);
            SetMusicVolume(defaultVolume);
            SetEffectsVolume(defaultVolume);
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
            brightnessTextValue.text = defaultBrightness.ToString("0.00");
            brightnessSlider.value = defaultBrightness;
            mainBrightness = defaultBrightness;
            fullscreenToggle.isOn = defaultFullscreen;
            GraphicsApply();
        }
        else if (MenuType == "Controls")
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
