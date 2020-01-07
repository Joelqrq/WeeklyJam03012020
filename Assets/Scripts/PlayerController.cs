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
    [SerializeField] private float dashMultiplier = 1.5f;
    [SerializeField] private float dashDistance = 2f;
    [SerializeField] private int dashCount = 1;
    [Header("Ground Collision Setting")]
    [SerializeField] private Vector3 gcOffset = Vector3.zero;
    [SerializeField] private float gcRadius = 0.5f;
    [SerializeField] private LayerMask gcMask = 0;
    [Header("Slope Collision Setting")]
    [SerializeField] private float slopeSpeed = 10f;
    [SerializeField] private float slopeThreshold = 0.98f;
    [SerializeField] private Vector3 slopeOffset = Vector3.zero;
    [SerializeField] private float slopeDist = 0.5f;
    [SerializeField] private LayerMask slopeMask = 0;
    [Header("Wall Run Setting")]
    [SerializeField] private Vector3 wcOffset = Vector3.zero;
    [SerializeField] private float wcDist = 1f;
    [SerializeField] private LayerMask wcMask = 0;

    private Rigidbody rb;
    private PlayerControls playerControls;

    private Vector3 inputResult;
    private Vector2 moveAxis;
    private float currentSpeed;

    //Jump
    private int jumpCount = 2;
    private int currentJumpCount;

    private const float gravity = -9.81f;

    //Dash
    private int currentDashCount;

    private State state;
    private Vector3 otherResult;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerControls = new PlayerControls();
        playerControls.Movement.Walk.performed += Move;
        playerControls.Movement.Walk.canceled += Halt;
        playerControls.Movement.Jump.performed += Jump;
        playerControls.Movement.Dash.performed += Dash;
        playerControls.Movement.WallRun.performed += WallRun;
        playerControls.Movement.WallRun.canceled += StopWallRun;
        playerControls.Enable();
    }

    private void FixedUpdate()
    {
        RefreshJump();
        RefreshDash();
        CheckSlope();
        HandleMove();
    }

    private void Move(InputAction.CallbackContext context)
    {
        moveAxis = context.ReadValue<Vector2>();
        //Debug.Log($"Direction: {moveAxis}");
        DirectionDamping();
    }

    private void HandleMove()
    {
        if (state == State.Normal && moveAxis.sqrMagnitude > 0.1f)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, (PlayerManager.GetMaxSpeed()), Time.deltaTime / timeToMaxSpeed);
            inputResult += (transform.right * moveAxis.x + transform.forward * moveAxis.y) * currentSpeed * Time.deltaTime;
        }
        else if(state == State.Normal)
        {
            inputResult.x = -Mathf.Lerp(0f, rb.velocity.x, Time.deltaTime / timeToStop);
            inputResult.z = -Mathf.Lerp(0f, rb.velocity.z, Time.deltaTime / timeToStop);
        }
        else if(state == State.Slope)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, (PlayerManager.GetMaxSpeed()), Time.deltaTime / timeToMaxSpeed);
            inputResult += (transform.right * moveAxis.x) * currentSpeed * Time.deltaTime;
        }

        ClampVelocity();
        inputResult.y = rb.velocity.y;
        Gravity();
        rb.velocity = inputResult + otherResult;
        //Debug.Log($"Direction: {moveAxis} Result: {inputResult}");
    }

    private void Halt(InputAction.CallbackContext context)
    {
        if (state == State.Normal || state == State.Slope)
        {
            moveAxis = Vector2.zero;
            currentSpeed = 0f;
        }
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (state != State.Normal)
            return;

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

        inputResult.y += gravity * fallMultiplier * Time.deltaTime;
    }

    private void ClampVelocity()
    {
        if (Mathf.Abs(rb.velocity.x) > PlayerManager.GetMaxSpeed())
        {
            inputResult.x = moveAxis.x * PlayerManager.GetMaxSpeed();
        }
        if (Mathf.Abs(rb.velocity.z) > PlayerManager.GetMaxSpeed())
        {
            inputResult.z = moveAxis.y * PlayerManager.GetMaxSpeed();
        }
    }

    private void DirectionDamping()
    {
        if (Mathf.Sign(rb.velocity.x) != moveAxis.x)
        {
            inputResult.x = rb.velocity.x * directionDamping;
            inputResult.y = rb.velocity.y;
            rb.velocity = inputResult;
        }
        if (Mathf.Sign(rb.velocity.z) != moveAxis.y)
        {
            inputResult.z = rb.velocity.z * directionDamping;
            inputResult.y = rb.velocity.y;
            rb.velocity = inputResult;
        }
    }

    private void Dash(InputAction.CallbackContext context)
    {
        //Debug.Log("Dashed");
        if (state != State.Normal || IsGrounded() || currentDashCount == 0)
            return;

        currentDashCount--;
        StartCoroutine(DashState());
    }

    private IEnumerator DashState()
    {
        state = State.Dash;
        rb.velocity = Vector3.zero;
        Vector3 prevPos = transform.position;
        float progress = 0f;
        float timeToEnd = dashDistance / (PlayerManager.GetMaxSpeed() * dashMultiplier);
        //Debug.Log($"Time: {timeToEnd} Distance: {dashDistance} Speed: {PlayerManager.GetMaxSpeed() * dashMultiplier}");
        while (progress < 1f)
        {
            progress += Time.deltaTime / timeToEnd;
            rb.MovePosition(Vector3.Lerp(prevPos, prevPos + (transform.forward * dashDistance), progress));
            yield return null;
        }
        rb.velocity = Vector3.zero;
        state = State.Normal;
    }

    private void RefreshDash()
    {
        if (IsGrounded())
            currentDashCount = dashCount;
    }

    public void ModifyJumpCount(int amount) => jumpCount += amount;
    public void ModifyDashCount(int amount) => dashCount += amount;

    private void WallRun(InputAction.CallbackContext context)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + wcOffset, -transform.right, out hit, wcDist, wcMask))// && Vector3.Cross(-transform.right, hit.normal) == Vector3.zero)
        {
            Debug.Log($"Left Wall Result: {Vector3.Dot(Vector3.up, hit.normal)}");
            //Physics.Raycast(transform.position + wcOffset, transform.right, out hit, wcDist, wcMask)
            state = State.WallRun;
        }
        if (Physics.Raycast(transform.position + wcOffset, transform.right, out hit, wcDist, wcMask))
            Debug.Log($"Right Wall Result: {Vector3.Dot(Vector3.up, hit.normal)}");
    }

    private void StopWallRun(InputAction.CallbackContext context)
    {
        state = State.Normal;
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(transform.position + gcOffset, gcRadius, gcMask);
    }

    private void CheckSlope()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + slopeOffset, Vector3.down, out hit, slopeDist, slopeMask))
        {
            if (Vector3.Dot(Vector3.up, hit.normal) < slopeThreshold)
            {
                Debug.Log($"Slope Result: {Vector3.Dot(Vector3.up, hit.normal)} Force Direction: {Vector3.Cross(Vector3.right, hit.normal)}");
                otherResult += Vector3.Cross(Vector3.right, hit.normal) * slopeSpeed * Time.deltaTime;
                state = State.Slope;
            }
        }
        else if(state == State.Slope)
        {
            state = State.Normal;
            otherResult = Vector3.zero;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position + gcOffset, gcRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position + slopeOffset, transform.position + slopeOffset + (Vector3.down * slopeDist));
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position + wcOffset, transform.position + wcOffset + (-transform.right * wcDist));
    }

    private enum State
    {
        Normal,
        Dash,
        WallRun,
        Slope
    }
}
