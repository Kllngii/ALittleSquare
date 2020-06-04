using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Advertisements;

//FIXME Wenn der Spieler "stribt" alle Türen wieder schließen und alles zurücksetzen.
[RequireComponent(typeof(BoxCollider2D))]
public class CharacterController : MonoBehaviour {
  [SerializeField, Tooltip("Maximale Geschwindigkeit in Längeneinheiten pro Sekunde")]
  float speed = 9;

  [SerializeField, Tooltip("Beschleunigung bei Bodenkontakt")]
  float walkAcceleration = 75;

  [SerializeField, Tooltip("Beschleunigung in der Luft")]
  float airAcceleration = 30;

  [SerializeField, Tooltip("Bremswirkung am Boden")]
  float groundDeceleration = 70;

  [SerializeField, Tooltip("Maximale Sprunghöhe")]
  float jumpHeight = 4;

  [SerializeField, Tooltip("Die den Level beendene Tür")]
  GameObject doorObject;

  [SerializeField, Tooltip("Derzeitiger Level")]
  int level = 1;

  [SerializeField, Tooltip("Die Totzone für die Touchsteuerung")]
  float touchDeadzone = 2;

  private BoxCollider2D doorCollider;
  private BoxCollider2D boxCollider;
  private Vector2 velocity;
  private bool grounded;
  private float moveInput = 0;
  private float jumpInput = 0;
  private Vector3 startPos;
  private Quaternion startRot;
  private List<string> scenesInBuild = new List<string>();
  private Touch theTouch;
  private Vector2 touchStartPosition, touchEndPosition;
  private bool onPlatform = false;
  private BoxMover lastBox = null;
  private Lever lastLever = null;
  private void Awake() {
    for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++) {
      string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
      int lastSlash = scenePath.LastIndexOf("/");
      scenesInBuild.Add(scenePath.Substring(lastSlash + 1, scenePath.LastIndexOf(".") - lastSlash - 1));
    }
    boxCollider = GetComponent<BoxCollider2D>();
    doorCollider = doorObject.GetComponent<BoxCollider2D>();
    startPos = transform.position;
    startRot = transform.rotation;

    Text levelTag = GameObject.FindGameObjectsWithTag("LevelTag")[0].GetComponent<Text>();
    //FIXME funktioniert irgendwie nicht korrekt
    //if(levelTag != null)
      //levelTag.text = "Level " + level;
    if(levelTag != null)
      levelTag.text = "";
    //FIXME Später nutzen
    ImageChanger saveTag = GameObject.FindGameObjectsWithTag("SaveTag")[0].GetComponent<ImageChanger>();
    ImageChanger pauseTag = GameObject.FindGameObjectsWithTag("PauseTag")[0].GetComponent<ImageChanger>();
    if(saveTag != null && pauseTag != null) {
      saveTag.active = false;
      pauseTag.active = false;
    }
  }
  //FIXME Irgendwann durch neue API ersetzen
  private void updateTouch() {
    if (Input.touchCount > 0) {
      theTouch = Input.GetTouch(0);
      if (theTouch.phase == UnityEngine.TouchPhase.Began)
        touchStartPosition = theTouch.position;
      else if (theTouch.phase == UnityEngine.TouchPhase.Moved || theTouch.phase == UnityEngine.TouchPhase.Ended) {
        touchEndPosition = theTouch.position;
        float x = touchEndPosition.x - touchStartPosition.x;
        float y = touchEndPosition.y - touchStartPosition.y;
        //Deadzone
        if (Mathf.Abs(x) < touchDeadzone && Mathf.Abs(y) < touchDeadzone) {
          moveInput = 0;
          jumpInput = 0;
          return;
        }
        //Wenn man nach schräg oben wischt, muss man springen UND laufen
        else if(Mathf.Abs(x)/Mathf.Abs(y) > 0.95 || Mathf.Abs(x)/Mathf.Abs(y) < 1.05 ) {
          moveInput = x > 0 ? 1f : -1f;
          jumpInput = y > 0 ? 1f : 0f;
        }

        else if (Mathf.Abs(x) > Mathf.Abs(y))
          moveInput = x > 0 ? 1f : -1f;
        else
          jumpInput = y > 0 ? 1f : 0f;
      }
      if(theTouch.phase == UnityEngine.TouchPhase.Ended) {
        moveInput = 0;
        jumpInput = 0;
      }
    }
  }
  public void OnMove(InputValue value) {
    Vector2 v = value.Get<Vector2>();
    moveInput = v.x;
    jumpInput = v.y;
  }
  public void OnSubmit(InputValue value) {
    Debug.Log("Taste gedrückt!");
  }
  private void Update() {
    updateTouch();
    if (grounded) {
      velocity.y = 0;
      if(jumpInput != 0)
        velocity.y = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y));
    }
    float acceleration = grounded ? walkAcceleration : airAcceleration;
    float deceleration = grounded ? groundDeceleration : 0;
    if (moveInput != 0) {
      velocity.x = Mathf.MoveTowards(velocity.x, speed * moveInput, acceleration * Time.deltaTime);
    }
    else {
      if(!onPlatform)
        velocity.x = Mathf.MoveTowards(velocity.x, 0, deceleration * Time.deltaTime);
    }
    velocity.y += Physics2D.gravity.y * Time.deltaTime;
    grounded = false;
    // Retrieve all colliders we have intersected after velocity has been applied.
    Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0);
    onPlatform = false;
    bool noBox = true;
    bool noLever = true;
    foreach(Collider2D hit in hits) {
      // Ignore our own collider.
      if(hit == boxCollider)
        continue;
      if(hit.gameObject.tag == "Deadzone") {
        transform.position = startPos;
        transform.rotation = startRot;
        velocity.x = 0;
        velocity.y = 0;
        break;
      }
      if(hit.gameObject.tag == "Lever") {
        Lever lever = hit.gameObject.GetComponent<Lever>();
        lever.collisionWithCharacterOccurred();
        continue;
      }
      if(hit == doorCollider) {
        if(Random.Range(0, 12) < 2)
          Advertisement.Show();
        PlayerPrefs.SetInt("nextLevel", (++level));
        string name = "Level"+level;
        Debug.Log("Lade nächstes Level: " + name);
        if(scenesInBuild.Contains(name))
          SceneManager.LoadScene(name, LoadSceneMode.Single);
        //FIXME Später eine "Spielbeendet" Szene erstellen und hier verwenden
        else
          SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        continue;
      }
      if(hit.gameObject.tag == "Box") {
        BoxMover box = hit.gameObject.GetComponent<BoxMover>();
        noBox = false;
        if(Mathf.Abs(box.gameObject.transform.position.y - gameObject.transform.position.y) < 0.9f)
          box.overrideVelocityWithPlayerVelocity(velocity.x);
      }
      ColliderDistance2D colliderDistance = hit.Distance(boxCollider);
      // Ensure that we are still overlapping this collider.
      // The overlap may no longer exist due to another intersected collider
      // pushing us out of this one.
      if(colliderDistance.isOverlapped) {
        transform.Translate(colliderDistance.pointA - colliderDistance.pointB);
        // If we intersect an object beneath us, set grounded to true.
        if (Vector2.Angle(colliderDistance.normal, Vector2.up) < 90 && velocity.y < 0)
          grounded = true;
      }
    }
    if(noBox && lastBox != null) {
      lastBox.overrideVelocityWithPlayerVelocity(0f);
      lastBox = null;
    }
    if(noLever && lastLever != null) {
      lastLever.unpressLever();
      lastLever = null;
    }
    transform.Translate(velocity * Time.deltaTime);
    Camera.main.transform.position = transform.position;
  }
}
