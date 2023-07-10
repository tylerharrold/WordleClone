using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordReader : MonoBehaviour
{
    public TextAsset textJSON;

    public static string[] words;

    // Start is called before the first frame update

    void Awake()
    {
        words = textJSON.text.Split(',');
        char[] charsToTrim = { '"', ' ' };

        for (int i = 0; i < words.Length; i++)
        {
            words[i] = words[i].Trim(charsToTrim);
        }

    }

    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string[] getWords()
    {
        return words;
    }

    public string getRandomWord()
    {
        string word = "";

        int randInt = Random.Range(0 , words.Length);

        word = words[randInt]; 

        return word;
    }
}
