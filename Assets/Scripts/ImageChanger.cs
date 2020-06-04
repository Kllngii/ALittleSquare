using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageChanger : MonoBehaviour {

  [Range(0.1f,2f)]
  public float speed = 0.5f;
  public bool active = true;
  public Sprite[] images;
  public Sprite clearImage;

  private Image parent;
  private float deltaTime;
  private int i = 0;
  private bool updateFlag = false;
  void Start() {
    parent = GetComponent<Image>();
  }
  void Update() {
    deltaTime += Time.deltaTime;
    if(updateFlag) {
      parent.overrideSprite = nextImage();
      updateFlag = false;
    }
    if(deltaTime > speed) {
      deltaTime -= speed;
      updateFlag = true;
      parent.overrideSprite = clearImage;
    }
    parent.enabled = active;
  }
  Sprite nextImage() {
    if(i >= images.Length)
      i = 0;
    return images[i++];
  }
}
