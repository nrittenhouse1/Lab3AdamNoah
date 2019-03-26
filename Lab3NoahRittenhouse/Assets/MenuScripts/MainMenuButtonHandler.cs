using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonHandler : MonoBehaviour
{
    [SerializeField]
    GameObject optionsDisplay;//To access the displays in the editor
    bool optionsStatus;//Boolean that is true only when the display is viewable

    void Update()
    {
        optionsDisplay.SetActive(optionsStatus);
    }

    #region Button Methods

    public void PlayGame()//Loads first level
    {
        SceneManager.LoadScene(1);
    }
    public void ExitGame()//Quits game
    {
        Application.Quit();
    }



    //These are just to show/hide the secondary menu displays
    public void ToggleOptions()
    {
        optionsStatus = !optionsStatus;
    }
    #endregion
}
