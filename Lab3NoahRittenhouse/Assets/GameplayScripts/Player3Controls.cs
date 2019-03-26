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

    void Start()
    {

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
        goonCount = 10;
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
        //Debug.Log(currentState);
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

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    MovementOrder(physicalSpawnpoints[3].position);
                }
                if (Input.GetMouseButtonDown(0))//If left click shoot ray from mouseclick and if it hits the floor send goons
                {
                    Vector3 mousePos = player3Camera.ScreenToWorldPoint(Input.mousePosition);
                    RaycastHit ray;
                    if (Physics.Raycast(mousePos, Vector3.down, out ray) && (ray.collider.tag == "Floor" || ray.collider.tag == "Player"))
                    {
                        GameObject marker = Instantiate(movementIndicatorPrefab, new Vector3(ray.point.x, ray.point.y + 50, ray.point.z), new Quaternion(0, 0, 0, 1), null);
                        marker.transform.eulerAngles = new Vector3(90, 0, 0);
                        MovementOrder(ray.point);
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

    public void SpawnLocation(int choice)
    {
        if (Input.GetMouseButtonUp(0))
        {
            //Choices are 0-3
            spawnpoint = physicalSpawnpoints[choice];
            goonParent.localPosition = spawnpoint.localPosition;
            virtualSpawnpoint = virtualSpawnpoints[choice];//Just for visuals
            doOnce = true;
            currentState = playerState.beginBattle;

        }
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

    public void MovementOrder(Vector3 destination)
    {
        foreach (GameObject goon in goons)
        {
            goon.GetComponent<NavMeshAgent>().SetDestination(destination);
        }
    }
}
