using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Lever : MonoBehaviour {
  [SerializeField, Tooltip("Diese ID muss der ID eines Openables entsprechen")]
  public int identifier;
  private GameObject[] openables;
  void Awake() {
    openables = GameObject.FindGameObjectsWithTag("Openable");
  }
  public void collisionWithCharacterOccurred() {
    Debug.Log("Lever Hit! Im Lever-Script");
    foreach(GameObject openable in openables) {
      Openable script = openable.GetComponent<Openable>();
      if(script.identifier == identifier) {
        script.switchStateOfOpenable();
        break;
      }
    }
  }
}
