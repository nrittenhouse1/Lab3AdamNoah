using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Player3Controls : MonoBehaviour
{
    Camera player3Camera;
    List<Transform> physicalSpawnpoints, virtualSpawnpoints, spawnpointButtons;
    List<GameObject> goons;
    Transform spawnpoint, virtualSpawnpoint, spIndicator, playerIndicator, player3HUD, goonParent;
    GameObject battleButton, pauseButton, goonPrefab, movementIndicatorPrefab;
    int goonCount;
    bool doOnce;
    //Phases of gameplay
    enum playerState
    {
        setSpawn,//Setup
        beginBattle,//Wait time
        battleBegun,//Battle time
        battleOver//End time
    }
    playerState currentState = playerState.setSpawn;

    public bool isAI;

    void Start()
    {
        isAI = false;
        //Set camera
        player3Camera = transform.GetChild(0).GetComponent<Camera>();
        //Set HUD
        player3HUD = transform.Find("Player3HUD");
        //Set physicalSpawnpoints, virtualSpawnpoints, and buttons
        physicalSpawnpoints = new List<Transform>();
        virtualSpawnpoints = new List<Transform>();
        spawnpointButtons = new List<Transform>();
        for (int i = 0; i < 4; i++)
        {
            physicalSpawnpoints.Add(transform.Find("Spawnpoints").transform.GetChild(i));//Finds the set of physicalSpawnpoints and adds each to the list
            virtualSpawnpoints.Add(player3HUD.Find("VirtualSpawns").transform.GetChild(i));
            spawnpointButtons.Add(player3HUD.Find("SpawnButtons").transform.GetChild(i + 1));
        }
        goons = new List<GameObject>();
        goonCount = 50;
        goonPrefab = Resources.Load("Goon") as GameObject;
        goonParent = transform.Find("Goons");
        spawnpoint = null;
        spIndicator = player3HUD.Find("VirtualSpawns/SpawnpointIndicator");
        playerIndicator = player3HUD.Find("VirtualSpawns/PlayerSpawn");
        spIndicator.gameObject.SetActive(false);
        battleButton = player3HUD.Find("WaveButton").gameObject;
        movementIndicatorPrefab = Resources.Load("MovementIndicator") as GameObject;
        doOnce = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            isAI = !isAI;
        }
        Debug.Log("Is the AI on? " + isAI);
        if (!isAI)//If AI is not on
        {
            switch (currentState)
            {
                case playerState.setSpawn:
                    if (doOnce)
                    {
                        battleButton.GetComponent<Button>().interactable = false;
                        doOnce = false;
                    }
                    break;
                case playerState.beginBattle:
                    #region Spawnpoint visuals on overview screen
                    spIndicator.gameObject.SetActive(true);
                    spIndicator.position = virtualSpawnpoint.position;
                    #endregion
                    if (doOnce)
                    {
                        battleButton.GetComponent<Button>().interactable = true;
                        doOnce = false;
                    }
                    break;
                case playerState.battleBegun:
                    if (doOnce)
                    {
                        playerIndicator.gameObject.SetActive(false);
                        battleButton.GetComponent<Button>().interactable = false;
                        foreach (Transform button in spawnpointButtons)
                        {
                            button.gameObject.GetComponent<Button>().interactable = false;
                        }
                        doOnce = false;
                    }
                    #region Player control area
                    if (Input.GetMouseButtonDown(0))//If left click shoot ray from mouseclick and if it hits the floor send goons
                    {
                        Vector3 mousePos = player3Camera.ScreenToWorldPoint(Input.mousePosition);
                        RaycastHit ray;
                        if (Physics.Raycast(mousePos, Vector3.down, out ray) && (ray.collider.tag == "Floor" || ray.collider.tag == "Team1" || ray.collider.tag == "Team2"))
                        {
                            GameObject marker = Instantiate(movementIndicatorPrefab, new Vector3(ray.point.x, ray.point.y + 50, ray.point.z), new Quaternion(0, 0, 0, 1), null);
                            marker.transform.eulerAngles = new Vector3(90, 0, 0);
                            MovementOrder(ray.point, -1);
                        }
                    }

                    #endregion
                    break;
                case playerState.battleOver:
                    break;
                default:
                    break;
            }
        }
        else
        {
            battleButton.GetComponent<Button>().interactable = false;
            foreach (Transform button in spawnpointButtons)
            {
                button.GetComponent<Button>().interactable = false;
            }
            playerIndicator.gameObject.SetActive(false);
            switch (currentState)
            {
                case playerState.setSpawn://Can set spawn only
                    int rnd = Random.Range(0, 3);
                    SpawnLocation(rnd);
                    break;
                case playerState.beginBattle://Can set spawn or begin battle (AI will just begin)
                    BeginBattle();
                    break;
                case playerState.battleBegun:
                    //Find players
                    GameObject[] players = GameObject.FindGameObjectsWithTag("Team1");
                    if (doOnce)
                    {
                        for (int i = 0; i < goons.Count; i++)
                        {
                            if (i < goons.Count / 2)
                            {
                                MovementOrder(players[0].transform.position, i);
                            }
                            else
                            {
                                MovementOrder(players[1].transform.position, i);
                            }
                        }
                        doOnce = false;
                    }
                    if(players.Length == 1)
                    {
                        if (players[0].gameObject.activeInHierarchy)
                        {
                            MovementOrder(players[0].transform.position, -1);
                        }
                        else
                        {
                            MovementOrder(players[1].transform.position, -1);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < goons.Count; i++)
                        {
                            if (i < goons.Count / 2)
                            {
                                MovementOrder(players[0].transform.position, i);
                            }
                            else
                            {
                                MovementOrder(players[1].transform.position, i);
                            }
                        }
                    }
                    break;
                case playerState.battleOver:
                    break;
                default:
                    break;
            }
        }
    }

    public void SpawnLocation(int choice)
    {
        //Choices are 0-3
        spawnpoint = physicalSpawnpoints[choice];
        goonParent.localPosition = spawnpoint.localPosition;
        virtualSpawnpoint = virtualSpawnpoints[choice];//Just for visuals
        doOnce = true;
        currentState = playerState.beginBattle;
    }

    public void BeginBattle()
    {
        doOnce = true;
        currentState = playerState.battleBegun;
        //Spawn goons
        for (int i = 0; i < goonCount; i++)
        {
            GameObject currentGoon = Instantiate(goonPrefab, goonParent.position, goonParent.rotation, goonParent);
            goons.Add(currentGoon);
        }
    }


    private void OnDrawGizmos()
    {
        if (spawnpoint != null)
        {
            Gizmos.DrawCube(spawnpoint.position, Vector3.one * 3);
        }
    }

    public void MovementOrder(Vector3 destination, int goonToOrder)
    {
        if (goonToOrder < 0)
        {
            foreach (GameObject goon in goons)
            {
                goon.GetComponent<NavMeshAgent>().SetDestination(destination);
            }
        }
        else
        {
            goons[goonToOrder].GetComponent<NavMeshAgent>().SetDestination(destination);
        }
    }
}
