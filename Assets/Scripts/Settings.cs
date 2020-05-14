using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Settings : MonoBehaviour {
  public bool isBackToMenu = false;
  public Color clickColor = Color.cyan;
  private Renderer render;
  private TextMeshPro textmeshPro;
  void Start() {
    render = GetComponent<Renderer>();
    textmeshPro = GetComponent<TextMeshPro>();
  }
  void OnMouseUp() {
    if(isBackToMenu) {
      changeColor();
      SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
  }
  void changeColor() {
    if(render)
      render.material.color = clickColor;
    if(textmeshPro)
      textmeshPro.color = clickColor;
  }
}
