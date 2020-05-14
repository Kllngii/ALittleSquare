using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MouseHover : MonoBehaviour {
  //FIXME Eventuell per Color32(R, G, B, A) lösen
  //FIXME Einen BoxCollider vorraussetzen (requirecomponent oder so)
  public Color basicColor = Color.white;
  public Color hoverColor = Color.green;
  private Renderer render;
  private TextMeshPro textmeshPro;
  void Start() {
    render = GetComponent<Renderer>();
    textmeshPro = GetComponent<TextMeshPro>();
    if(render)
      render.material.color = basicColor;
    if(textmeshPro)
      textmeshPro.color = basicColor;
  }
  void OnMouseEnter() {
    if(render)
      render.material.color = hoverColor;
    if(textmeshPro)
      textmeshPro.color = hoverColor;
  }
  void OnMouseExit() {
    if(render)
      render.material.color = basicColor;
    if(textmeshPro)
      textmeshPro.color = basicColor;
  }
}
