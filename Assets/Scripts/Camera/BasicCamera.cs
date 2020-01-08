using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

[RequireComponent (typeof(Camera))]
public class BasicCamera : MonoBehaviour
{
    [SerializeField]
    private Transform controlledBody = null; //Transform affected by this script. 
    [SerializeField]
    private Vector3 cbOffset = Vector3.zero;
    
    [SerializeField]
    private Quaternion InitialRotation; //Default rotation
    [SerializeField]
    private float TimeBetweenReset = 0.5f;//Time before the camera returns to default position
    [SerializeField]
    private float CameraSpeed = 1.0f;//Mouse movement constant scale factor
    [SerializeField]
    private Vector3 MaxRotation = new Vector3(50, 80, 0);//Clamp rotaiton
    [SerializeField]
    private float TimeToReachDestination = 0.05f; //Time that takes to reach destination

    private float ResetTimer = 0.0f;//Timer to reset the camera
    private float Timer = 0.0f;//Timer to Calculate lerp positions. 

    private PlayerControls playerControls;//Input
    private Quaternion DesiredRotation; //Rotation to be achieved by camera. 

    // Start is called before the first frame update
    void Start()
    {
        playerControls = new PlayerControls();
        playerControls.Camera.Camera_Movement.started += Move;
        playerControls.Camera.Camera_Movement.Enable();

        InitialRotation = transform.rotation;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.rotation.eulerAngles); 
        if (Timer <= TimeToReachDestination && DesiredRotation != InitialRotation)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, DesiredRotation, Timer / TimeToReachDestination); 
            Timer += Time.deltaTime; 
        }
        else if (ResetTimer < TimeBetweenReset)
        {
            ResetTimer += Time.deltaTime; 
        }
        else if(DesiredRotation != InitialRotation)
        {
            DesiredRotation = InitialRotation;
            Timer = 0.0f; 
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, DesiredRotation, Timer / (TimeToReachDestination * 3.0f));
            Timer += Time.deltaTime;
        }

        controlledBody.localEulerAngles = Vector3.up * transform.localEulerAngles.y;
        transform.position = controlledBody.position + cbOffset;
    }

    /*Calculates the rotation where should the camera look at*/
    public void Move(InputAction.CallbackContext DeltaX)
    {

        Vector2 rotation = DeltaX.ReadValue<Vector2>() * CameraSpeed * new Vector2(-1,1); ;
        Vector3 CurrentRotation = transform.rotation.eulerAngles;

        CurrentRotation = ClampRotation(transform.rotation.eulerAngles + new Vector3(rotation.y,rotation.x, 0));

        DesiredRotation = Quaternion.Euler(CurrentRotation.x, CurrentRotation.y, 0);

        ResetTimer = 0.0f; Timer = TimeToReachDestination * 0.1f; ;
    }
    /*Clamps rotation to a maxium degree*/
    private Vector3 ClampRotation(Vector3 Rotation)
    {

        Vector3 result = Rotation;

        if (Rotation.x > (360 - MaxRotation.x) || Rotation.x < MaxRotation.x) { }
        else
        {
            result.x = ((360 - MaxRotation.x - Rotation.x) < (Rotation.x - MaxRotation.x)) ? 360 - MaxRotation.x : MaxRotation.x;   
        }

        if (Rotation.y > (360 - MaxRotation.y) || Rotation.y < MaxRotation.y) { }
        else
        {
            result.y = ((360 - MaxRotation.y - Rotation.y) < (Rotation.y - MaxRotation.y)) ? 360 - MaxRotation.y : MaxRotation.y;
        }


        result.z = 0; 
        return result; 

    }
}
