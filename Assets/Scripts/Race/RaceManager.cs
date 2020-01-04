using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    /*Atributes*/
    [SerializeField]
    private Transform StartingPoint;
    [SerializeField]
    private Transform EndPoint;
    [SerializeField]
    private GameObject Player; 


    bool RaceFinished = false;


    private static RaceManager SingletonManager; 
    public static RaceManager Instace { get { return SingletonManager; } }

    private void Awake()
    {
        if (SingletonManager && SingletonManager != this)
            Destroy(this.gameObject);
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

    public void StartRace()
    {
        RaceFinished = false; 
        if (Player)
            Player.transform.SetPositionAndRotation(StartingPoint.position, StartingPoint.rotation);  
    }

    public void PlayerReachedEnd()
    {
        RaceFinished = true; 
    }

    public bool IsRaceFinished() { return RaceFinished;  }


}
