using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Openable : MonoBehaviour {
  [SerializeField, Tooltip("Diese ID muss der ID eines Openables entsprechen")]
  public int identifier;
  //false = closed; true = open
  private bool state = false;
  private Vector3 localScale;
  void Awake() {
    localScale = transform.localScale;
  }
  public void switchStateOfOpenable() {
    //FIXME Objekt wieder "schließen"
    Debug.Log("Lever Hit! Im Openable-Script");
    transform.localScale = new Vector3(0,0,0);
  }
}
