using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageChanger : MonoBehaviour {
  public Image[] images;
  [Range(0.1f,2f)]
  public float speed = 0.5f;
  public bool active = true;

  private float deltaTime;
  private int i = 0;
  private bool updateFlag = false;
  private Color white = Color.white;
  private Color trans = new Color(0,0,0,0);
  void Start() {
    foreach(Image im in images) {
      im.color = trans;
    }
  }
  void Update() {
    deltaTime += Time.deltaTime;
    if(updateFlag) {
      if(i >= images.Length)
        i = 0;
      foreach(Image im in images) {
        im.color = trans;
      }
      images[i].color = white;
      i++;
      updateFlag = false;
    }
    if(deltaTime > speed) {
      deltaTime -= speed;
      updateFlag = true;
    }
  }
}
