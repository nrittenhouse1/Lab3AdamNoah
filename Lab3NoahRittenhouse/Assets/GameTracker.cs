using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTracker : MonoBehaviour
{
    public static int numPlayers = 0;
    public static bool team1Win = false;
    public static bool team2Win = false;

    public static int numGoons = 10; //number of goons still alive
    public static int numTeam1 = 2; //number of players on team 1 that are still alive

}
