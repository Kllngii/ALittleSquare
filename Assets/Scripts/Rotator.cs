using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {
  public float speed = 1f, x = 1f, y = 1f, z = 1f;
  void FixedUpdate() {
    Vector3 r = new Vector3((transform.rotation.x + (x * speed)) % 360f,
      (transform.rotation.y + (y * speed)) % 360f, (transform.rotation.z + (z * speed)) % 360f);
    transform.Rotate(r);
  }
}
