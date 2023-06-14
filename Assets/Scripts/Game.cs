using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Game : MonoBehaviour, IDataPersistence
{
    public TextMeshProUGUI ui;
    public TextMeshProUGUI forceValue;
    public TextMeshProUGUI sessionValue;
    public TextMeshProUGUI lifetimeValue;
    public TextMeshProUGUI prestigeValue;
    public TextMeshProUGUI wheyEarningsValue;
    public TextMeshProUGUI wheySpentValue;

    private Coroutine cpsCoroutine; 

    private int force;
    private int multiplier;
    private int CPS_value;

    public void Increment() {
      force += multiplier;

      forceValue.text = force.ToString();
      sessionValue.text = (int.Parse(sessionValue.text) + multiplier).ToString();
      lifetimeValue.text = (int.Parse(lifetimeValue.text) + multiplier).ToString();
    }

    public void Buy(int num)
    {
        if(num == 1 && force >= 25)
        {
            multiplier += 1;
            force -= 25;
        }
        
        if(num == 2 && force >= 125)
        {
            multiplier += 10;
            force -= 125; 
        }

        if(num == 3 && force >= 1500)
        {
            multiplier += 100;
            force -= 1500;
        }

        if(num == 4 && force >= 12000)
        {
            multiplier += 1000;
            force -= 12000;
        }

        forceValue.text = force.ToString();
    }

    public void CPS(int num)
    {
        int cost =  GameManager.CPS_values[num][1];
        int value = GameManager.CPS_values[num][0];

        if(force >= cost)
        {
            force -= cost;
            
            CPS_value += value;
        }
    }

    private IEnumerator UpdateForceCPS() {
      while (true) {
        force += CPS_value;
        forceValue.text = force.ToString();
        sessionValue.text = (int.Parse(sessionValue.text) + CPS_value).ToString();
        lifetimeValue.text = (int.Parse(lifetimeValue.text) + CPS_value).ToString();
        yield return new WaitForSeconds(1f);
      }
    }

    public void Reset() {
      multiplier = 1;
      force = 0;
      CPS_value = 0;

      sessionValue.text = "0";
      forceValue.text = "0";
      prestigeValue.text = (int.Parse(prestigeValue.text) + 1).ToString();
    }

    public void openUpgradesMenu() {
      GameManager.mainScreen.SetActive(false);
      GameManager.upgradesScreen.SetActive(true);
    }

    public void closeUpgradesMenu() {
      GameManager.mainScreen.SetActive(true);
      GameManager.upgradesScreen.SetActive(false);
    }

    public void openProfileMenu() {
      GameManager.mainScreen.SetActive(false);
      GameManager.profileScreen.SetActive(true);
    }

    public void closeProfileMenu() {
      GameManager.mainScreen.SetActive(true);
      GameManager.profileScreen.SetActive(false);
    }

    void Update() {
      ui.text = force.ToString();

      if (cpsCoroutine == null && CPS_value > 0) {
        cpsCoroutine = StartCoroutine(UpdateForceCPS());
      }
    }

    public void LoadData(GameData data)
    {
        this.force = data.force;
        this.multiplier = data.multiplier;
        this.CPS_value = data.CPS_value;
    }

    public void SaveData(ref GameData data)
    {
        data.force = this.force;
        data.multiplier = this.multiplier;
        data.CPS_value = this.CPS_value;
    }

}
