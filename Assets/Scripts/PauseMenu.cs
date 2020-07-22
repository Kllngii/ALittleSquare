using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
  public bool paused = false;

  [SerializeField, Tooltip("Referenz zum Root-Panel des Pause-Menüs")]
  public GameObject pauseMenuUI;
  public SettingsMenu settingsMenu;

  public void OnPause() {
    switchState();
  }
  public void switchState() {
    paused = !paused;
    if(pauseMenuUI == null)
      return;
    pauseMenuUI.active = paused;
    Time.timeScale = paused ? 0f : 1f;
  }
  /*
   *
   * Actions
   *
   */
   public void backToMenuAction() {
     SceneManager.LoadScene("Menu", LoadSceneMode.Single);
   }
   public void openSettingsAction() {
     settingsMenu.switchState();
   }
}
