using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public double forceVal;
    public int forceExp;
    public double multVal;
    public int multExp;
    public double CPS_valueVal;
    public int CPS_valueExp;
    public double wheyVal;
    public int wheyExp;
    public int sessionForce;
    public int lifetimeForce;
    public int totalPrestiges;
    public int lifetimeWheyEarned;
    public int lifetimeWheySpent;

    public GameData()
    {    
        this.forceVal = 0;
        this.forceExp = 0;
        this.multVal = 1;
        this.multExp = 0;
        this.CPS_valueVal = 0;
        this.CPS_valueExp = 0;
        this.wheyVal = 0;
        this.wheyExp = 0;
        this.sessionForce = 0;
        this.lifetimeForce = 0;
        this.totalPrestiges = 0;
        this.lifetimeWheyEarned = 0;
        this.lifetimeWheySpent = 0;
    }
}
