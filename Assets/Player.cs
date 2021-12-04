// GENERATED AUTOMATICALLY FROM 'Assets/Player.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Player"",
    ""maps"": [
        {
            ""name"": ""Controls"",
            ""id"": ""264c51f1-aef7-407e-a3ca-a28b34180594"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""c4e8858e-acde-4226-9f29-43cc2ce61424"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rotate"",
                    ""type"": ""PassThrough"",
                    ""id"": ""f6f8395f-8ad8-4dca-a219-065a2d0c674d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""635195a1-31bd-412f-b119-09cd796e38f2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Escape"",
                    ""type"": ""Button"",
                    ""id"": ""5c3b9bad-f0cb-45af-a976-71644980a77a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""60c08b23-b7f8-4070-8d83-6f99dd73ceeb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Map"",
                    ""type"": ""Button"",
                    ""id"": ""338cd7d3-0524-40f6-b321-2566f3053ee8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8c312ba2-cc3c-43d1-8d44-567976b5830f"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Wasd"",
                    ""id"": ""8e4a50bf-9578-48a7-b47f-8c6977710746"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""6401e198-7b61-49bc-ae19-52a21f8488c1"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""262d448a-b7f4-49e9-b7da-e61220be345c"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""0dc99065-58e6-4b66-9f17-9453bca5b581"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""7a768c04-32f8-475b-9c95-1836fdda017e"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""033695ab-3f0d-4ea6-82d3-a26f817b54dc"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": ""ScaleVector2(x=15,y=15)"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""27ce9aa1-9152-431f-8339-d29c1f4d61bd"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""670ee678-e09f-433d-8a58-63d1d2bb79f3"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ea389f4f-9b16-4296-b6e3-1e32578f324a"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0714dc35-919a-42e8-86d1-9611fbfbe2db"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Escape"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bdda3a6f-e95f-4eac-8ea9-24cfc60a2da6"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Escape"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""da903ead-7d4d-4ae6-bbb7-2d114a1c1e84"",
                    ""path"": ""<Mouse>/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6fe3b626-8737-4546-8d0c-8d43fdf99129"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5a4943ae-a4c9-487c-ad6e-2b749f6285f5"",
                    ""path"": ""<Keyboard>/m"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Map"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Inspection"",
            ""id"": ""67b28ccf-6651-4f28-9a60-35ac8300a7b3"",
            ""actions"": [
                {
                    ""name"": ""RotateX"",
                    ""type"": ""PassThrough"",
                    ""id"": ""c13ab0b5-aadf-43ad-ad3c-ec5866eb0491"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotateY"",
                    ""type"": ""PassThrough"",
                    ""id"": ""9d10f1fe-0e72-4ecc-8975-5dd06e51b723"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Mouse"",
                    ""type"": ""Button"",
                    ""id"": ""153c1dc2-2d33-44d7-a0f6-af9e538505e9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(pressPoint=0.1,behavior=2)""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""bc0ad691-c75b-4b19-8efc-b431984547f0"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": ""ScaleVector2(x=15,y=15)"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""RotateX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""36f5052c-50a7-4ee5-b8b8-26a91aa567ce"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""RotateX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d5ae9be2-c6e4-4c5f-91d7-26a3d56f9cce"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": ""ScaleVector2(x=15,y=15)"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""RotateY"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""86f47496-f497-4f4a-afa3-4fe4d95b39bc"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""RotateY"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4a0b0117-39f0-4026-99b3-5acec5411231"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Mouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard and Mouse"",
            ""bindingGroup"": ""Keyboard and Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Joystick"",
            ""bindingGroup"": ""Joystick"",
            ""devices"": [
                {
                    ""devicePath"": ""<HID::DragonRise Inc.   Generic   USB  Joystick  >"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Controls
        m_Controls = asset.FindActionMap("Controls", throwIfNotFound: true);
        m_Controls_Move = m_Controls.FindAction("Move", throwIfNotFound: true);
        m_Controls_Rotate = m_Controls.FindAction("Rotate", throwIfNotFound: true);
        m_Controls_Crouch = m_Controls.FindAction("Crouch", throwIfNotFound: true);
        m_Controls_Escape = m_Controls.FindAction("Escape", throwIfNotFound: true);
        m_Controls_Interact = m_Controls.FindAction("Interact", throwIfNotFound: true);
        m_Controls_Map = m_Controls.FindAction("Map", throwIfNotFound: true);
        // Inspection
        m_Inspection = asset.FindActionMap("Inspection", throwIfNotFound: true);
        m_Inspection_RotateX = m_Inspection.FindAction("RotateX", throwIfNotFound: true);
        m_Inspection_RotateY = m_Inspection.FindAction("RotateY", throwIfNotFound: true);
        m_Inspection_Mouse = m_Inspection.FindAction("Mouse", throwIfNotFound: true);
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

    // Controls
    private readonly InputActionMap m_Controls;
    private IControlsActions m_ControlsActionsCallbackInterface;
    private readonly InputAction m_Controls_Move;
    private readonly InputAction m_Controls_Rotate;
    private readonly InputAction m_Controls_Crouch;
    private readonly InputAction m_Controls_Escape;
    private readonly InputAction m_Controls_Interact;
    private readonly InputAction m_Controls_Map;
    public struct ControlsActions
    {
        private @PlayerControls m_Wrapper;
        public ControlsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Controls_Move;
        public InputAction @Rotate => m_Wrapper.m_Controls_Rotate;
        public InputAction @Crouch => m_Wrapper.m_Controls_Crouch;
        public InputAction @Escape => m_Wrapper.m_Controls_Escape;
        public InputAction @Interact => m_Wrapper.m_Controls_Interact;
        public InputAction @Map => m_Wrapper.m_Controls_Map;
        public InputActionMap Get() { return m_Wrapper.m_Controls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ControlsActions set) { return set.Get(); }
        public void SetCallbacks(IControlsActions instance)
        {
            if (m_Wrapper.m_ControlsActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnMove;
                @Rotate.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnRotate;
                @Rotate.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnRotate;
                @Rotate.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnRotate;
                @Crouch.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnCrouch;
                @Crouch.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnCrouch;
                @Crouch.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnCrouch;
                @Escape.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnEscape;
                @Escape.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnEscape;
                @Escape.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnEscape;
                @Interact.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnInteract;
                @Map.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnMap;
                @Map.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnMap;
                @Map.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnMap;
            }
            m_Wrapper.m_ControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Rotate.started += instance.OnRotate;
                @Rotate.performed += instance.OnRotate;
                @Rotate.canceled += instance.OnRotate;
                @Crouch.started += instance.OnCrouch;
                @Crouch.performed += instance.OnCrouch;
                @Crouch.canceled += instance.OnCrouch;
                @Escape.started += instance.OnEscape;
                @Escape.performed += instance.OnEscape;
                @Escape.canceled += instance.OnEscape;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Map.started += instance.OnMap;
                @Map.performed += instance.OnMap;
                @Map.canceled += instance.OnMap;
            }
        }
    }
    public ControlsActions @Controls => new ControlsActions(this);

    // Inspection
    private readonly InputActionMap m_Inspection;
    private IInspectionActions m_InspectionActionsCallbackInterface;
    private readonly InputAction m_Inspection_RotateX;
    private readonly InputAction m_Inspection_RotateY;
    private readonly InputAction m_Inspection_Mouse;
    public struct InspectionActions
    {
        private @PlayerControls m_Wrapper;
        public InspectionActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @RotateX => m_Wrapper.m_Inspection_RotateX;
        public InputAction @RotateY => m_Wrapper.m_Inspection_RotateY;
        public InputAction @Mouse => m_Wrapper.m_Inspection_Mouse;
        public InputActionMap Get() { return m_Wrapper.m_Inspection; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InspectionActions set) { return set.Get(); }
        public void SetCallbacks(IInspectionActions instance)
        {
            if (m_Wrapper.m_InspectionActionsCallbackInterface != null)
            {
                @RotateX.started -= m_Wrapper.m_InspectionActionsCallbackInterface.OnRotateX;
                @RotateX.performed -= m_Wrapper.m_InspectionActionsCallbackInterface.OnRotateX;
                @RotateX.canceled -= m_Wrapper.m_InspectionActionsCallbackInterface.OnRotateX;
                @RotateY.started -= m_Wrapper.m_InspectionActionsCallbackInterface.OnRotateY;
                @RotateY.performed -= m_Wrapper.m_InspectionActionsCallbackInterface.OnRotateY;
                @RotateY.canceled -= m_Wrapper.m_InspectionActionsCallbackInterface.OnRotateY;
                @Mouse.started -= m_Wrapper.m_InspectionActionsCallbackInterface.OnMouse;
                @Mouse.performed -= m_Wrapper.m_InspectionActionsCallbackInterface.OnMouse;
                @Mouse.canceled -= m_Wrapper.m_InspectionActionsCallbackInterface.OnMouse;
            }
            m_Wrapper.m_InspectionActionsCallbackInterface = instance;
            if (instance != null)
            {
                @RotateX.started += instance.OnRotateX;
                @RotateX.performed += instance.OnRotateX;
                @RotateX.canceled += instance.OnRotateX;
                @RotateY.started += instance.OnRotateY;
                @RotateY.performed += instance.OnRotateY;
                @RotateY.canceled += instance.OnRotateY;
                @Mouse.started += instance.OnMouse;
                @Mouse.performed += instance.OnMouse;
                @Mouse.canceled += instance.OnMouse;
            }
        }
    }
    public InspectionActions @Inspection => new InspectionActions(this);
    private int m_KeyboardandMouseSchemeIndex = -1;
    public InputControlScheme KeyboardandMouseScheme
    {
        get
        {
            if (m_KeyboardandMouseSchemeIndex == -1) m_KeyboardandMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard and Mouse");
            return asset.controlSchemes[m_KeyboardandMouseSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_JoystickSchemeIndex = -1;
    public InputControlScheme JoystickScheme
    {
        get
        {
            if (m_JoystickSchemeIndex == -1) m_JoystickSchemeIndex = asset.FindControlSchemeIndex("Joystick");
            return asset.controlSchemes[m_JoystickSchemeIndex];
        }
    }
    public interface IControlsActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnRotate(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
        void OnEscape(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnMap(InputAction.CallbackContext context);
    }
    public interface IInspectionActions
    {
        void OnRotateX(InputAction.CallbackContext context);
        void OnRotateY(InputAction.CallbackContext context);
        void OnMouse(InputAction.CallbackContext context);
    }
}
