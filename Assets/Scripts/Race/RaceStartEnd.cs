using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
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
            rManager = FindObjectOfType<RaceManager>();
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

    private void Reset()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }
}
