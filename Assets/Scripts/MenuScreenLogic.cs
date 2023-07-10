using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuScreenLogic : MonoBehaviour
{
    [SerializeField] GameObject selectProfilePopupMenu;
    [SerializeField] GameObject inputProfilePopupMenu;

    [SerializeField] Button selectProfileButton;
    [SerializeField] Button createNewProfileButton;
    [SerializeField] Button closeSelectProfileButton;
    [SerializeField] Button enterInput;
    [SerializeField] Button quitButton;

    [SerializeField] TMP_InputField inputField;

    [SerializeField] ProfileHandler profileHandler;

    [SerializeField] GUIProfile guiProfile;

    private bool IS_IN_INPUT_STATE = false;

    void Awake()
    {
        selectProfilePopupMenu.SetActive(false);
        inputProfilePopupMenu.SetActive(false);

        ProfileHandler.instance.onButtonTriggerClick += closeProfilesMenu;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IS_IN_INPUT_STATE)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                closeNewProfileMenu();
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                submitInput();
            }
        }
    }

    public void showSelectProfileMenu()
    {
        selectProfilePopupMenu.SetActive(true);
        guiProfile.displayProfileNames(profileHandler.getProfiles());
    }

    public void closeSelectProfileMenu()
    {
        guiProfile.cleanup();
        selectProfilePopupMenu.SetActive(false);
    }

    public void createNewProfileMenu()
    {
        // hide the select profile menu
        selectProfilePopupMenu.SetActive(false);

        // show the input popup
        inputProfilePopupMenu.SetActive(true);

        IS_IN_INPUT_STATE = true;

        inputField.Select();

    }

    public void closeNewProfileMenu()
    {
        // clear input field
        inputField.text = "";

        IS_IN_INPUT_STATE = false;
        // hide input profile menu
        inputProfilePopupMenu.SetActive(false);

        // show select profile menu
        selectProfilePopupMenu.SetActive(true);
    }

    public void submitInput()
    {
        if (profileHandler.maxPlayersReached())
        {
            Debug.Log("sorry max players reached");
        }
        else if(inputField.text == "")
        {
            // do nothing
        }
        else
        {
            profileHandler.createNewProfile(inputField.text);
            profileHandler.batchSave();
            // set active user to the one just input
            ProfileHandler.instance.ButtonTriggerClick(inputField.text);
        }

        



        // close both input and profiles screen
        closeNewProfileMenu();
        selectProfilePopupMenu.SetActive(false);


    }

    public void loadNextScene()
    {
        // batch save the profiles left in memory
        profileHandler.batchSave();

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 0)
            currentSceneIndex++;
        else
            currentSceneIndex--;

        SceneManager.LoadScene(currentSceneIndex);
    }

    public void closeProfilesMenu(string s)
    {
        closeSelectProfileMenu();
    }

    public void quitApplication()
    {
        profileHandler.batchSave();
        Application.Quit();
    }

    public void OnApplicationQuit()
    {
        profileHandler.batchSave();
    }
}
