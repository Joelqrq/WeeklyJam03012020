using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float timeToMaxSpeed = 0.2f;
    private Rigidbody rb;
    private PlayerControls playerControls;

    private Vector3 moveResult;
    private Vector2 moveAxis;
    private float currentSpeed;
    private float currentProgress;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerControls = new PlayerControls();
        playerControls.Movement.Walk.performed += Move;
        playerControls.Movement.Walk.canceled += Halt;
        playerControls.Movement.Jump.performed += Jump;
        playerControls.Enable();
    }

    private void FixedUpdate()
    {
        moveResult.x = moveAxis.x;
        moveResult.z = moveAxis.y;
        if (moveAxis.sqrMagnitude > 0.1f)
        {
            currentProgress += Time.deltaTime / timeToMaxSpeed;
            Debug.Log(currentProgress);
        }
        currentSpeed = Mathf.Lerp(0f, (PlayerManager.GetMaxSpeed() * 0.7f), currentProgress);
        moveResult *= currentSpeed * Time.deltaTime;
        //Debug.Log($"Direction: {moveAxis} Result: {moveResult}");
        moveResult += transform.position;
        rb.MovePosition(moveResult);
        moveResult = Vector3.zero;
    }

    private void Move(InputAction.CallbackContext context)
    {
        moveAxis = context.ReadValue<Vector2>();
        //Debug.Log($"Direction: {moveAxis}");
    }

    private void Halt(InputAction.CallbackContext context)
    {
        moveAxis = Vector2.zero;
        currentSpeed = 0f;
        currentProgress = 0f;
    }

    private void Jump(InputAction.CallbackContext context)
    {
        rb.AddForce(MathLibrary.CalculateJump(transform.up, rb.velocity), ForceMode.Impulse);
    }
}
