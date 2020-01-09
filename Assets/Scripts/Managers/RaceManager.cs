using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    /*Atributes*/
    [SerializeField]
    private Transform StartingPoint = null;
    [SerializeField]
    private Transform EndPoint = null;
    [SerializeField]
    private GameObject Player = null;
    [SerializeField] private GameManager gameManager = null;

    private Transform CheckPoint; 

    bool RaceFinished = false;

    private static RaceManager SingletonManager; 
    public static RaceManager Instance { get { return SingletonManager; } }

    private void Awake()
    {
        if (SingletonManager && SingletonManager != this)
            Destroy(gameObject);
        else
            SingletonManager = this; 
    }

    private void Start()
    {
        if(!Player)
        {
            GameObject[] players;
            players = GameObject.FindGameObjectsWithTag("Player");
            Player = (players.Length > 0) ? players[0] : null; 
        }
    }

    /*Player starts the race, with time 0 and at starting point*/
    public void StartRace()
    {
        RaceFinished = false;
        CheckPoint = StartingPoint;
        RespawnPlayer(); 
        Debug.Log("Player starts race"); 
    }
    /*Player restart the race from the last checkpoint*/
    public void RestartRace()
    {
        RaceFinished = false;
        RespawnPlayer();
        PowerUpManager.Instance.ChoosePUPhase();
        Debug.Log("Player restart the race"); 
    }
    /*Notifies the player has reached the end*/
    public void PlayerReachedEnd()
    {
        RaceFinished = true;
        gameManager.StopGame();
        StartCoroutine(gameManager.DisplayScore());
        Debug.Log("Player finished the race"); 
    }

    /*Check if race has finished*/
    public bool IsRaceFinished() { return RaceFinished;  }

    /*Teleport the player to the last checkpoint */
    private void RespawnPlayer()
    {
        if (Player)
            Player.transform.SetPositionAndRotation(CheckPoint.position, CheckPoint.rotation);
    }
}
