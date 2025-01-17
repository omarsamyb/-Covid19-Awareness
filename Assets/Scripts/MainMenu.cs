﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        if (!AudioManager.instance.isPlaying("MenuMusic"))
        {
            AudioManager.instance.Play("MenuMusic");
        }
    }
    public void Play()
    {
        AudioManager.instance.Stop("MenuMusic");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
