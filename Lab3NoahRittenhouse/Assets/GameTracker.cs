using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTracker : MonoBehaviour
{
    public static GameTracker gcInstance;
    public int numPlayers;
    public bool team1Win;
    public bool team2Win;

    public int numGoons; //number of goons still alive
    public int numTeam1; //number of players on team 1 that are still alive
    bool doOnce;
    private void Awake()
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
    private void Start()
    {
        numPlayers = 0;
        team1Win = false;
        team2Win = false;
        numGoons = 20;
        numTeam1 = 2;
        doOnce = true;
    }
    private void Update()
    {
        if (numGoons <= 0)//if no goons left
        {
            team1Win = true;
        }
        if (numTeam1 <= 0)//if no players left
        {
            team2Win = true;
        }
        if(team1Win || team2Win)
        {
            if (doOnce)
            {
                SceneManager.LoadScene(0);
                doOnce = false;
            }
        }
    }
}
