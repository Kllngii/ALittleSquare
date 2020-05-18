using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
namespace kllngii.menu {
public class MainMenu : MonoBehaviour {
  public bool isStart = false, isSettings = false, isQuit = false;
  public Color clickColor = Color.cyan;
  private Renderer render;
  private TextMeshPro textmeshPro;
  void Start() {
    render = GetComponent<Renderer>();
    textmeshPro = GetComponent<TextMeshPro>();
  }
  void OnMouseUp() {
    //FIXME Settings müssen eine Verwendung kriegen
    if(isStart) {
      changeColor();
      if(!PlayerPrefs.HasKey("nextLevel")) {
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
        return;
      }
      SceneManager.LoadScene("Level" + PlayerPrefs.GetInt("nextLevel"), LoadSceneMode.Single);
    }
    if(isSettings) {
      changeColor();
      SceneManager.LoadScene("Settings", LoadSceneMode.Single);
    }
    if (isQuit) {
      changeColor();
      Application.Quit();
    }
  }
  void changeColor() {
    if(render)
      render.material.color = clickColor;
    if(textmeshPro)
      textmeshPro.color = clickColor;
  }
}
}
