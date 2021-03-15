using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;

public class Player : MonoBehaviour
{
  public float speed = 20;
  float screenWidthHalf;

  public event Action OnPlayerDeath;

  void Start()
  {
    screenWidthHalf = (Camera.main.aspect * Camera.main.orthographicSize) + (transform.localScale.x / 2);
  }

  void Update()
  {
    Vector3 moveAmt = Vector3.right * Input.GetAxis("Horizontal");
    transform.Translate(moveAmt * Time.deltaTime * speed);

    if (transform.position.x < -screenWidthHalf) transform.position = new Vector2(screenWidthHalf, transform.position.y);
    if (transform.position.x > screenWidthHalf) transform.position = new Vector2(-screenWidthHalf, transform.position.y);
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.CompareTag("Falling Block"))
    {
      if (OnPlayerDeath != null) OnPlayerDeath();
      AudioManager.instance.Play("PlayerExplosion");
      Destroy(gameObject);
    }
  }
}
