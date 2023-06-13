using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int force;
    public static int multiplier;
    public static int CPS;

    public static GameObject mainScreen;
    public static GameObject upgradesScreen;

    public static GameObject profileScreen;

    public static Dictionary<int, List<int>> CPS_values = new Dictionary<int, List<int>>();

    void Start()
    {
        mainScreen = GameObject.FindWithTag("MainScreen");
        upgradesScreen = GameObject.FindWithTag("UpgradesScreen");
        profileScreen = GameObject.FindWithTag("ProfileScreen");

        upgradesScreen.SetActive(false);
        profileScreen.SetActive(false);

        multiplier = PlayerPrefs.GetInt("multiplier", 1);
        force = PlayerPrefs.GetInt("force", 0);
        CPS = PlayerPrefs.GetInt("CPS", 0);

        CPS_values.Add(1, new List<int> {1, 100});
        CPS_values.Add(2, new List<int> {10, 800});
    }

    

}
