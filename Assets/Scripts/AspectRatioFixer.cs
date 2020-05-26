﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectRatioFixer : MonoBehaviour {
  [SerializeField]
  float targetAspectRatio = 16.0f / 9.0f;

  private void Start() {
    float windowAspectRatio = (float)Screen.width / (float)Screen.height;
    float scaleHeight = windowAspectRatio / targetAspectRatio;
    Camera camera = GetComponent<Camera>();
    
    if (scaleHeight < 1.0f) {
      Rect rect = camera.rect;
      rect.width = 1.0f;
      rect.height = scaleHeight;
      rect.x = 0;
      rect.y = (1.0f - scaleHeight) / 2.0f;
      camera.rect = rect;
    }
    else {
      float scalewidth = 1.0f / scaleHeight;
      Rect rect = camera.rect;
      rect.width = scalewidth;
      rect.height = 1.0f;
      rect.x = (1.0f - scalewidth) / 2.0f;
      rect.y = 0;
      camera.rect = rect;
    }
  }
}
