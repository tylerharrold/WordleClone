using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;



public class ProfileHandler : MonoBehaviour
{

    //private PlayerData playerData;
    public static ProfileHandler instance;

    private PlayerProfiles allPlayers;

    [SerializeField] private int MAX_PROFILE_COUNT = 5;

    private string path = "";
    private string persistentPath = "";
    

    [SerializeField] SelectedUser selectedUser;

    [SerializeField] TMP_Text activeUserDisplay;

    private string playerPrefsKey = "activeUser";

    private void Awake()
    {
        instance = this;
        ProfileHandler.instance.onButtonTriggerClick += setNewActiveProfile;

    }

    // Start is called before the first frame update
    void Start()
    {

        // create a bucket for all our individual player profiles
        allPlayers = new PlayerProfiles();

        // set the save paths
        setPaths();
        

        // get the paths to all the json files in the target directory
        IEnumerable<string> paths = Directory.EnumerateFiles(persistentPath, "*.json");

        // load every json file in paths into a string then into a playerdata
        foreach(string p in paths)
        {
            string sData = loadData(p);
            PlayerData loadedPlayer = new PlayerData();
            loadedPlayer.readJSONString(sData);
            allPlayers.addNewPlayerData(loadedPlayer);
            

        }

        handleActiveUser();
        setActiveUserDisplay();

    }

    private void handleActiveUser()
    {
        // look at playerprefs for active user key
        string activeUser = PlayerPrefs.GetString(playerPrefsKey, "noActiveUserFound");
        

        // if there isn't one, do nothing just return
        if (activeUser == "noActiveUserFound")
        {

            /// FOR TESTING PLEASE DELETE THIS NEXT BIT
            //PlayerPrefs.SetString("activeUser", "Tom Bovis");
            //selectedUser.setSelectedUser(new PlayerData("DontMatter"));

            // this return statement stays
            Debug.Log("no active user found");
            return;

            
        }

        // if there is one, match the name to the one of the loaded profiles
        PlayerData p = null;
        foreach(PlayerData player in allPlayers.getPlayers())
        {
            if (player.getName() == activeUser) { 
                p = player;
                break;
            }
        }

        // if there is no match, do nothing, assume no active user
        if(p == null) { return; }

        // else set the SelectedUser to the PlayerData of the active profile
        //if (!SelectedUser.instance.isNull())
        //SelectedUser.instance.setSelectedUser(p);
        ButtonTriggerClick(p.getName());
    }

    private void setActiveUserDisplay()
    {
        if (!SelectedUser.instance.isNull())
        {
            activeUserDisplay.SetText("User: " + SelectedUser.instance.getSelectedUser().getName());
        }
        else
        {
            Debug.Log("selected user is null");
        }
    }

    private void setPaths()
    {
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "PlayerData" + Path.AltDirectorySeparatorChar;
        persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    public void SaveData()
    {
        string savePath = path;
        string json = JsonUtility.ToJson(playerData);
        Debug.Log(json);

        using StreamWriter writer = new StreamWriter(savePath);
        writer.Write(json);
    }
    */

    public void batchSave()
    {
        string savePath = persistentPath;
        string json = "";
        foreach (PlayerData profiles in allPlayers.getPlayers()) {
            json = JsonUtility.ToJson(profiles);
            using StreamWriter writer = new StreamWriter(savePath + profiles.name + ".json");
            writer.Write(json);
        }
            

    }

    public string loadData(string s)
    {
        using StreamReader reader = new StreamReader(s);
        string json = reader.ReadToEnd();
        return json;
        
    }

    public void createNewProfile(string name)
    {
        PlayerData newPlayer = new PlayerData(name);
        allPlayers.addNewPlayerData(newPlayer);
        Debug.Log("added a newly created player");
    }

    public bool maxPlayersReached()
    {
        if (allPlayers.getLength() >= MAX_PROFILE_COUNT)
            return true;
        else
            return false;
    }

    public PlayerProfiles getProfiles()
    {
        return allPlayers;
    }

    public event Action<string> onButtonTriggerClick;
    public void ButtonTriggerClick(string username)
    {
        if (onButtonTriggerClick != null)
        {
            onButtonTriggerClick(username);
        }
    }


    public void setNewActiveProfile(string newProfileName)
    {
        // search the loaded profiles for matching string name
        foreach(PlayerData player in allPlayers.getPlayers())
        {
            if (newProfileName.Equals(player.name))
            {
                //if(!SelectedUser.instance.isNull())
                {
                    SelectedUser.instance.setSelectedUser(player);
                    PlayerPrefs.SetString(playerPrefsKey, player.name);
                    PlayerPrefs.Save();
                    Debug.Log("saved new playerprefs");
                }
                    
            }
        }

        setActiveUserDisplay();
    }

    public void OnDestroy()
    {
        ProfileHandler.instance.onButtonTriggerClick -= setNewActiveProfile;
    }



}
