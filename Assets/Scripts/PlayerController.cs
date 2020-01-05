using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float timeToMaxSpeed = 0.2f;
    [SerializeField] private float timeToStop = 0.2f;
    [SerializeField] private float fallMultiplier = 1.2f;
    [SerializeField] private float directionDamping = 0.2f;
    [Header("Dash Setting")]
    [SerializeField] private float dashDuration = 0.5f;
    [SerializeField] private int dashCount = 1;
    [SerializeField] private float dashDistance = 2f;
    [Header("Ground Collision Setting")]
    [SerializeField] private Vector3 gcOffset = Vector3.zero;
    [SerializeField] private float gcRadius = 0.5f;
    [SerializeField] private LayerMask gcMask = 0;
    [Header("Wall Run Setting")]
    [SerializeField] private Vector3 wcOffset = Vector3.zero;
    [SerializeField] private float wcDist = 1f;
    [SerializeField] private LayerMask wcMask = 0;

    private Rigidbody rb;
    private PlayerControls playerControls;

    private Vector3 moveResult;
    private Vector2 moveAxis;
    private float currentSpeed;

    //Jump
    private int jumpCount = 2;
    private int currentJumpCount;

    private const float gravity = -9.81f;

    //Dash
    private int currentDashCount;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerControls = new PlayerControls();
        playerControls.Movement.Walk.performed += Move;
        playerControls.Movement.Walk.canceled += Halt;
        playerControls.Movement.Jump.performed += Jump;
        playerControls.Movement.Dash.performed += Dash;
        playerControls.Enable();
    }

    private void FixedUpdate()
    {
        RefreshJump();
        RefreshDash();
        if (moveAxis.sqrMagnitude > 0.1f)
        {
            moveResult.x = moveAxis.x;
            moveResult.z = moveAxis.y;
            currentSpeed = Mathf.Lerp(currentSpeed, (PlayerManager.GetMaxSpeed() * 0.7f), Time.deltaTime / timeToMaxSpeed);
            moveResult.x *= currentSpeed;
            moveResult.z *= currentSpeed;
        }
        else
        {
            moveResult.x = -Mathf.Lerp(0f, rb.velocity.x, Time.deltaTime / timeToStop);
            moveResult.z = -Mathf.Lerp(0f, rb.velocity.z, Time.deltaTime / timeToStop);
        }

        ClampVelocity();
        moveResult.y = rb.velocity.y;
        Gravity();
        rb.velocity = moveResult;
        //Debug.Log($"Direction: {moveAxis} Result: {moveResult}");
    }

    private void Move(InputAction.CallbackContext context)
    {
        moveAxis = context.ReadValue<Vector2>();
        //Debug.Log($"Direction: {moveAxis}");
        DirectionDamping();
    }

    private void Halt(InputAction.CallbackContext context)
    {
        moveAxis = Vector2.zero;
        currentSpeed = 0f;
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (currentJumpCount > 0)
        {
            rb.AddForce(MathLibrary.CalculateJump(transform.up, rb.velocity), ForceMode.Impulse);
            currentJumpCount--;
        }
    }

    private void RefreshJump()
    {
        if (IsGrounded() && rb.velocity.y < 0f)
            currentJumpCount = jumpCount;
    }

    private void Gravity()
    {
        if (IsGrounded())
            return;

        moveResult.y += gravity * fallMultiplier * Time.deltaTime;
    }

    private void ClampVelocity()
    {
        if (Mathf.Abs(rb.velocity.x) > PlayerManager.GetMaxSpeed())
        {
            moveResult.x = moveAxis.x * PlayerManager.GetMaxSpeed();
        }
        if (Mathf.Abs(rb.velocity.z) > PlayerManager.GetMaxSpeed())
        {
            moveResult.z = moveAxis.y * PlayerManager.GetMaxSpeed();
        }
    }

    private void DirectionDamping()
    {
        if (Mathf.Sign(rb.velocity.x) != moveAxis.x)
        {
            moveResult.x = rb.velocity.x * directionDamping;
            moveResult.y = rb.velocity.y;
            rb.velocity = moveResult;
        }
        if (Mathf.Sign(rb.velocity.z) != moveAxis.y)
        {
            moveResult.z = rb.velocity.z * directionDamping;
            moveResult.y = rb.velocity.y;
            rb.velocity = moveResult;
        }
    }

    private void Dash(InputAction.CallbackContext context)
    {
        Debug.Log("Dashed");
        if (IsGrounded() || currentDashCount == 0)
            return;

        currentDashCount--;
        StartCoroutine(DashState());
    }

    private IEnumerator DashState()
    {
        rb.velocity = Vector3.zero;
        playerControls.Movement.Walk.Disable();
        playerControls.Movement.Jump.Disable();
        Vector3 prevPos = transform.position;
        float progress = 0f;
        float tempDuration = dashDuration;
        while(tempDuration > 0f)
        {
            progress += Time.deltaTime / dashDuration;
            rb.MovePosition(Vector3.Lerp(prevPos, prevPos + (transform.forward * dashDistance), progress));
            tempDuration -= Time.deltaTime;
            yield return null;
        }
        rb.velocity = Vector3.zero;
        playerControls.Movement.Walk.Enable();
        playerControls.Movement.Jump.Enable();
    }

    private void RefreshDash()
    {
        if (IsGrounded())
            currentDashCount = dashCount;
    }

    public void ModifyJumpCount(int amount) => jumpCount += amount;
    public void ModifyDashCount(int amount) => dashCount += amount;

    private bool IsGrounded()
    {
        return Physics.CheckSphere(transform.position + gcOffset, gcRadius, gcMask);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(transform.position + gcOffset, gcRadius);
    }
}
