using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class InitializeAdsScript : MonoBehaviour {
  [SerializeField]
  public bool testMode = false;

  string gameIdApple = "3622512";
  string gameIdGoogle = "3622513";

  void Start() {
    if(Application.platform == RuntimePlatform.IPhonePlayer)
      Advertisement.Initialize(gameIdApple, testMode);
      if(Application.platform == RuntimePlatform.Android)
        Advertisement.Initialize(gameIdGoogle, testMode);
  }
}
