using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextField : MonoBehaviour
{
    [SerializeField] TMP_Text tmp; 

    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeButtonText(string s)
    {
        // GetComponentInChildren<TextMeshProUGUI>().SetText(s);
        tmp.SetText(s);
        Debug.Log(tmp.text);
    }

    public void changeButtonColor(Color c) 
    {
        GetComponent<Image>().color = c;
    }

    public string getText()
    {
        return tmp.text;
    }

    public void setFill()
    {
        GetComponent<Image>().fillCenter = true;
    }

}
