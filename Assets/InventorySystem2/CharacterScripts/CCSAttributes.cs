using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CCSAttributes
{
    public int stamina;
    public int strength;
    public int agility;
    public int intellect;
    public int spirit;


    public override string ToString()
    {
        string statsString = "";
        if (stamina != 0)
        {
            statsString += "Stamina " + stamina + "\n";
        }
        if (strength != 0)
        {
            statsString += "Strength " + strength + "\n";
        }
        if (agility != 0)
        {
            statsString += "Agility " + agility + "\n";
        }
        if (intellect != 0)
        {
            statsString += "Intellect " + intellect + "\n";
        }
        if (spirit != 0)
        {
            statsString += "Spirit " + spirit + "\n";
        }
        return statsString;
    }
}
