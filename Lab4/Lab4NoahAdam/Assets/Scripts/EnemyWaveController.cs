using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyWaveController : MonoBehaviour
{
    public static EnemyWaveController enemyController;//This will let the goon script tell the B I G  B O S S they have died or fled
    public GameManager gcInstance;//Instance of the game manager so the B I G  B O S S can tell if there are one or two targets

    public Text goonCountDisplay, timerDisplay;
    public GameObject goonPrefab;//Prefab for the goon
    int goonsAllowed;//Total goons allowed to spawn
    public List<GameObject> goonsAlive;//Public so when the goons die or retreat they can decrement the list

    public List<GameObject> targets;//Will either have one or two targets depending on gamemode
    bool waveOver;//Bool that is true if the wave is over/hasn't begun/can begin

    float waveTimer;//Tracks how long the wave has gone on for
    public float maxWaveTime;//Indicates how long the wave will go on for before the goons retreat

    float passiveTimer;//Tracks how much time has passed between waves
    public float maxPassiveTime;//Indicates how much time there should be between waves

    public enum WaveState
    {
        WaveZero,//Before everything there was nothing
        WaveOne,
        WaveTwo,
        WaveThree,
        WaveFour,
        WaveFive
    }
    public WaveState currentWave;
    public enum WavePhase
    {
        Passive,//Only active when no goons exist
        Fight,//Exists when the goons are targeting and attacking the players
        Retreat//Active only when there are goons but the wave has gone on for too long
    }
    public WavePhase currentPhase;

    void Start()
    {
        gcInstance = GameManager.gcInstance;
        currentWave = WaveState.WaveZero;//Start with no wave on
        currentPhase = WavePhase.Passive;
        targets = new List<GameObject>();
        waveOver = true;

        waveTimer = 0;
        if (maxWaveTime <= 0)
        {
            maxWaveTime = 30;//Defaults to 30 seconds
        }
        passiveTimer = 0;
        if (maxPassiveTime <= 0)
        {
            maxPassiveTime = 10;//Defaults to 10 seconds
        }
    }

    void Update()
    {
        #region Debug Stuff
        Debug.Log(currentWave);//Show me the W A V E
        if (Input.GetKeyDown(KeyCode.Space) && waveOver && currentWave < WaveState.WaveFive)
        //Debug wave starting and only if the wave hasnt started and theres only been 4
        {
            BeginWave();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && !waveOver)//Debug retreat
        {
            Retreat();
        }
        #endregion

        goonCountDisplay.text = goonsAlive.Count.ToString();

        #region PassivePhase
        if (currentPhase == WavePhase.Passive)//if its passive phase
        {
            if (waveOver && currentWave < WaveState.WaveFive && passiveTimer >= maxPassiveTime)//If the wave is over and there are still waves available...
            {
                BeginWave();
            }
            passiveTimer += Time.deltaTime;
            timerDisplay.text = "Next wave in " + Mathf.RoundToInt(maxPassiveTime - passiveTimer) + " seconds...";
        }
        #endregion

        #region CombatPhase
        if (!waveOver && currentPhase == WavePhase.Fight)//If the wave has not ended and they are not retreating
        {
            GiveTarget();
            waveTimer += Time.deltaTime;
            timerDisplay.text = "Wave ends in " + Mathf.RoundToInt(maxWaveTime - waveTimer) + " seconds...";
        }
        if (waveTimer >= maxWaveTime)//If the wave has gone on for the max wave time...
        {
            Retreat();
        }
        #endregion

        #region RetreatPhase
        if (currentPhase == WavePhase.Retreat)//If its retreat phase, loop through all goons and remove them as they escape
        {
            //The above if is there to make script more efficient
            foreach (GameObject goon in goonsAlive)
            {
                if (goon.GetComponent<NavMeshAgent>().destination == transform.position)
                //if they are retreating and have reached their destination/the exit
                {
                    RemoveGoon(goon);
                }
            }
        }
        #endregion

        if (goonsAlive.Count <= 0)//if there are no living goons or all goons have fled
        {
            waveOver = true;//The wave is over boys
            currentPhase = WavePhase.Passive;
        }
    }

    public void BeginWave()//Repeatable ad infinitum
    {
        currentPhase = WavePhase.Fight;
        waveTimer = 0;//Reset wave timer
        waveOver = false;//Wave has begun
        passiveTimer = 0;//Reset passive timer
        currentWave = currentWave + 1;//Move to next wave
        if (gcInstance.playerCount > 0)//If anyone is playing -- Mostly for early game dev since no main menu yet
        {
            goonsAllowed = (int)currentWave * 6;//6, 12, 
            for (int i = 0; i < goonsAllowed; i++)//Loop until all allowed goons have been spawned
            {
                GameObject goon = Instantiate(goonPrefab, transform);//Spawn goon
                goonsAlive.Add(goon);//Add to list of living goons, to be removed as they die, and recalled at Retreat phase
            }
        }
        GiveTarget();//Once all goons are instantiated set them off
    }

    public void GiveTarget()//Loops through all living goons to give target
    {
        targets.Clear();//Clear old targets in case a player died
        if (GameObject.FindGameObjectsWithTag("Player").Length == 1)//One tank to target
        {
            targets.Add(GameObject.FindGameObjectWithTag("Player"));//Find first and hopefully only tank with Player tag
        }
        else if (GameObject.FindGameObjectsWithTag("Player").Length > 1)//Two tanks to target
        {
            targets.Add(GameObject.FindGameObjectsWithTag("Player")[0]);//Find first tank with Player tag
            targets.Add(GameObject.FindGameObjectsWithTag("Player")[1]);//Find second and hopefully last tank with Player tag
        }
        for (int i = 0; i < goonsAlive.Count; i++)//Loop until all living goons have been given a target
        {
            if (targets.Count == 1)//1 player
            {
                goonsAlive[i].GetComponent<NavMeshAgent>().SetDestination(targets[0].transform.position);//Set target to player
            }
            else if (targets.Count == 2)//2 players, split the goons in half
            {
                if (i < goonsAllowed / 2)//If less than half the goons have been set to a target...
                {
                    goonsAlive[i].GetComponent<NavMeshAgent>().SetDestination(targets[0].transform.position);//Set target to player 1
                }
                else
                {
                    goonsAlive[i].GetComponent<NavMeshAgent>().SetDestination(targets[1].transform.position);//Set target to player 2
                }
            }
        }
    }

    public void Retreat()
    //Once this is called, all living goons go to a selected destination and once reached will despawn and 
    //tell the B I G  B O S S they have departed
    {
        waveTimer = 0;
        timerDisplay.text = "The enemy is retreating!";
        currentPhase = WavePhase.Retreat;
        foreach (GameObject goon in goonsAlive)//For every goon alive...
        {
            goon.GetComponent<NavMeshAgent>().SetDestination(transform.position);
        }
    }

    public void RemoveGoon(GameObject goonBeingRemoved)//Called when the goon is killed OR retreats and disappears
    {
        goonsAlive.Remove(goonBeingRemoved);
    }
}
