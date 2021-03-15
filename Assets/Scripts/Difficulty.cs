using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Difficulty
{
  static float secondsMaxDiff = 60;

  public static float GetDiffPercent()
  {
    return Mathf.Clamp01(Time.timeSinceLevelLoad / secondsMaxDiff);
  }
}
