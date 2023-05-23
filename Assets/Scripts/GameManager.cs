using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int force;
    public static int multiplier;

    void Start()
    {
        multiplier = PlayerPrefs.GetInt("multiplier", 1);
        force = PlayerPrefs.GetInt("force", 0);
    }

}
