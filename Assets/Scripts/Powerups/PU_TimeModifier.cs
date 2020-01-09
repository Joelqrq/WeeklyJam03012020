using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "New Time Modifier", menuName = "Powerup Factory/New Time Modifier")]
public class PU_TimeModifier : Powerup
{
    [SerializeField] private float multiplier = 0.5f;
    public override void Execute(PlayerController player)
    {
        player.PlayerControls.Movement.SlowMotion.performed += SlowMotion;
        player.PlayerControls.Movement.CancelSlowMotion.performed += CancelSlowMotion;
    }

    private void SlowMotion(InputAction.CallbackContext context)
    {
        Time.timeScale = 1 * multiplier; 
        Time.fixedDeltaTime = 0.02f * multiplier;
        Debug.Log("slowmo");
    }

    private void CancelSlowMotion(InputAction.CallbackContext context)
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        Debug.Log("remove s");
    }
}
