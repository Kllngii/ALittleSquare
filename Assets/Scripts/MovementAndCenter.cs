using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MovementAndCenter : MonoBehaviour {
  [Range(0, 20)][SerializeField]
  public float speed = 0.2f;
  public float jumpHeight = 2.7f;
  public Transform groundCheck;
  [Range(0, 2)][SerializeField]
  public float groundCheckRadius = 0.72f;
  public LayerMask whatIsGround;

  public Transform doorCheck;
  [Range(0, 2)][SerializeField]
  public float doorCheckRadius = 0;
  public LayerMask whatIsDoor;
  public int level = 1;
  private bool grounded = false;
  private bool hithead = false;
  private bool hitside = false;
  private Vector2 v = new Vector2();
  public void OnMove(InputValue value) {
    v = value.Get<Vector2>();
    if(v.y > 0 && grounded)
      v.y = jumpHeight;
  }
  void Update() {
    if(groundCheck != null) {
      Collider2D ground = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
      if(ground != null && ground.bounds.min.y > transform.position.y) {
        grounded = false;
        hithead = true;
      }
      else if(ground != null)
        grounded = true;
      else
        grounded = false;
    }
    if(v.y > jumpHeight*0.05)
      v.y *= 0.95f;
    else if(hithead || !grounded) {
      v.y = -jumpHeight*0.4f;
      hithead = false;
    }
    else if(grounded)
      v.y = 0;

    //Es sollte nicht möglich sein horizontal durch Wände durch zu gehen. Also testen wir...
    if(hitside) {
      v.x = 0;
      hitside = false;
    }
    Vector2 pointA = transform.position;
    Vector2 pointB = transform.position;

    pointA.x -= 0.6f;
    pointB.x += 0.6f;

    Collider2D hit = Physics2D.OverlapArea(pointA, pointB, whatIsGround);
    if(hit != null && hit.bounds.max.x < transform.position.x) {
      v.x = -v.x;
      hitside = true;
    }
    else if(hit != null && hit.bounds.min.x > transform.position.x) {
      v.x = -v.x;
      hitside = true;
    }
    //Testen, ob man auf einer Tür steht
    Collider2D door = Physics2D.OverlapCircle(doorCheck.position, doorCheckRadius, whatIsDoor);
    if(door != null) {
      Debug.Log("Lade nächstes Level: " + "Level"+(++level));
      SceneManager.LoadScene("Level"+level, LoadSceneMode.Single);
    }
    transform.Translate(v * speed);
    Camera.main.transform.position = transform.position;
  }
}
