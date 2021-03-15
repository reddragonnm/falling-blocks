using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
  public Text secondsText;
  public Text highScoreText;
  public Text countDownText;

  public GameObject newHighScoreObj;
  public GameObject gameOverScreen;
  public GameObject pauseScreen;

  public CameraShake cameraShake;
  public ParticleSystem playerExplosion;

  bool gameOver = false;
  bool paused = false;

  float startTime;
  Player player;

  private void Start()
  {
    startTime = Time.time;

    player = FindObjectOfType<Player>();
    player.OnPlayerDeath += GameOverEffects;
    player.OnPlayerDeath += GameOver;

    StartCoroutine(CountDown());
  }

  private void Update()
  {
    if (gameOver)
    {
      if (Input.GetKeyDown(KeyCode.Space)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
      else if (Input.GetKeyDown(KeyCode.R)) ResetScore();
    }
    else
    {
      if (Input.GetKeyDown(KeyCode.P))
      {
        paused = !paused;
        pauseScreen.SetActive(paused);

        if (paused) Time.timeScale = 0;
        else
        {
          StartCoroutine(CountDown());
        }
      }
    }
  }

  IEnumerator CountDown()
  {
    Time.timeScale = 0;

    countDownText.gameObject.SetActive(true);

    int i = 3;
    while (i > 0)
    {
      countDownText.text = (i--).ToString();
      yield return new WaitForSecondsRealtime(1);
    }

    countDownText.gameObject.SetActive(false);

    Time.timeScale = 1;
  }

  public void GameOver()
  {
    gameOver = true;
    gameOverScreen.SetActive(true);

    int score = Mathf.RoundToInt(Time.timeSinceLevelLoad);
    secondsText.text = score.ToString();

    if (score > PlayerPrefs.GetInt("HighScore", 0))
    {
      PlayerPrefs.SetInt("HighScore", score);
      newHighScoreObj.SetActive(true);
    }

    highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
  }

  public void ResetScore()
  {
    PlayerPrefs.SetInt("HighScore", 0);
    highScoreText.text = "0";
  }

  public void GameOverEffects()
  {
    playerExplosion.transform.position = player.transform.position;
    playerExplosion.Play();
    StartCoroutine(cameraShake.Shake(0.2f, 0.5f));
  }
}
