// GENERATED AUTOMATICALLY FROM 'Assets/InputActions/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    private InputActionAsset asset;
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Movement"",
            ""id"": ""0dd07543-39f0-4545-8939-fc99e358562a"",
            ""actions"": [
                {
                    ""name"": ""Walk"",
                    ""type"": ""Value"",
                    ""id"": ""b2421e6c-7326-4940-adb9-a02ed28522df"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""82a2c7dc-63ad-46d6-b9e0-a3a81a780b61"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""e1bc8bef-3f3f-4c2d-a8b0-bf87f705e242"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Wall Run"",
                    ""type"": ""Button"",
                    ""id"": ""524bdbf0-e73f-44dc-834c-a331267b70d1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CancelWallRun"",
                    ""type"": ""Button"",
                    ""id"": ""78b96e8d-df31-4ddf-bbf6-b6ce4b73243e"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SlowMotion"",
                    ""type"": ""Button"",
                    ""id"": ""e6275030-32de-4356-834d-a22e1e3ad4d3"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CancelSlowMotion"",
                    ""type"": ""Button"",
                    ""id"": ""c86d0733-9467-41fd-b530-a2377c07b938"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Move"",
                    ""id"": ""abb739aa-5356-4031-9153-2d7fa9ccae89"",
                    ""path"": ""2DVector(normalize=false)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Walk"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""dc872e17-d2ea-4b0f-a601-dcfa50ed503c"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Movement"",
                    ""action"": ""Walk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""506485a2-19b8-4af4-af38-cb4339140b5b"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Movement"",
                    ""action"": ""Walk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""a3fce478-e2d4-45b8-bf96-061c74973d8c"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Movement"",
                    ""action"": ""Walk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""885d5a9f-744f-4580-9ff5-b89ee1b68fa2"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Movement"",
                    ""action"": ""Walk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""fe37ccb3-fc8e-4bca-a616-90001aaee229"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Movement"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aac191fe-4cf6-4dc0-9c86-840b5827b1d6"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Movement"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ced284b4-a54f-4c1b-be2d-fcf791141a41"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Hold(pressPoint=0.1)"",
                    ""processors"": """",
                    ""groups"": ""Movement"",
                    ""action"": ""Wall Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""39d7f8e1-5486-4153-9d3f-fc1a4bf3b012"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Movement"",
                    ""action"": ""CancelWallRun"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""68614795-7264-40bf-ae05-330786f2a951"",
                    ""path"": ""<Mouse>/press"",
                    ""interactions"": ""Hold(pressPoint=0.1)"",
                    ""processors"": """",
                    ""groups"": ""Movement"",
                    ""action"": ""SlowMotion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6c1389cd-24be-4709-952f-a7e8c4198f9d"",
                    ""path"": ""<Mouse>/press"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Movement"",
                    ""action"": ""CancelSlowMotion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Camera"",
            ""id"": ""283342b7-3c92-4ae3-8fd0-b4a53a593552"",
            ""actions"": [
                {
                    ""name"": ""Camera_Movement"",
                    ""type"": ""Value"",
                    ""id"": ""1020abeb-5b40-461b-8368-b6bcb6583240"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6efc22da-1954-4ee8-8838-8c72c269f5ff"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": ""InvertVector2"",
                    ""groups"": ""Movement"",
                    ""action"": ""Camera_Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Movement"",
            ""bindingGroup"": ""Movement"",
            ""devices"": []
        }
    ]
}");
        // Movement
        m_Movement = asset.FindActionMap("Movement", throwIfNotFound: true);
        m_Movement_Walk = m_Movement.FindAction("Walk", throwIfNotFound: true);
        m_Movement_Jump = m_Movement.FindAction("Jump", throwIfNotFound: true);
        m_Movement_Dash = m_Movement.FindAction("Dash", throwIfNotFound: true);
        m_Movement_WallRun = m_Movement.FindAction("Wall Run", throwIfNotFound: true);
        m_Movement_CancelWallRun = m_Movement.FindAction("CancelWallRun", throwIfNotFound: true);
        m_Movement_SlowMotion = m_Movement.FindAction("SlowMotion", throwIfNotFound: true);
        m_Movement_CancelSlowMotion = m_Movement.FindAction("CancelSlowMotion", throwIfNotFound: true);
        // Camera
        m_Camera = asset.FindActionMap("Camera", throwIfNotFound: true);
        m_Camera_Camera_Movement = m_Camera.FindAction("Camera_Movement", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Movement
    private readonly InputActionMap m_Movement;
    private IMovementActions m_MovementActionsCallbackInterface;
    private readonly InputAction m_Movement_Walk;
    private readonly InputAction m_Movement_Jump;
    private readonly InputAction m_Movement_Dash;
    private readonly InputAction m_Movement_WallRun;
    private readonly InputAction m_Movement_CancelWallRun;
    private readonly InputAction m_Movement_SlowMotion;
    private readonly InputAction m_Movement_CancelSlowMotion;
    public struct MovementActions
    {
        private @PlayerControls m_Wrapper;
        public MovementActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Walk => m_Wrapper.m_Movement_Walk;
        public InputAction @Jump => m_Wrapper.m_Movement_Jump;
        public InputAction @Dash => m_Wrapper.m_Movement_Dash;
        public InputAction @WallRun => m_Wrapper.m_Movement_WallRun;
        public InputAction @CancelWallRun => m_Wrapper.m_Movement_CancelWallRun;
        public InputAction @SlowMotion => m_Wrapper.m_Movement_SlowMotion;
        public InputAction @CancelSlowMotion => m_Wrapper.m_Movement_CancelSlowMotion;
        public InputActionMap Get() { return m_Wrapper.m_Movement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MovementActions set) { return set.Get(); }
        public void SetCallbacks(IMovementActions instance)
        {
            if (m_Wrapper.m_MovementActionsCallbackInterface != null)
            {
                @Walk.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnWalk;
                @Walk.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnWalk;
                @Walk.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnWalk;
                @Jump.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnJump;
                @Dash.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnDash;
                @WallRun.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnWallRun;
                @WallRun.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnWallRun;
                @WallRun.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnWallRun;
                @CancelWallRun.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnCancelWallRun;
                @CancelWallRun.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnCancelWallRun;
                @CancelWallRun.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnCancelWallRun;
                @SlowMotion.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnSlowMotion;
                @SlowMotion.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnSlowMotion;
                @SlowMotion.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnSlowMotion;
                @CancelSlowMotion.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnCancelSlowMotion;
                @CancelSlowMotion.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnCancelSlowMotion;
                @CancelSlowMotion.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnCancelSlowMotion;
            }
            m_Wrapper.m_MovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Walk.started += instance.OnWalk;
                @Walk.performed += instance.OnWalk;
                @Walk.canceled += instance.OnWalk;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @WallRun.started += instance.OnWallRun;
                @WallRun.performed += instance.OnWallRun;
                @WallRun.canceled += instance.OnWallRun;
                @CancelWallRun.started += instance.OnCancelWallRun;
                @CancelWallRun.performed += instance.OnCancelWallRun;
                @CancelWallRun.canceled += instance.OnCancelWallRun;
                @SlowMotion.started += instance.OnSlowMotion;
                @SlowMotion.performed += instance.OnSlowMotion;
                @SlowMotion.canceled += instance.OnSlowMotion;
                @CancelSlowMotion.started += instance.OnCancelSlowMotion;
                @CancelSlowMotion.performed += instance.OnCancelSlowMotion;
                @CancelSlowMotion.canceled += instance.OnCancelSlowMotion;
            }
        }
    }
    public MovementActions @Movement => new MovementActions(this);

    // Camera
    private readonly InputActionMap m_Camera;
    private ICameraActions m_CameraActionsCallbackInterface;
    private readonly InputAction m_Camera_Camera_Movement;
    public struct CameraActions
    {
        private @PlayerControls m_Wrapper;
        public CameraActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Camera_Movement => m_Wrapper.m_Camera_Camera_Movement;
        public InputActionMap Get() { return m_Wrapper.m_Camera; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CameraActions set) { return set.Get(); }
        public void SetCallbacks(ICameraActions instance)
        {
            if (m_Wrapper.m_CameraActionsCallbackInterface != null)
            {
                @Camera_Movement.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnCamera_Movement;
                @Camera_Movement.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnCamera_Movement;
                @Camera_Movement.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnCamera_Movement;
            }
            m_Wrapper.m_CameraActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Camera_Movement.started += instance.OnCamera_Movement;
                @Camera_Movement.performed += instance.OnCamera_Movement;
                @Camera_Movement.canceled += instance.OnCamera_Movement;
            }
        }
    }
    public CameraActions @Camera => new CameraActions(this);
    private int m_MovementSchemeIndex = -1;
    public InputControlScheme MovementScheme
    {
        get
        {
            if (m_MovementSchemeIndex == -1) m_MovementSchemeIndex = asset.FindControlSchemeIndex("Movement");
            return asset.controlSchemes[m_MovementSchemeIndex];
        }
    }
    public interface IMovementActions
    {
        void OnWalk(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnWallRun(InputAction.CallbackContext context);
        void OnCancelWallRun(InputAction.CallbackContext context);
        void OnSlowMotion(InputAction.CallbackContext context);
        void OnCancelSlowMotion(InputAction.CallbackContext context);
    }
    public interface ICameraActions
    {
        void OnCamera_Movement(InputAction.CallbackContext context);
    }
}
