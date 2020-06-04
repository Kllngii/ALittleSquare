using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BoxMover : MonoBehaviour {

  private BoxCollider2D boxCollider;
  private Vector2 velocity;
  private Lever lastLever = null;
  private bool grounded;

  private void Awake() {
    boxCollider = GetComponent<BoxCollider2D>();

  }
  private void Update() {
    applyGravity();
  }
  private void applyGravity() {
    if(grounded)
      velocity.y = 0;
    velocity.y += Physics2D.gravity.y * Time.deltaTime;
    grounded = false;
    Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0);
    bool noLever = true;
    foreach(Collider2D hit in hits) {
      if(hit == boxCollider)
        continue;
      if(hit.gameObject.tag == "Lever") {
        noLever = false;
        Lever lever = hit.gameObject.GetComponent<Lever>();
        Debug.Log("Lever Hit! Im BoxMover");
        lever.collisionWithCharacterOccurred();
        continue;
      }
      ColliderDistance2D colliderDistance = hit.Distance(boxCollider);
      if(colliderDistance.isOverlapped) {
        transform.Translate(colliderDistance.pointA - colliderDistance.pointB);
        if (Vector2.Angle(colliderDistance.normal, Vector2.up) < 90 && velocity.y < 0)
          grounded = true;
      }
    }
    if(noLever && lastLever != null) {
      lastLever.unpressLever();
      lastLever = null;
    }
    transform.Translate(velocity * Time.deltaTime);
  }
  public void overrideVelocityWithPlayerVelocity(float xVelocity) {
    transform.Translate(new Vector2(xVelocity, 0f) * Time.deltaTime);
  }
}
