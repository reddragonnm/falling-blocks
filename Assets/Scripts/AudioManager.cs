using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Sound
{
  public string name;
  public AudioClip clip;
  public bool loop;

  [Range(0, 1)]
  public float volume;
  [Range(.1f, 3f)]
  public float pitch;

  [HideInInspector]
  public AudioSource source;
}

public class AudioManager : MonoBehaviour
{
  public static AudioManager instance;
  public Sound[] sounds;

  private void Awake()
  {
    // Ensure only one AudioManager is present
    if (instance == null) instance = this;
    else Destroy(gameObject);

    DontDestroyOnLoad(gameObject);

    foreach (Sound s in sounds)
    {
      s.source = gameObject.AddComponent<AudioSource>();
      s.source.clip = s.clip;
      s.source.volume = s.volume;
      s.source.pitch = s.pitch;
      s.source.loop = s.loop;
    }
  }

  private void Start()
  {
    Play("Theme", false);
  }

  public void Play(string name, bool warning = true)
  {
    Sound s = Array.Find(sounds, sounds => sounds.name == name);

    if (s == null)
    {
      if (warning) Debug.LogWarning("AudioManager: Sound " + name + " not found");
      return;
    }

    s.source.Play();
  }

  public void NextScene()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
  }
  }
