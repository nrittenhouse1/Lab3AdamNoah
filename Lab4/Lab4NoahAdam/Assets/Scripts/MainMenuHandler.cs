using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    GameManager gcInstance;
    
    public GameObject playerParent, vehicleParent, confirmationPanel;
    public Text gameInfo;
    
    private void Start()
    {
        gcInstance = GameManager.gcInstance;
    }

    private void Update()
    {
        gameInfo.text = gcInstance.playerCount + " player(s)" + " with " + gcInstance.currentGameMode;
    }

    public void Confirmation(int playerCount)
    {
        gcInstance.playerCount = playerCount;
        playerParent.SetActive(false);
        confirmationPanel.SetActive(true);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("MainGame");
    }
    public void OpenChoice()
    {
        vehicleParent.SetActive(true);
        playerParent.SetActive(false);
    }
    public void VehicleChoice(int vehicleCount)
    {
        switch (vehicleCount)
        {
            case 1:
                gcInstance.currentGameMode = GameManager.GameMode.OneTank;
                break;
            case 2:
                gcInstance.currentGameMode = GameManager.GameMode.TwoTank;
                break;
            default:
                break;
        }
        vehicleParent.SetActive(false);
        playerParent.SetActive(false);
        Confirmation(4);
    }
    public void Cancel(GameObject panel)
    {
        if (panel.activeInHierarchy)
        {
            panel.SetActive(false);
        }
        else
        {
            panel.SetActive(true);
        }
        playerParent.SetActive(true);
        gcInstance.currentGameMode = GameManager.GameMode.OneTank;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
