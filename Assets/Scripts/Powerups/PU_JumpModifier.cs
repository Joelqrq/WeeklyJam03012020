using UnityEngine;

[CreateAssetMenu(fileName = "New Jump Modifier", menuName = "Powerup Factory/New Jump Modifier")]
public class PU_JumpModifier : Powerup
{
    [SerializeField] private int increaseCount = 1;
    [SerializeField] private float multiplier = 1.5f;
    public override void Execute(PlayerController player)
    {
        player.ModifyJumpCount(increaseCount);
        player.ModifyJumpSpeed(multiplier);
    }
}
