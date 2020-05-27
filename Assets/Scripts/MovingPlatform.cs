using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
  [SerializeField, Tooltip("Ein Empty-Gameobject als Startpunkt der Bewegung")]
  GameObject startEmpty;
  [SerializeField, Tooltip("Ein Empty-Gameobject als Endpunkt der Bewegung")]
  GameObject endEmpty;
  [SerializeField, Tooltip("Die Geschwindigkeit der Bewegung")]
  float speed;
  [SerializeField, Tooltip("Ob sich die Platform bewegt")]
  bool active = true;

  public Vector3 dir;

  private Vector3 startVector;
  private Vector3 endVector;
  private bool reversed = false;

  void Awake() {
    startVector = startEmpty.transform.position;
    endVector = endEmpty.transform.position;
    //Z auf null setzen, um auf gleicher Höhe mit dem Rest zu bleiben
    startVector.z = 0;
    endVector.z = 0;
    transform.position = startVector;
  }
  void Update() {
    Vector3 difference = (endVector - startVector) / Vector3.Distance(endVector, startVector);
    //FIXME Zurzeit muss der EndVector immer weiter rechts oder weiter unten als der StartVector sein
    if(transform.position.x-endVector.x > 0 || transform.position.y-endVector.y > 0)
      reversed = true;
    if(transform.position.x-startVector.x < 0 || transform.position.y-startVector.y < 0)
      reversed = false;
    if(reversed)
      difference = startVector - endVector;
    dir = difference * Time.deltaTime * speed;
    transform.Translate(dir);
  }
}
