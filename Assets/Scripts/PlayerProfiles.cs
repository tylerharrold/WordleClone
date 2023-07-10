using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProfiles 
{
    public List<PlayerData> playerProfiles;

    public PlayerProfiles()
    {
        playerProfiles = new List<PlayerData>();
    }

    // method checks to see if this is a new playername, if it is, it adds it, otherwise does not and returns false
    public void addNewPlayerData(PlayerData pd)
    {
        playerProfiles.Add(pd);
        
    }


    public int getLength() { return playerProfiles.Count; }

    public List<PlayerData> getPlayers() { return playerProfiles; }
}
