using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
  public Vector2 speedMinMax;
  float width, height, speed;

  void Start()
  {
    width = Camera.main.aspect * Camera.main.orthographicSize;
    height = Camera.main.orthographicSize * 2;

    speed = Mathf.Lerp(speedMinMax.x, speedMinMax.y, Difficulty.GetDiffPercent());
  }

  public void SetSize(float size) {
    gameObject.transform.localScale = Vector2.one * size;
  } 

  public void Update()
  {
    transform.Translate(Vector2.down * Time.deltaTime * speed, Space.Self);

    if (OffScreen()) Destroy(gameObject);
  }

  public bool OffScreen()
  {
    return transform.position.y + transform.localScale.y < -height / 2;
  }
}
