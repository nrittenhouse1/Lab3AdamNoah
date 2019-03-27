using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtonHandler : MonoBehaviour
{
    GameTracker gcInstance;
    [SerializeField]
    GameObject optionsDisplay;//To access the displays in the editor
    bool optionsStatus;//Boolean that is true only when the display is viewable
    [SerializeField]
    Text scoreText;
    private void Start()
    {
        gcInstance = GameTracker.gcInstance;
    }
    void Update()
    {
        optionsDisplay.SetActive(optionsStatus);

        if(gcInstance.team1Win)
        {
            scoreText.text = "TeAm One WIn!!!!!!!!!!!";
        }
        else if(gcInstance.team2Win)
        {
            scoreText.text = "TeAm tWWo WIn????";
        }
        else
        {
            scoreText.text = "Play the game first, John.";
        }
    }

    #region Button Methods

    public void PlayGame(int numPlayer)//Loads first level
    {
        gcInstance.numPlayers = numPlayer;
        gcInstance.numTeam1 = numPlayer;
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
