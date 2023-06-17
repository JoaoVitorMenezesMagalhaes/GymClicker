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

    private IdleNumber force;
    private IdleNumber multiplier;
    private IdleNumber CPS_value;
    private IdleNumber whey;
    private int randAnimation = 0;

    private IdleNumber wheyToClaim;

    public void Increment() {
      // force += (multiplier + (multiplier * ((whey*2)/100)));
      if (whey.value > 0) {
        // mult = new IdleNumber(multiplier * ((whey*2)/100));
        IdleNumber perc = new IdleNumber(2, 0);
        IdleNumber div = new IdleNumber(100, 0);
        IdleNumber wheyCp = new IdleNumber(whey.value, whey.exp);
        wheyCp.mult(perc);
        wheyCp.div(div);
        IdleNumber multCp = new IdleNumber(multiplier.value, multiplier.exp);
        multCp.mult(wheyCp);
        multCp.add(multCp.value, multCp.exp);
        force.add(multCp.value, multCp.exp);
        perc = null;
        div = null;
        wheyCp = null;
        multCp = null;
      } else {
        force.add(multiplier.value, multiplier.exp);
      }

      forceValue.text = force.formatNumber();

      sessionValue.text = (int.Parse(sessionValue.text) + multiplier).ToString();
      lifetimeValue.text = (int.Parse(lifetimeValue.text) + multiplier).ToString();
    }

    public void Buy(int num)
    {
        if (num == 1 && force.canBuy(25, 0)) {
          multiplier.add(1, 0);
          force.sub(25, 0);
        }
        
        if(num == 2 && force.canBuy(125, 0))
        {
            multiplier.add(10, 0);
            force.sub(125, 0); 
        }

        if(num == 3 && force.canBuy(1.5, 3))
        {
            multiplier.add(100, 0);
            force.sub(1.5, 3);
        }

        if(num == 4 && force.canBuy(12, 3))
        {
            multiplier.add(1, 3);
            force.sub(12, 3);
        }

        if (num == 5 && force.canBuy(500, 3)) {
          multiplier.add(500, 3);
          force.sub(500, 3);
        }
        ChangeAnimation();
        forceValue.text = force.ToString();
    }

    public void CPS(int num)
    {
        int cost =  GameManager.CPS_values[num][1];
        int value = GameManager.CPS_values[num][0];

        if(force.canBuy(cost, (int) Mathf.Floor(Mathf.Log10(Mathf.Abs(cost)) / 3) * 3))
        {
            force.sub(cost, (int) Mathf.Floor(Mathf.Log10(Mathf.Abs(cost)) / 3) * 3);
            
            CPS_value.add(value, (int) Mathf.Floor(Mathf.Log10(Mathf.Abs(value)) / 3) * 3);
        }
    }

    private IEnumerator UpdateForceCPS() {
      while (true) {
        IdleNumber perc = new IdleNumber(2, 0);
        IdleNumber div = new IdleNumber(100, 0);
        IdleNumber wheyCp = new IdleNumber(whey.value, whey.exp);
        wheyCp.mult(perc);
        wheyCp.div(div);
        IdleNumber cpsCp = new IdleNumber(CPS_value.value, CPS_value.exp);
        cpsCp.mult(wheyCp);
        cpsCp.add(cpsCp.value, cpsCp.exp);
        force.add(cpsCp.value, cpsCp.exp);
        perc = null;
        div = null;

        forceValue.text = force.formatNumber();
        sessionValue.text = (int.Parse(sessionValue.text) + CPS_value).ToString();
        lifetimeValue.text = (int.Parse(lifetimeValue.text) + CPS_value).ToString();
        yield return new WaitForSeconds(1f);
      }
    }

    public void Prestige() {
      if (wheyToClaim.value > 0) {
        multiplier.value = 1;
        multiplier.exp = 0;
        force.value = 0;
        force.exp = 0;
        CPS_value.value = 0;
        CPS_value.exp = 0;
        whey.add(wheyToClaim.value, wheyToClaim.exp);

        sessionValue.text = "0";
        forceValue.text = "0";
        prestigeValue.text = (int.Parse(prestigeValue.text) + 1).ToString();
        wheyEarningsValue.text = (int.Parse(prestigeValue.text) + wheyToClaim).ToString();

        wheyToClaim.value = 0;
        wheyToClaim.exp = 0;
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

    void Update() {
      wheyToClaim = Mathf.RoundToInt((Mathf.Sqrt(int.Parse(lifetimeValue.text) / 1000000000) * 150) - whey);

      forceTextMain.text = force.formatNumber();
      forceTextUpgrade.text = force.formatNumber();
      forceTextShop.text = force.formatNumber();
      wheyTextMain.text = whey.ToString();
      wheyTextUpgrade.text = whey.ToString();
      wheyTextShop.text = whey.ToString();
      wheyToClaimText.text = wheyToClaim.ToString();

      if (cpsCoroutine == null && CPS_value.value > 0) {
        cpsCoroutine = StartCoroutine(UpdateForceCPS());
      }
    }

    public void LoadData(GameData data)
    {
        this.force = new IdleNumber(data.forceVal, data.forceExp);
        this.multiplier = new IdleNumber(data.multVal, data.multExp);
        this.CPS_value = new IdleNumber(data.CPS_valueVal, data.CPS_valueExp);
        this.whey = new IdleNumber(data.wheyVal, data.wheyExp);
        this.forceValue.text = this.force.formatNumber();
        this.sessionValue.text = data.sessionForce.ToString();
        this.lifetimeValue.text = data.lifetimeForce.ToString();
        this.prestigeValue.text = data.totalPrestiges.ToString();
        this.wheyEarningsValue.text = data.lifetimeWheyEarned.ToString();
        this.wheySpentValue.text = data.lifetimeWheySpent.ToString();
    }

    public void SaveData(ref GameData data)
    {
        data.forceVal = this.force.value;
        data.forceExp = this.force.exp;
        data.multVal = this.multiplier.value;
        data.multExp = this.multiplier.exp;
        data.CPS_valueVal = this.CPS_value.value;
        data.CPS_valueExp = this.CPS_value.exp;
        data.wheyVal = this.whey.value;
        data.wheyExp = this.whey.exp;
        data.sessionForce = int.Parse(this.sessionValue.text);
        data.lifetimeForce = int.Parse(this.lifetimeValue.text);
        data.totalPrestiges = int.Parse(this.prestigeValue.text);
        data.lifetimeWheyEarned = int.Parse(this.wheyEarningsValue.text);
        data.lifetimeWheySpent = int.Parse(this.wheySpentValue.text);
    }

    public void ChangeAnimation()
    {
      int rand = Random.Range(0, 100);
      if (rand < 75){
        Clickers[randAnimation].SetActive(false);
        randAnimation = Random.Range(0, 9);
        Clickers[randAnimation].SetActive(true);
      } 
    }

    public void rewardAd(){
      this.force.value += 1000;
    }

}