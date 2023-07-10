using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsMenu : MonoBehaviour
{
    [SerializeField] TMP_Text attempts;
    [SerializeField] TMP_Text oneGuess;
    [SerializeField] TMP_Text twoGuess;
    [SerializeField] TMP_Text threeGuess;
    [SerializeField] TMP_Text fourGuess;
    [SerializeField] TMP_Text fiveGuess;
    [SerializeField] TMP_Text sixGuess;
    [SerializeField] TMP_Text fails;


    [SerializeField] 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateStatsMenu(PlayerData p)
    {
        Debug.Log(p.ToString());

        int attempts = p.attempts;
        int oneGuess = p.guess1;
        int twoGuess = p.guess2;
        int threeGuess = p.guess3;
        int fourGuess = p.guess4;
        int fiveGuess = p.guess5;
        int sixGuess = p.guess6;
        int fails = p.fails;

        this.attempts.SetText("Attempts: " + attempts.ToString());
        this.oneGuess.SetText("One Guess: "  +oneGuess.ToString());
        this.twoGuess.SetText("Two Guess: " + twoGuess.ToString());
        this.threeGuess.SetText("Three Guess: " + threeGuess.ToString());
        this.fourGuess.SetText("Four Guess: " + fourGuess.ToString());
        this.fiveGuess.SetText("Five Guess: " + fiveGuess.ToString());
        this.sixGuess.SetText("Six Guess: " + sixGuess.ToString());
        this.fails.SetText("Fails: " + fails.ToString());
    }
}
