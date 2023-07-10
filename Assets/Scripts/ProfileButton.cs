using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProfileButton : MonoBehaviour
{

   

    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void triggerUserSwap()
    {
        ProfileHandler.instance.ButtonTriggerClick(GetComponentInChildren<TMP_Text>().text);
    }



   

}
