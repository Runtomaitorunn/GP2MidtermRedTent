using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource windSource;
    public AudioClip bgm;
    public AudioClip windSound;

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);

    }
    private void Start()
    {
        musicSource.clip = bgm;
        musicSource.Play();
        windSource.clip = windSound;
        windSource.Play();
    }
   
}
