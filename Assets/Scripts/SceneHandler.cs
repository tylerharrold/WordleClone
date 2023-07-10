using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// THIS ENTIRE SCRIPT CAN PROBABLY BE DELETED


public class SceneHandler : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 0)
            currentSceneIndex++;
        else
            currentSceneIndex--;

        SceneManager.LoadScene(currentSceneIndex);
    }
}
