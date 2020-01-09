using UnityEngine;

[CreateAssetMenu(fileName = "New Powerup List", menuName = "Powerup Factory/New Powerup List", order = 0)]
public class PowerupList : ScriptableObject
{
    [SerializeField]
    private Powerup[] powerups = new Powerup[0];
    public Powerup[] Powerups => powerups;
}
