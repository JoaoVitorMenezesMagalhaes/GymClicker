using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Game : MonoBehaviour, IDataPersistence
{
    public TextMeshProUGUI forceTextMain;
    public TextMeshProUGUI wheyTextMain;
    public TextMeshProUGUI forceTextUpgrade;
    public TextMeshProUGUI wheyTextUpgrade;
    public TextMeshProUGUI forceTextShop;
    public TextMeshProUGUI wheyTextShop;
    public TextMeshProUGUI wheyToClaimText;
    public TextMeshProUGUI totalWheyText;
    public TextMeshProUGUI forceValue;
    public TextMeshProUGUI sessionValue;
    public TextMeshProUGUI lifetimeValue;
    public TextMeshProUGUI prestigeValue;
    public TextMeshProUGUI wheyEarningsValue;
    public TextMeshProUGUI wheySpentValue;
    public TextMeshProUGUI awayEarnedText;
    public List<GameObject> Clickers;  

    private Coroutine cpsCoroutine; 

    private IdleNumber force;
    private long multiplier;
    private long CPS_value;
    private long whey;
    private int randAnimation = 0;
    private long forceGainedWhileAway = 0;
    private string lastDate;
    private string nowDate;

    private long wheyToClaim;

    public void Increment() {
      // force += (multiplier + (multiplier * ((whey*2)/100)));
      IdleNumber mult;
      if (whey > 0) {
        mult = new IdleNumber(multiplier * ((whey*2)/100));
      } else {
        mult = new IdleNumber(multiplier);
      }
      force.add(mult);
      mult = null;

      forceValue.text = force.formatNumber();
      sessionValue.text = (long.Parse(sessionValue.text) + multiplier).ToString();
      lifetimeValue.text = (long.Parse(lifetimeValue.text) + multiplier).ToString();
    }

    public void Buy(int num)
    {
        if (num == 1 && force.canBuy(25)) {
          multiplier += 1;
          force.sub(25);
        }
        
        if(num == 2 && force.canBuy(125))
        {
            multiplier += 10;
            force.sub(125); 
        }

        if(num == 3 && force.canBuy(1500))
        {
            multiplier += 100;
            force.sub(1500);
        }

        if(num == 4 && force.canBuy(12000))
        {
            multiplier += 1000;
            force.sub(12000);
        }

        if (num == 5 && force.canBuy(500000)) {
          multiplier += 500000;
          force.sub(500000);
        }
        ChangeAnimation();
        forceValue.text = force.formatNumber();
    }

    public void CPS(int num)
    {
        int cost =  GameManager.CPS_values[num][1];
        int value = GameManager.CPS_values[num][0];

        if(force.canBuy((double) cost))
        {
            force.sub((double) cost);
            
            CPS_value += value;
        }
    }

    private IEnumerator UpdateForceCPS() {
      while (true) {
        IdleNumber mult = new IdleNumber(CPS_value + (CPS_value * ((whey*2)/100)));
        // force += (CPS_value + (CPS_value * ((whey*2)/100)));
        force.add(mult);
        forceValue.text = force.formatNumber();
        sessionValue.text = (long.Parse(sessionValue.text) + CPS_value).ToString();
        lifetimeValue.text = (long.Parse(lifetimeValue.text) + CPS_value).ToString();
        yield return new WaitForSeconds(1f);
      }
    }

    public void Prestige() {
      if (wheyToClaim > 0) {
        multiplier = 1;
        force.value = 0;
        force.exp = 0;
        CPS_value = 0;
        whey += wheyToClaim;

        sessionValue.text = "0";
        forceValue.text = "0";
        prestigeValue.text = (long.Parse(prestigeValue.text) + 1).ToString();
        wheyEarningsValue.text = (long.Parse(wheyEarningsValue.text) + wheyToClaim).ToString();
        totalWheyText.text = (long.Parse(totalWheyText.text) + wheyToClaim).ToString();

        wheyToClaim = 0;
      }
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

    public void openWelcomeBackMenu() {
      GameManager.mainScreen.SetActive(false);
      GameManager.welcomeBackScreen.SetActive(true);
    }

    public void closeWelcomeBackMenu() {
      GameManager.mainScreen.SetActive(true);
      GameManager.welcomeBackScreen.SetActive(false);
    }

    public void closeOnlyWelcomeBackMenu() {
      GameManager.welcomeBackScreen.SetActive(false);
    }

    void Update() {
      if (forceGainedWhileAway > 0) {
        openWelcomeBackMenu();
      } else {
        closeOnlyWelcomeBackMenu();
      }	

      if (Mathf.RoundToInt((Mathf.Sqrt(long.Parse(lifetimeValue.text) / 1000000000) * 150) - whey) < 0) {
        wheyToClaim = 0;
      } else {
        wheyToClaim = Mathf.RoundToInt((Mathf.Sqrt(long.Parse(lifetimeValue.text) / 1000000000) * 150) - whey);
      }

      forceTextMain.text = force.formatNumber();
      forceTextUpgrade.text = force.formatNumber();
      forceTextShop.text = force.formatNumber();
      wheyTextMain.text = whey.ToString();
      wheyTextUpgrade.text = whey.ToString();
      wheyTextShop.text = whey.ToString();
      wheyToClaimText.text = wheyToClaim.ToString();
      awayEarnedText.text = forceGainedWhileAway.ToString();
      totalWheyText.text = whey.ToString();

      if (cpsCoroutine == null && CPS_value > 0) {
        cpsCoroutine = StartCoroutine(UpdateForceCPS());
      }
    }

    public void claimAway(){
      IdleNumber idleGained = new IdleNumber(forceGainedWhileAway);
      force.add(idleGained);
      idleGained = null;
      forceGainedWhileAway = 0;
      closeWelcomeBackMenu();
    }

    public void rewardAd(){
      forceGainedWhileAway *= 2;
      claimAway();
    }

    public void LoadData(GameData data)
    {
        this.force = new IdleNumber(data.forceVal * Mathf.Pow(10, data.forceExp));
        this.multiplier = data.multiplier;
        this.CPS_value = data.CPS_value;
        this.whey = data.whey;
        this.forceValue.text = this.force.formatNumber();
        this.sessionValue.text = data.sessionForce.ToString();
        this.lifetimeValue.text = data.lifetimeForce.ToString();
        this.prestigeValue.text = data.totalPrestiges.ToString();
        this.wheyEarningsValue.text = data.lifetimeWheyEarned.ToString();
        this.wheySpentValue.text = data.lifetimeWheySpent.ToString();
        this.lastDate = data.lastDate;
    }

    public void SaveData(ref GameData data)
    {
        data.forceVal = this.force.value;
        data.forceExp = this.force.exp;
        data.multiplier = this.multiplier;
        data.CPS_value = this.CPS_value;
        data.whey = this.whey;
        data.sessionForce = long.Parse(this.sessionValue.text);
        data.lifetimeForce = long.Parse(this.lifetimeValue.text);
        data.totalPrestiges = long.Parse(this.prestigeValue.text);
        data.lifetimeWheyEarned = long.Parse(this.wheyEarningsValue.text);
        data.lifetimeWheySpent = long.Parse(this.wheySpentValue.text);
        data.lastDate = this.lastDate;
    }

    public void ChangeAnimation()
    {
      int rand = UnityEngine.Random.Range(0, 100);
      if (rand < 75){
        Clickers[randAnimation].SetActive(false);
        randAnimation = UnityEngine.Random.Range(0, 9);
        Clickers[randAnimation].SetActive(true);
      } 
    }
    
    public void timeAway(){
        nowDate = System.DateTime.Now.ToString();
        if (!string.IsNullOrEmpty(lastDate))
        {
            DateTime nowDateTime = DateTime.Parse(nowDate);
            DateTime lastDateTime = DateTime.Parse(lastDate);
            double ts = (nowDateTime - lastDateTime).TotalSeconds;
            forceGainedWhileAway = (int)(ts * CPS_value);
        }
    }
    
    private void OnApplicationFocus(bool hasFocus)
    {
      if (hasFocus)
      {
          timeAway();
      }
      else
      {
          lastDate = System.DateTime.Now.ToString();
      }    
    }
}
