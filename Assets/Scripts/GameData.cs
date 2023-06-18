using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public double forceVal;
    public int forceExp;
    public long multiplier;
    public long CPS_value;
    public long whey;
    public long sessionForce;
    public long lifetimeForce;
    public long totalPrestiges;
    public long lifetimeWheyEarned;
    public long lifetimeWheySpent;
    public string lastDate;

    public GameData()
    {    
        this.forceVal = 0;
        this.forceExp = 0;
        this.multiplier = 1;
        this.CPS_value = 0;
        this.whey = 0;
        this.sessionForce = 0;
        this.lifetimeForce = 0;
        this.totalPrestiges = 0;
        this.lifetimeWheyEarned = 0;
        this.lifetimeWheySpent = 0;
        this.lastDate = "";
    }
}
