using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public string name;
    public int attempts;
    public int guess1;
    public int guess2;
    public int guess3;
    public int guess4;
    public int guess5;
    public int guess6;
    public int fails;

    public PlayerData(string name , int attempts , int guess1 , int guess2 , int guess3 , int guess4 , int guess5 , int guess6 , int fails) {
        this.name = name;
        this.attempts = attempts;
        this.guess1 = guess1;  
        this.guess2 = guess2;   
        this.guess3 = guess3;   
        this.guess4 = guess4;   
        this.guess5 = guess5;
        this.guess6 = guess6;   
        this.fails = fails;
    }

    public PlayerData(string name)
    {
        this.name = name;
        this.attempts = 0;
        this.guess1 = 0;
        this.guess2 = 0;
        this.guess3 = 0;
        this.guess4 = 0;
        this.guess5 = 0;
        this.guess6 = 0;
        fails = 0;
    }

    public PlayerData()
    {
        this.name = "";
        this.attempts = 0;
        this.guess1 = 0;
        this.guess2 = 0;
        this.guess3 = 0;
        this.guess4 = 0;
        this.guess5 = 0;
        this.guess6 = 0;
        fails = 0;
    }

    public override string ToString()
    {
        return $"my name is {name} i have {attempts} attempts, i have {guess1} one guesses , i have {guess2} two guesses , i have {guess3} three guesses , i have {guess4} four guesses , i have {guess5} five guesses, i have {guess6} six guesses and {fails} failures";
    }

    public void readJSONString(string s)
    {
        // trim leading and trailing whitespaces or {}
        char[] charsToTrim = { ' ', '{', '}' };
        string sTrimmed = s.Trim(charsToTrim);
        // split string into its comma separated values
        string[] fields = sTrimmed.Split(',');
       
        // parse the values into our fields
        char[] trimQuotations = { '"'};
        string tmpName = fields[0].Split(':')[1];
        tmpName = tmpName.Trim(trimQuotations);
        this.name = tmpName;
        this.attempts = int.Parse(fields[1].Split(':')[1]);
        this.guess1 = int.Parse(fields[2].Split(':')[1]);
        this.guess2 = int.Parse(fields[3].Split(':')[1]);
        this.guess3 = int.Parse(fields[4].Split(':')[1]);
        this.guess4 = int.Parse(fields[5].Split(':')[1]);
        this.guess5 = int.Parse(fields[6].Split(':')[1]);
        this.guess6 = int.Parse(fields[7].Split(':')[1]);
        this.fails  = int.Parse(fields[8].Split(':')[1]);
    }

    public string getName() { return this.name; }


}
   

