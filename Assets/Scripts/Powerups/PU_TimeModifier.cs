using UnityEngine;

[CreateAssetMenu(fileName = "New Time Modifier", menuName = "Powerup Factory/New Time Modifier")]
public class PU_TimeModifier : Powerup
{
    [SerializeField] private float multiplier = 0.5f;
    public override void Execute(PlayerController player)
    {
        Time.timeScale = 1 * multiplier;
    }
}
