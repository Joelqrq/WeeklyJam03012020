using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float timeToMaxSpeed = 0.2f;
    [SerializeField] private float fallMultiplier = 1.2f;
    [SerializeField, Range(0f, 1f)] private float directionDamping = 0.2f;
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
    [SerializeField, Range(0f, 1f)] private float slopeThreshold = 0.98f;
    [SerializeField] private Vector3 slopeOffset = Vector3.zero;
    [SerializeField] private float slopeDist = 0.5f;
    [SerializeField] private LayerMask slopeMask = 0;
    [Header("Wall Run Setting")]
    [SerializeField] private float wrMultiplier = 1.2f;
    [SerializeField, Range(0f, 1f)] private float positiveThreshold = 0.1f;
    [SerializeField, Range(-0f, -1f)] private float negativeThreshold = -0.1f;
    [SerializeField] private Vector3 wrOffset = Vector3.zero;
    [SerializeField] private float wrDist = 1f;
    [SerializeField] private LayerMask wrMask = 0;

    public Action<PlayerController> timeEvent;

    private Rigidbody rb;
    private PlayerControls playerControls;
    public PlayerControls PlayerControls => playerControls;

    private Vector3 inputResult;
    private Vector2 moveAxis;
    private float speedProgress;
    private float currentSpeed;

    //Jump
    private int jumpCount = 2;
    private int currentJumpCount;

    private float gravityResult;
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
        playerControls.Movement.CancelWallRun.performed += StopWallRun;
        playerControls.Enable();
    }

    private void FixedUpdate()
    {
        RefreshJump();
        RefreshDash();
        CheckSlope();
        HandleMove();
    }

    private void HandleMove()
    {
        switch (state)
        {
            case State.Normal:
                CalculateStoppingSpeed();
                if (moveAxis.sqrMagnitude > 0f)
                {
                    if (timeToMaxSpeed < 0.1f)
                    {
                        speedProgress += Time.deltaTime / timeToMaxSpeed;
                        currentSpeed = Mathf.Lerp(0f, PlayerManager.GetMaxSpeed(), speedProgress);
                    }
                    else
                    {
                        currentSpeed = PlayerManager.GetMaxSpeed();
                    }
                    inputResult = ((transform.right * moveAxis.x) + (transform.forward * moveAxis.y)) * currentSpeed;
                }
                break;

            case State.Dash:

                break;

            case State.Slope:
                if (moveAxis.sqrMagnitude > 0f)
                {
                    if (timeToMaxSpeed < 0.1f)
                    {
                        speedProgress += Time.deltaTime / timeToMaxSpeed;
                        currentSpeed = Mathf.Lerp(0f, PlayerManager.GetMaxSpeed(), speedProgress);
                    }
                    else
                    {
                        currentSpeed = PlayerManager.GetMaxSpeed();
                    }
                    inputResult = (transform.right * moveAxis.x) * currentSpeed;
                }
                break;

            case State.WallRun:
                HandleWallRun();
                break;

            default:
                break;
        }

        Gravity();
        ClampInputVelocity();
        rb.AddForce(inputResult + (Vector3.up * gravityResult) + otherResult);
        //Debug.Log($"Input Result: {inputResult} Gravity Result: {gravityResult} Other Result: {otherResult}");
        gravityResult = 0f;
        otherResult = Vector3.zero;
    }

    private void Move(InputAction.CallbackContext context)
    {
        moveAxis = context.ReadValue<Vector2>();
        //Debug.Log($"Direction: {moveAxis}");
    }

    private void Halt(InputAction.CallbackContext context)
    {
        if (state == State.Normal || state == State.Slope)
        {
            moveAxis = Vector2.zero;
            inputResult = Vector3.zero;
            currentSpeed = 0f;
            speedProgress = 0f;
        }
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (state == State.Normal || state == State.Slope)
        {
            if (currentJumpCount > 0)
            {
                rb.AddForce(MathLibrary.CalculateJump(transform.up, rb.velocity), ForceMode.Impulse);
                currentJumpCount--;
            }
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

        gravityResult += gravity * fallMultiplier;
    }

    private void ClampInputVelocity()
    {
        if (Mathf.Abs(rb.velocity.x) > PlayerManager.GetMaxSpeed())
        {
            //Debug.Log($"Horizontal clamped diff: {rb.velocity.x}");
            inputResult = transform.right * 0f;
        }
        if (Mathf.Abs(rb.velocity.z) > PlayerManager.GetMaxSpeed())
        {
            //Debug.Log($"Forward clamped diff: {rb.velocity.z}");
            inputResult = transform.forward * 0f;
        }
    }

    private void CalculateStoppingSpeed()
    {
        if (IsGrounded())
        {
            if (moveAxis.x > -0.1f && moveAxis.x < 0.1f)
            {
                Vector3 stopVector = new Vector3();
                float rightSpeed = Vector3.Dot(rb.velocity, transform.right);
                stopVector += rightSpeed * -transform.right * rb.velocity.magnitude;
                rb.AddForce(stopVector);
            }
            if (moveAxis.y > -0.1f && moveAxis.y < 0.1f)
            {
                Vector3 stopVector = new Vector3();
                float fwdSpeed = Vector3.Dot(rb.velocity, transform.forward);
                stopVector += fwdSpeed * -transform.forward * rb.velocity.magnitude;
                rb.AddForce(stopVector);
            }
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
    public void ModifyJumpSpeed(float multiplier) => PlayerManager.IncreaseMaxJump(PlayerManager.GetMaxJump() * multiplier);
    public void ModifyDashDist(float multiplier) => dashDistance *= multiplier;

    private void WallRun(InputAction.CallbackContext context)
    {
        CheckWallForRun();
    }

    private void HandleWallRun()
    {
        //Change to MovePosition
        otherResult += transform.forward * PlayerManager.GetMaxSpeed() * wrMultiplier;
    }

    private void StopWallRun(InputAction.CallbackContext context)
    {
        if (state != State.WallRun)
            return;

        state = State.Normal;
        Debug.Log($"WallRun to Normal: {state}");
    }

    private void CheckWallForRun()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + wrOffset, -transform.right, out hit, wrDist, wrMask))
        {
            Debug.Log($"Left Wall Result: {Vector3.Dot(Vector3.up, hit.normal)} Target's Normal: {hit.normal}");
        }
        else if (Physics.Raycast(transform.position + wrOffset, transform.right, out hit, wrDist, wrMask))
        {
            Debug.Log($"Right Wall Result: {Vector3.Dot(Vector3.up, hit.normal)} Target's Normal: {hit.normal}");
        }

        if (hit.collider == null)
            return;

        float threshold = Vector3.Dot(Vector3.up, hit.normal);
        if (threshold > 0f && threshold < positiveThreshold || threshold < 0f && threshold > negativeThreshold)
        {
            rb.velocity = Vector3.zero;
            state = State.WallRun;
            Debug.Log(Vector3.Angle(hit.normal, transform.forward));
        }
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
                //Debug.Log($"Slope Dot: {Vector3.Dot(Vector3.up, hit.normal)} Slope direction: {Vector3.ProjectOnPlane(transform.forward, hit.normal)} Slope direction(2): {Vector3.Dot(Vector3.ProjectOnPlane(transform.forward, hit.normal), hit.normal)}");
            if (Vector3.Dot(Vector3.up, hit.normal) < slopeThreshold)
            {
                //Debug.Log($"Slope Result: {Vector3.Dot(Vector3.up, hit.normal)} Force Direction: {Vector3.Cross(Vector3.right, hit.normal)}");
                Vector3 direction = transform.forward;
                if (Vector3.Dot(Vector3.ProjectOnPlane(transform.forward, hit.normal), hit.normal) < -0.1f)
                {
                    direction = -transform.forward;
                }
                otherResult += Vector3.ProjectOnPlane(direction, hit.normal) * slopeSpeed;
                otherResult.y = 0f;
                state = State.Slope;
            }
            else if (state == State.Slope)
            {
                state = State.Normal;
                otherResult = Vector3.zero;
            }
        }
        else if (state == State.Slope)
        {
            state = State.Normal;
            otherResult = Vector3.zero;
        }
    }

    private void OnDestroy()
    {
        playerControls.Movement.Walk.performed += Move;
        playerControls.Movement.Walk.canceled += Halt;
        playerControls.Movement.Jump.performed += Jump;
        playerControls.Movement.Dash.performed += Dash;
        playerControls.Movement.WallRun.performed += WallRun;
        timeEvent?.Invoke(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position + gcOffset, gcRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position + slopeOffset, transform.position + slopeOffset + (Vector3.down * slopeDist));
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position + wrOffset, transform.position + wrOffset + (-transform.right * wrDist));
    }

    private enum State
    {
        Normal,
        Dash,
        WallRun,
        Slope
    }
}
