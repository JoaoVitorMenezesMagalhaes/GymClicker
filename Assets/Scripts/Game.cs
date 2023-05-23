using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Game : MonoBehaviour
{

    public TextMeshProUGUI ui;

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
    }

    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        GameManager.multiplier = 1;
        GameManager.force = 0;
    }

    void Update()
    {
        ui.text = GameManager.force.ToString();
    }
}
