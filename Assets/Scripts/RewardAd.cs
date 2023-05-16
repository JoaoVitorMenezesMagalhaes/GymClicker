using UnityEngine;
using UnityEngine.Advertisements;
using TMPro;

public class RewardAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener {
  [SerializeField] string _androidAdUnitId = "Rewarded_Android";
  [SerializeField] string _iOSAdUnitId = "Rewarded_iOS";
  [SerializeField] TextMeshProUGUI coinsText;
  [SerializeField] GameObject adsManager;
  string _adUnitId = "Rewarded_Android";

  void Awake() {
    #if UNITY_IOS
      _adUnitId = _iOSAdUnitId;
    #elif UNITY_ANDROID
      _adUnitId = _androidAdUnitId;
    #endif
  }

  public void LoadAd() {
    Debug.Log("Loading Ad: " + _adUnitId);
    Advertisement.Load(_adUnitId, this);
  }

  public void OnUnityAdsAdLoaded(string adUnitId) {
    Debug.Log("Ad Loaded: " + adUnitId);

    if (adUnitId.Equals(_adUnitId)) {
      ShowAd();
    }
  }

  public void ShowAd() {
    adsManager.GetComponent<BannerAd>().HideBannerAd();
    Advertisement.Show(_adUnitId, this);
  }

  public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState) {
    if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED)) {
      adsManager.GetComponent<BannerAd>().ShowBannerAd();
      Debug.Log("Unity Ads Rewarded Ad Completed");
      int coins = int.Parse(coinsText.text.Split(":")[1].Trim());
      coinsText.text = "Coins: " + (coins * 2).ToString();
    }
  }

  public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message) {
    Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
  }

  public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message){
    Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
  }

  public void OnUnityAdsShowStart(string adUnitId) {

  }
  public void OnUnityAdsShowClick(string adUnitId) {
    
  }

  void OnDestroy() {
  
  }
}
