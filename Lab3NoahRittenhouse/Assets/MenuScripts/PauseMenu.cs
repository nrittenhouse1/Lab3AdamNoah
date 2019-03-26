using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    bool paused;

    [SerializeField]
    List<GameObject> pauseMenus;

    void Start()
    {
        paused = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

        if(paused)
        {
            Time.timeScale = 0;
            foreach (GameObject menu in pauseMenus)
            {
                menu.SetActive(true);
            }
        }
        else
        {
            Time.timeScale = 1;
            foreach (GameObject menu in pauseMenus)
            {
                menu.SetActive(false);
            }
        }
    }
    void Pause()
    {
        paused = !paused;
    }
    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Resume()
    {
        Pause();
    }
}
