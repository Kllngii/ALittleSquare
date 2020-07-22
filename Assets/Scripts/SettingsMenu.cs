using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour {
  public static bool settingsShown = false;

  [SerializeField, Tooltip("Referenz zum Root-Panel des Settings-Menüs")]
  public GameObject settingsMenuUI;
  //TODO Wenn esc gedrückt wird: Settings und Pause-Menü schließen

  public void switchState() {
    settingsShown = !settingsShown;
    if(settingsMenuUI == null)
      return;
    settingsMenuUI.active = settingsShown;
    Time.timeScale = settingsShown ? 0f : 1f;
  }
  /*
   *
   * Actions
   *
   */

}
