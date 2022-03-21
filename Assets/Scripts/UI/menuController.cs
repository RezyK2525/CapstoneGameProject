using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
//using UnityEngine.InputSystem;

public class menuController : MonoBehaviourPunCallbacks
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
    private Dictionary<string, KeyCode> controlScheme = null;
    private Dictionary<string, KeyCode> tempControlScheme = null;
    private Dictionary<string, KeyCode> defaultControlScheme = new Dictionary<string, KeyCode>() {
        { "forward",     KeyCode.W },
        { "back",   KeyCode.S },
        { "left",   KeyCode.A },
        { "right",  KeyCode.D },
        { "attack",     KeyCode.Space },
        { "inventory",  KeyCode.I },
        { "ability",    KeyCode.L }
    };
    private string controlToChange = null;
    //private InputActionRebindingExtensions.RebindingOperation rebindingOperation;
    private GameObject currentKey;
    private Color keyBg = new Color(0.6784f, 0.6549f, 0.3882f, 1);
    [SerializeField] private Color selectedKey = new Color(0.8f, 0.5f, 0.5f, 1);


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
    [SerializeField] private TMP_Text deleteSlotText = null;

    [Header("Levels To Load")]
    public string _newGameLevel;
    private string levelToLoad;
    // [SerializeField] private GameObject noSavedGameDialog = null;

    [Header("Mulitplayer Panels/Inputs")]
    public GameObject LoadingScreen;
    public GameObject LobbyPanel;
    public TMP_InputField hostInput;
    public TMP_InputField joinInput;

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
        PlayerPrefs.SetInt("isMultiplayer", 0);

        SceneManager.LoadScene(_newGameLevel);
    }
    public void LoadGame()
    {
        PlayerData data = SaveSystem.loadgameState(currentSlot);
        string s = data.money.ToString() + "|";
        // s += data.weapon.ToString();
        PlayerPrefs.SetInt("isMultiplayer", 0);

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
        
        // delete confirmation
        deleteSlotText.text = "Are you sure you want to delete the Save data for Slot " + slotNumber+"?";

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
        // RenderSettings.ambientLight = new Color(brightness, brightness, brightness, 1);
    }
    public void GraphicsApply()
    {
        if (fullscreenToggle.isOn)
        {
            PlayerPrefs.SetInt("fullscreen", 1);
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
        else
        {
            PlayerPrefs.SetInt("fullscreen", 0);
            Screen.fullScreenMode = FullScreenMode.Windowed; 
        }
        mainBrightness = tempBrightness;
        RenderSettings.ambientLight = new Color(mainBrightness, mainBrightness, mainBrightness, 1);
        PlayerPrefs.SetFloat("brightness", tempBrightness);
        // show prompt
        StartCoroutine(ConfirmationBox());
    }
    public void SetGraphicsSettingsToCurrentVal()
    {
        brightnessSlider.value = mainBrightness;
        fullscreenToggle.isOn = Screen.fullScreenMode == FullScreenMode.ExclusiveFullScreen;
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
    public void controlChange(string control)
    {
        controlToChange = control;
       //  changingControl = true;
    }
    void OnGUI()
    {

        
        if (currentKey != null)
        {
            // Debug.Log(currentKey.name);
            Event e = Event.current;
            if (e.isKey)
            {
                tempControlScheme[currentKey.name] = e.keyCode;
                currentKey.GetComponent<TextMeshProUGUI>().text = e.keyCode.ToString();
                currentKey.GetComponent<TextMeshProUGUI>().color = keyBg;
                currentKey = null;
            }
        }
    }
    public void ChangeKey(GameObject clicked)
    {
        if (currentKey != null)
        {
            currentKey.GetComponent<TextMeshProUGUI>().color = keyBg;
            currentKey = null;
        }
        currentKey = clicked;
        currentKey.GetComponent<TextMeshProUGUI>().color = selectedKey;
    }

    public void ControlsApply()
    {
        controlScheme = new Dictionary<string, KeyCode>(tempControlScheme);
        string controlString = "";// front back left right attack ability inventory
        foreach (KeyValuePair<string, KeyCode> control in controlScheme)
        {
            controlString += control.Value.ToString();
        }
        PlayerPrefs.SetString("controlScheme", controlString);

        //rebindingOperation = jumpAction.action.PerformInteractiveRebinding().WithControlsExcluding("Mouse").OnMatchWaitForAnother(0.1f).OnComplete(operation => RebindComplete()).Start();
        // show prompt
        StartCoroutine(ConfirmationBox());
    }
   /* private void RebindComplete()
    {
        int bindingIndex = jumpaction.action.GetBindingIndexForControl(jumptaction.controls[0]);

        rebindingOperation.Dispose();
    }*/
    public void SetControlsToCurrentVal()
    {
        if (controlScheme == null)
        {
            controlScheme = new Dictionary<string, KeyCode>(defaultControlScheme);
        }
        if (tempControlScheme == null)
        {
            tempControlScheme = new Dictionary<string, KeyCode>(controlScheme);
        }
        currentKey = null;
        // Debug.Log(GameObject.Find("forward"));
        foreach (KeyValuePair<string, KeyCode> control in controlScheme)
        {
            GameObject.Find(control.Key).GetComponent<TextMeshProUGUI>().text = control.Value.ToString();
            GameObject.Find(control.Key).GetComponent<TextMeshProUGUI>().color = keyBg;
        }
        
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
            tempControlScheme = new Dictionary<string, KeyCode>(defaultControlScheme);
            foreach (KeyValuePair<string, KeyCode> control in tempControlScheme)
            {
                GameObject.Find(control.Key).GetComponent<TextMeshProUGUI>().text = control.Value.ToString();
                GameObject.Find(control.Key).GetComponent<TextMeshProUGUI>().color = keyBg;
            }
            ControlsApply();
        }

    }
    public IEnumerator ConfirmationBox()
    {
        confirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(1);
        confirmationPrompt.SetActive(false);
    }



    // multiplayer stuff
    public void ConnectToServer()
    {
        /*AppSettings connectionSettings = new AppSettings();
        connectionSettings.UseNameServer = true;
        connectionSettings.ServerAddress = "ns.photonengine.cn";
        connectionSettings.AppIdRealtime = "491273a3-d20f-4d61-83cf-f0089fa65c7d"; // TODO: replace with your own PUN AppId unlocked for China region
        connectionSettings.AppVersion = "ChinaAppVersion"; // optional
*/
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    //public void OnJoinedLobby()
    public override void OnJoinedLobby()
    {
        //SceneManager.LoadScene("MainMenu");
        LoadingScreen.SetActive(false);
        LobbyPanel.SetActive(true);
    }

    public void CreateRoom()
    {
        // Photon.Realtime.RoomOptions roomOptions = new Photon.Realtime.RoomOptions();
        // roomOptions.IsVisible = false;  roomOptions.MaxPlayers = 4;
        // PhotonNetwork.CreateRoom(hostInput.text, roomOptions);
        PhotonNetwork.CreateRoom(hostInput.text);

    }
    public void DiconnectFromServer()
    {

        PhotonNetwork.Disconnect();
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }
    public override void OnJoinedRoom()
    {
        PlayerPrefs.SetInt("isMultiplayer", 1);
        Debug.Log("Joined room");
        PhotonNetwork.LoadLevel(_newGameLevel);
    }
}
