using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int force;
    public int multiplier;
    public int CPS_value;

    public GameData()
    {    
        this.force = 0;
        this.multiplier = 1;
        this.CPS_value = 0;
    }
}
