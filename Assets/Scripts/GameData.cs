using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int force;
    public int multiplier;
    public int CPS_value;
    public int whey;
    public int sessionForce;
    public int lifetimeForce;
    public int totalPrestiges;
    public int lifetimeWheyEarned;
    public int lifetimeWheySpent;

    public GameData()
    {    
        this.force = 0;
        this.multiplier = 1;
        this.CPS_value = 0;
        this.whey = 0;
        this.sessionForce = 0;
        this.lifetimeForce = 0;
        this.totalPrestiges = 0;
        this.lifetimeWheyEarned = 0;
        this.lifetimeWheySpent = 0;
    }
}
