using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mencontroller : MonoBehaviour
{
    public GameObject pauseCanvas;
    bool isPaused;

    void Start()
    {
        Time.timeScale = 1f;
        isPaused = false;
        pauseCanvas.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        pauseCanvas.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

}
