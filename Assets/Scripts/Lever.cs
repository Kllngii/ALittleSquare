using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Lever : MonoBehaviour {
  [SerializeField, Tooltip("Diese ID muss der ID eines Openables entsprechen")]
  public int identifier;
  private GameObject[] openables;
  private GameObject[] invisibles;
  void Awake() {
    openables = GameObject.FindGameObjectsWithTag("Openable");
    invisibles = GameObject.FindGameObjectsWithTag("Disappear");
  }
  public void unpressLever() {
    transform.localScale = new Vector3(1f, 0.25f, 1f);
    foreach(GameObject openable in openables) {
      Openable script = openable.GetComponent<Openable>();
      if(script.identifier == identifier) {
        script.close();
        break;
      }
    }
  }
  public void collisionWithCharacterOccurred() {
    transform.localScale = new Vector3(1f, 0.125f, 1f);
    Debug.Log("Lever Hit! Im Lever-Script");
    foreach(GameObject openable in openables) {
      Openable script = openable.GetComponent<Openable>();
      if(script.identifier == identifier) {
        script.switchStateOfOpenable();
        break;
      }
    }
    foreach(GameObject inv in invisibles) {
      MakeInvisibleAfterTime script = inv.GetComponent<MakeInvisibleAfterTime>();
      if(script.identifier == identifier) {
        script.disappear();
        break;
      }
    }
  }
}
