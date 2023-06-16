using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Game : MonoBehaviour, IDataPersistence
{
    public TextMeshProUGUI forceTextMain;
    public TextMeshProUGUI wheyTextMain;
    public TextMeshProUGUI forceTextUpgrade;
    public TextMeshProUGUI wheyTextUpgrade;
    public TextMeshProUGUI forceTextShop;
    public TextMeshProUGUI wheyTextShop;
    public TextMeshProUGUI wheyToClaimText;
    public TextMeshProUGUI forceValue;
    public TextMeshProUGUI sessionValue;
    public TextMeshProUGUI lifetimeValue;
    public TextMeshProUGUI prestigeValue;
    public TextMeshProUGUI wheyEarningsValue;
    public TextMeshProUGUI wheySpentValue;
    public List<GameObject> Clickers;  

    private Coroutine cpsCoroutine; 

    private int force;
    private int multiplier;
    private int CPS_value;
    private int whey;
    private int randAnimation = 0;

    private int wheyToClaim;

    public void Increment() {
      force += (multiplier + (multiplier * ((whey*2)/100)));

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
        ChangeAnimation();
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
        force += (CPS_value + (CPS_value * ((whey*2)/100)));
        forceValue.text = force.ToString();
        sessionValue.text = (int.Parse(sessionValue.text) + CPS_value).ToString();
        lifetimeValue.text = (int.Parse(lifetimeValue.text) + CPS_value).ToString();
        yield return new WaitForSeconds(1f);
      }
    }

    public void Prestige() {
      if (wheyToClaim > 0) {
        multiplier = 1;
        force = 0;
        CPS_value = 0;
        whey += wheyToClaim;

        sessionValue.text = "0";
        forceValue.text = "0";
        prestigeValue.text = (int.Parse(prestigeValue.text) + 1).ToString();
        wheyEarningsValue.text = (int.Parse(prestigeValue.text) + wheyToClaim).ToString();

        wheyToClaim = 0;
      }
    }

    public void HardReset() {
      multiplier = 1;
      force = 0;
      CPS_value = 0;
      whey += wheyToClaim;

      sessionValue.text = "0";
      forceValue.text = "0";
      prestigeValue.text = "0";
      wheyEarningsValue.text = "0";

      wheyToClaim = 0;
    }

    public void openUpgradesMenu() {
      GameManager.mainScreen.SetActive(false);
      GameManager.upgradesScreen.SetActive(true);
    }

    public void closeUpgradesMenu() {
      GameManager.mainScreen.SetActive(true);
      GameManager.upgradesScreen.SetActive(false);
    }

    public void openShopMenu() {
      GameManager.mainScreen.SetActive(false);
      GameManager.shopScreen.SetActive(true);
    }

    public void closeShopMenu() {
      GameManager.mainScreen.SetActive(true);
      GameManager.shopScreen.SetActive(false);
    }

    public void openProfileMenu() {
      GameManager.mainScreen.SetActive(false);
      GameManager.profileScreen.SetActive(true);
    }

    public void closeProfileMenu() {
      GameManager.mainScreen.SetActive(true);
      GameManager.profileScreen.SetActive(false);
    }

    public void openPrestigeMenu() {
      GameManager.mainScreen.SetActive(false);
      GameManager.prestigeScreen.SetActive(true);
    }

    public void closePrestigeMenu() {
      GameManager.mainScreen.SetActive(true);
      GameManager.prestigeScreen.SetActive(false);
    }

    void Update() {
      wheyToClaim = Mathf.RoundToInt((Mathf.Sqrt(int.Parse(lifetimeValue.text) / 1000000000000000) * 150) - whey);

      forceTextMain.text = force.ToString();
      forceTextUpgrade.text = force.ToString();
      forceTextShop.text = force.ToString();
      wheyTextMain.text = whey.ToString();
      wheyTextUpgrade.text = whey.ToString();
      wheyTextShop.text = whey.ToString();
      wheyToClaimText.text = wheyToClaim.ToString();

      if (cpsCoroutine == null && CPS_value > 0) {
        cpsCoroutine = StartCoroutine(UpdateForceCPS());
      }
    }

    public void LoadData(GameData data)
    {
        this.force = data.force;
        this.multiplier = data.multiplier;
        this.CPS_value = data.CPS_value;
        this.whey = data.whey;
        this.forceValue.text = data.force.ToString();
        this.sessionValue.text = data.sessionForce.ToString();
        this.lifetimeValue.text = data.lifetimeForce.ToString();
        this.prestigeValue.text = data.totalPrestiges.ToString();
        this.wheyEarningsValue.text = data.lifetimeWheyEarned.ToString();
        this.wheySpentValue.text = data.lifetimeWheySpent.ToString();
    }

    public void SaveData(ref GameData data)
    {
        data.force = this.force;
        data.multiplier = this.multiplier;
        data.CPS_value = this.CPS_value;
        data.whey = this.whey;
        data.sessionForce = int.Parse(this.sessionValue.text);
        data.lifetimeForce = int.Parse(this.lifetimeValue.text);
        data.totalPrestiges = int.Parse(this.prestigeValue.text);
        data.lifetimeWheyEarned = int.Parse(this.wheyEarningsValue.text);
        data.lifetimeWheySpent = int.Parse(this.wheySpentValue.text);
    }

    public void ChangeAnimation()
    {
      int rand = Random.Range(0, 100);
      if (rand < 60){
        Clickers[randAnimation].SetActive(false);
        randAnimation = Random.Range(0, 3);
        Clickers[randAnimation].SetActive(true);
        } 
    }

    public void rewardAd(){
      this.force += 1000;
    }

}