using UnityEngine;

[CreateAssetMenu(fileName = "New Dash Modifier", menuName = "Powerup Factory/New Dash Modifier")]
public class PU_DashModifier : Powerup
{
    [SerializeField] private int increaseCount = 1;
    [SerializeField] private float multiplier = 1.5f;
    public override void Execute(PlayerController player)
    {
        player.ModifyDashCount(increaseCount);
        player.ModifyDashDist(multiplier);
    }
}
