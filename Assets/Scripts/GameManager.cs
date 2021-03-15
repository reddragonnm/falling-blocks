using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public Vector2 spawnTimeMinMax;
  public Vector2 blockSize;
  public float rotationAngle;
  public GameObject blockPrefab;

  Vector2 screenHalfUnits;
  float nextSpawnTime;

  void Start()
  {
    screenHalfUnits = new Vector2(Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);
  }

  void Update()
  {
    if (Time.time > nextSpawnTime)
    {
      nextSpawnTime = Time.time + Mathf.Lerp(spawnTimeMinMax.y, spawnTimeMinMax.x, Difficulty.GetDiffPercent());
      NewBlock();
    }
  }

  void NewBlock()
  {
    float dim = Random.Range(blockSize.x, blockSize.y);
    float rotation = Random.Range(-rotationAngle, rotationAngle);

    Vector2 spawnPos = new Vector2(Random.Range(-screenHalfUnits.x + dim / 2, screenHalfUnits.x - dim / 2), screenHalfUnits.y + dim);
    GameObject block = Instantiate(blockPrefab, spawnPos, Quaternion.Euler(0, 0, rotation));

    Block blockScript = block.GetComponent<Block>();
    blockScript.SetSize(dim);
  }
}
