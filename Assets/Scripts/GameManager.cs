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

      CPS_values.Add(1, new List<int> {1, 100});
      CPS_values.Add(2, new List<int> {10, 800});
    }
}
