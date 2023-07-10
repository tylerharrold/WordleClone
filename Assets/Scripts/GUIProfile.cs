using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUIProfile : MonoBehaviour
{
    [SerializeField] List<Button> profileButtons;

    private void Awake()
    {
        // grab all the blank profileButtons
        foreach (Button t in GetComponentsInChildren<Button>())
        {
            
            if (t.tag == "ProfileButton")
            {
                profileButtons.Add(t);
            }
        }

        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void displayProfileNames(PlayerProfiles pp)
    {
        List<PlayerData> listOfPlayers = pp.getPlayers();

        
        for(int i = 0; i < pp.getLength(); i++)
        {
            TMP_Text t = profileButtons[i].GetComponentInChildren<TMP_Text>();
            t.SetText(listOfPlayers[i].getName());
        }
        
    }

    public void cleanup()
    {
        foreach(Button b in profileButtons)
        {
            TMP_Text t = b.GetComponentInChildren<TMP_Text>();
            t.SetText("");
        }
    }
}
