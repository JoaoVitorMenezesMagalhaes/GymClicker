using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class GameManager : MonoBehaviour
{
    public static GameObject mainScreen;
    public static GameObject upgradesScreen;
    public static GameObject shopScreen;
    public static GameObject profileScreen;
    public static GameObject prestigeScreen;
    public static GameObject welcomeBackScreen;

    public static Dictionary<int, List<int>> CPS_values = new Dictionary<int, List<int>>();


    private void Awake()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite) ||
            !Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead)  )
        {
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
            Permission.RequestUserPermission(Permission.ExternalStorageRead);
        }
    }

    void Start() {
      mainScreen = GameObject.FindWithTag("MainScreen");
      upgradesScreen = GameObject.FindWithTag("UpgradesScreen");
      profileScreen = GameObject.FindWithTag("ProfileScreen");
      prestigeScreen = GameObject.FindWithTag("PrestigeScreen");
      shopScreen = GameObject.FindWithTag("ShopScreen");
      welcomeBackScreen = GameObject.FindWithTag("WelcomeBackScreen");

      upgradesScreen.SetActive(false);
      prestigeScreen.SetActive(false);
      profileScreen.SetActive(false);
      shopScreen.SetActive(false);

      CPS_values.Add(1, new List<int> {1, 18000});
      CPS_values.Add(2, new List<int> {8, 130000});
      CPS_values.Add(3, new List<int> {20, 300000});
      CPS_values.Add(4, new List<int> {100, 1400000});
      CPS_values.Add(5, new List<int> {500, 6500000});
      CPS_values.Add(6, new List<int> {2000, 25000000});
      CPS_values.Add(7, new List<int> {10000, 120000000});
      CPS_values.Add(8, new List<int> {100000, 1000000000});
    }
}
