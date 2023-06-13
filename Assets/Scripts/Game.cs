using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Game : MonoBehaviour
{

    public TextMeshProUGUI ui;

    private Coroutine cpsCoroutine; 


    public void Increment()
    {
        GameManager.force += GameManager.multiplier;
        PlayerPrefs.SetInt("force", GameManager.force);
    }

    public void Buy(int num)
    {
        if(num == 1 && GameManager.force >= 25)
        {
            GameManager.multiplier += 1;
            GameManager.force -= 25;
            PlayerPrefs.SetInt("force", GameManager.force);
            PlayerPrefs.SetInt("multiplier", GameManager.multiplier);
        }
        
        if(num == 2 && GameManager.force >= 125)
        {
            GameManager.multiplier += 10;
            GameManager.force -= 125; 
            PlayerPrefs.SetInt("force", GameManager.force);
            PlayerPrefs.SetInt("multiplier", GameManager.multiplier);

        }

        if(num == 3 && GameManager.force >= 1500)
        {
            GameManager.multiplier += 100;
            GameManager.force -= 1500;
            PlayerPrefs.SetInt("force", GameManager.force);
            PlayerPrefs.SetInt("multiplier", GameManager.multiplier);
        }

        if(num == 4 && GameManager.force >= 12000)
        {
            GameManager.multiplier += 1000;
            GameManager.force -= 12000;
            PlayerPrefs.SetInt("force", GameManager.force);
            PlayerPrefs.SetInt("multiplier", GameManager.multiplier);
        }

        if(num == 5 && GameManager.force >= 50000)
        {
            GameManager.multiplier += 10000;
            GameManager.force -= 50000;
            PlayerPrefs.SetInt("force", GameManager.force);
            PlayerPrefs.SetInt("multiplier", GameManager.multiplier);
        }

        if(num == 6 && GameManager.force >= 100000)
        {
            GameManager.multiplier += 100000;
            GameManager.force -= 100000;
            PlayerPrefs.SetInt("force", GameManager.force);
            PlayerPrefs.SetInt("multiplier", GameManager.multiplier);
        }
    }

    public void CPS(int num)
    {
        int cost =  GameManager.CPS_values[num][1];
        int value = GameManager.CPS_values[num][0];

        if(GameManager.force >= cost)
        {
            GameManager.force -= cost;
            PlayerPrefs.SetInt("force", GameManager.force);
            
            GameManager.CPS += value;
            PlayerPrefs.SetInt("CPS", GameManager.CPS);
        }
    }

    private IEnumerator UpdateForceCPS()
    {
        while (true)
        {
            GameManager.force += GameManager.CPS;
            PlayerPrefs.SetInt("force", GameManager.force);
            
            yield return new WaitForSeconds(1f);
        }
    }


    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        GameManager.multiplier = 1;
        GameManager.force = 0;
        GameManager.CPS = 0;
    }

    public void openUpgradesMenu()
    {
        GameManager.mainScreen.SetActive(false);
        GameManager.upgradesScreen.SetActive(true);
    }

    public void closeUpgradesMenu()
    {
        GameManager.mainScreen.SetActive(true);
        GameManager.upgradesScreen.SetActive(false);
    }

    void Update()
    {
        ui.text = GameManager.force.ToString();
        if (cpsCoroutine == null && GameManager.CPS > 0)
        {
            cpsCoroutine = StartCoroutine(UpdateForceCPS());
        }
    }
}
