using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedUser : MonoBehaviour
{
    public static SelectedUser instance;
    private PlayerData selectedUser;
    private string path;
    private string persistentPath;
    

    private void Awake()
    {
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "PlayerData";
        persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar;

        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setSelectedUser(PlayerData p)
    {
        SelectedUser.instance.selectedUser = p;
    }

    public PlayerData getSelectedUser()
    {
        return selectedUser;
    }

    public bool isNull()
    {
        if (selectedUser == null) { return true; }
        else { return false; }
    }

    public void saveData()
    {
        string path = persistentPath;
        string json = "";
        json = JsonUtility.ToJson(instance.selectedUser);
        using StreamWriter writer = new StreamWriter(path + instance.selectedUser.name + ".json");
        writer.Write(json);
    }
}
