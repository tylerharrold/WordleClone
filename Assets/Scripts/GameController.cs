using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    private Regex rx = new Regex(@"[a-zA-Z]"); // filter for alphabetical characters input

    [SerializeField] GameObject[] rows;
    [SerializeField] TextField[] letterFields;
    [SerializeField] TMP_Text endgameTextField;
    [SerializeField] TMP_Text replayText;
    [SerializeField] GameObject menuLayover;
    [SerializeField] TMP_Text usernameField;
    [SerializeField] SelectedUser selectedUser;
    private GameObject currentRow;
    
    [SerializeField] private int currentRowCounter;
    [SerializeField] private int currentColCounter;
    [SerializeField] private string hiddenWord;

    private bool enableRestart;
    private bool isDisplayingMenu = false;
    private Color wrong = new Color(27f/255 , 40f/255 , 83f/255);
    private Color almost = new Color(243f/255 , 206f/255 ,  117f/255);
    private Color correct = new Color(4f/255 , 196f/255 , 202f/255);


    void Awake()
    {
        menuLayover.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentRowCounter = 0;
        currentColCounter = 0;

        hiddenWord = getHiddenWord();

        // choose random word


        // set the current working row to the first row
        currentRow = rows[currentRowCounter];
        // grab all the child buttons of the current row
        letterFields = currentRow.GetComponentsInChildren <TextField> ();

        enableRestart = false;



        // display username
        if (!SelectedUser.instance.isNull())
            usernameField.SetText(SelectedUser.instance.getSelectedUser().getName());

        // update stats menu
        if(!SelectedUser.instance.isNull())
            menuLayover.GetComponent<StatsMenu>().updateStatsMenu(SelectedUser.instance.getSelectedUser());

    }

    // Update is called once per frame
    void Update()
    {
        if (isDisplayingMenu)
        {
            // wait until menu is closed
        }
        else
        {
            if (enableRestart)
            {
                if (Input.anyKeyDown == true)
                {
                    // restart scene
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
            else
            {
                if (currentRowCounter > rows.Length - 1)
                {
                    // game is over
                    gameOver();
                }

                if (Input.anyKeyDown)
                {
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        // handle hitting enter here , only works if current counter is equal to length of text input array (full word typed)
                        if (currentColCounter == letterFields.Length)
                        {
                            // assemble string of guess
                            string guess = getGuessString();

                            // check to see if the guess is a permissable word!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

                            // check to see if the guess is the correct guess
                            if (hiddenWord.Equals(guess))
                            {
                                handleWin();
                            }
                            else
                            {
                                evaluateGuess(guess);
                                // set up moving on to the next row
                                switchToNextRow();
                            }

                        }
                    }

                    if (Input.inputString == "\b")
                    {

                        currentColCounter = currentColCounter - 1;
                        if (currentColCounter < 0)
                        {
                            currentColCounter = 0;
                        }
                        letterFields[currentColCounter].ChangeButtonText(" ");

                    }
                    else
                    {
                        if (currentColCounter < letterFields.Length)
                        {
                            // filter for only alphabetical characters
                            Match match = rx.Match(Input.inputString);
                            if (match.Success)
                            {
                                // call to method to update the current letter
                                letterFields[currentColCounter].ChangeButtonText(match.Value.ToUpper());
                                currentColCounter++;
                                //if(currentColCounter > letterFields.Length - 1)
                                //{
                                //    currentColCounter = letterFields.Length - 1;
                                //}
                            }
                        }
                    }
                }
            }
        }
        

        
    }

    private string getGuessString()
    {
        string guess = "";
        foreach (TextField t in letterFields) {
            guess += t.getText();
        }
        return guess;
    }

    private void evaluateGuess(string guess)
    {
        List<int> usedHintIndex = new List<int>();

       // make all the greens
       for(int i = 0; i < guess.Length; i++)
        {
            bool letterColored = false;
            if (guess[i] == hiddenWord[i])
            {
                // go green
                letterFields[i].changeButtonColor(correct);
                letterFields[i].setFill();
                // add index to usedHintIndex
                usedHintIndex.Add(i);
                letterColored = true;
            }
            else
            {
                for (int j = 0; j < hiddenWord.Length; j++)
                {
                    if (!usedHintIndex.Contains(j))
                    {
                        if(guess[i] == hiddenWord[j])
                        {
                            // go yellow
                            letterFields[i].changeButtonColor(almost);
                            letterFields[i].setFill();
                            // add index to usedHintIndex
                            usedHintIndex.Add(j);
                            letterColored = true;
                            break;
                        }
                    }
                }
            }

            if (!letterColored)
            {
                // go grey
                letterFields[i].changeButtonColor(wrong);
                letterFields[i].setFill();
            }

            
        }

    }

    private void switchToNextRow()
    {
        currentRowCounter++;
        currentColCounter = 0;

        // set the current working row to the first row
        if (currentRowCounter < rows.Length)
            currentRow = rows[currentRowCounter];
        // grab all the child buttons of the current row
        letterFields = currentRow.GetComponentsInChildren<TextField>();
    }

    private void handleWin()
    {

        foreach(TextField t in letterFields)
        {
            t.changeButtonColor(correct);
            t.setFill();
        }

        if (!SelectedUser.instance.isNull())
            updateProfileData(currentRowCounter + 1);

        if (!SelectedUser.instance.isNull())
            menuLayover.GetComponent<StatsMenu>().updateStatsMenu(SelectedUser.instance.getSelectedUser());

        displayMenu();

        replayText.gameObject.SetActive(true);
        enableRestart = true;
    }

    private void gameOver()
    {
        updateProfileData(currentColCounter + 1);
        replayText.gameObject.SetActive(true);
        enableRestart = true;
    }

    private string getHiddenWord()
    {
        WordReader wordReader = GetComponent<WordReader>();
        string[] words = wordReader.getWords();

        int randomNum = Random.Range(0, words.Length);
        return words[randomNum].ToUpper();

    }

    public void displayMenu()
    {
        isDisplayingMenu = true;
        menuLayover.SetActive(true);
    }

    public void hideMenu()
    {
        isDisplayingMenu = false;
        menuLayover.SetActive(false);
    }

    public void quit()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 0)
            currentSceneIndex++;
        else
            currentSceneIndex--;

        // write a save for any player data that has been uploaded
        if (!SelectedUser.instance.isNull())
            SelectedUser.instance.saveData();
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void onApplicationQuit()
    {
        if (!SelectedUser.instance.isNull())
            SelectedUser.instance.saveData();
    }

    private void updateProfileData(int endRow)
    {
        PlayerData p = SelectedUser.instance.getSelectedUser();

        p.attempts += 1;

        switch (endRow)
        {
            case 1:
                p.guess1 += 1;
                break;
            case 2:
                p.guess2 += 1;
                break;
            case 3:
                p.guess3 += 1;
                break;
            case 4:
                p.guess4 += 1;
                break;
            case 5:
                p.guess5 += 1;
                break;
            case 6:
                p.guess6 += 1;
                break;
            case 7:
                p.fails += 1;
                break;
            default:
                Debug.Log("cant correctly update profile statistics");
                break;
                // 
        }

    }
}
