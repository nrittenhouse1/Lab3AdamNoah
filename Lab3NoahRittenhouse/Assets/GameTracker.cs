using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTracker : MonoBehaviour
{
    public static GameTracker gcInstance;
    public int numPlayers = 0;
    public bool team1Win = false;
    public bool team2Win = false;

    public int numGoons = 10; //number of goons still alive
    public int numTeam1 = 2; //number of players on team 1 that are still alive

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

    private void Update()
    {

        if(numGoons <= 0)//if no goons left
        {
            team1Win = true;
        }
        if(numTeam1 <= 0)//if no players left
        {
            team2Win = true;
        }
    }
}
