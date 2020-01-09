using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceStartEnd : MonoBehaviour
{

    public bool isRaceStart = true;
    [SerializeField]
    private RaceManager rManager; 

    // Start is called before the first frame update
    void Start()
    {
        if(!rManager)
        {
            rManager = GameObject.FindObjectOfType<RaceManager>();
            if (!rManager)
                Debug.LogError("Race manager not found"); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (!isRaceStart)
            {
                rManager.PlayerReachedEnd();
            }
        }
       
    }

}
