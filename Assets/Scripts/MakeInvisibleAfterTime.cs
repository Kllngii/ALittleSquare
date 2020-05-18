using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MakeInvisibleAfterTime : MonoBehaviour {
  public bool executeAfterAwake = true;
  public bool executeAfterAction = false;
  [SerializeField, Range(0, 10)]
  public float time = 5f;
  [SerializeField, Range(0, 1)]
  public float speed = 0.999f;
  public int identifier = 0;
  private bool isCoroutineExecuting = false;
  private bool destroy = false;
  private int tick = 0;
  void Awake() {
    if(executeAfterAwake)
      StartCoroutine(executeAfterTime(time, () => {
        destroy = true;
      }));

  }
  void Update() {
    if(tick < 250 && destroy) {
      float x = transform.localScale.x * speed;
      float y = transform.localScale.y * speed;
      float z = transform.localScale.z * speed;
      transform.localScale = new Vector3(x,y,z);
      tick++;
    }
  }
  private IEnumerator executeAfterTime(float time, Action task) {
    if (isCoroutineExecuting)
      yield break;
    isCoroutineExecuting = true;
    yield return new WaitForSeconds(time);
    task();
    isCoroutineExecuting = false;
  }
  public void disappear() {
    destroy = true;
  }
}
