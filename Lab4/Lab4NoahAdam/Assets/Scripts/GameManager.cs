using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager gcInstance;
    public int playerCount;//Number of players playing :) -- Chosen in main menu
    public enum GameMode
    {
        OneTank,//One tank up to four players
        TwoTank //Two tanks up to two player EACH
    }
    public GameMode currentGameMode;
    public int killCount;//Number of kills the players have racked up, should only be incremented when a projectile kills a goon not when they retreat and despawn
    private void Awake()//Standard game management stuff
    {
        if (gcInstance == null)
        {
            gcInstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        killCount = 0;
        if(SceneManager.GetActiveScene().name == "MainGame")//If in game
        {

        }
    }
}
