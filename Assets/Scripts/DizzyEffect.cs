using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Aktivieren per:
 * dizzy = GetComponent<DizzyEffect>();
 * dizzy.enabled = true;
 */
public class DizzyEffect : MonoBehaviour {
  private Matrix4x4 originalProjection;
  private Camera cam;
  [Range(0,1)]
  public float strength = 0.1F;
  [Range(0,3)]
  public float speed = 1F;
  void Start() {
    cam = GetComponent<Camera>();
    originalProjection = cam.projectionMatrix;
  }
  void Update() {
    Matrix4x4 p = originalProjection;
    p.m01 += Mathf.Sin(Time.time * 1.2F * speed) * strength;
    p.m10 += Mathf.Sin(Time.time * 1.5F * speed) * strength;
    cam.projectionMatrix = p;
  }
}
