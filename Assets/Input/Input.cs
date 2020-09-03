// GENERATED AUTOMATICALLY FROM 'Assets/Input/Input.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Input : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Input()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Input"",
    ""maps"": [
        {
            ""name"": ""PlayMode"",
            ""id"": ""b6405f91-4b7b-4555-b505-97373a78fb61"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""fe7377c6-d2bf-4d81-a58a-139cc98007df"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Roll"",
                    ""type"": ""Button"",
                    ""id"": ""66125524-fc07-4851-a66c-e492522a28f6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ShiftLeft"",
                    ""type"": ""Button"",
                    ""id"": ""934e4f49-6a5f-4e83-b770-54abba8dc8a0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ShiftRight"",
                    ""type"": ""Button"",
                    ""id"": ""d2ae1d54-9762-4019-b5f3-5bab0afa99b5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Test_IncreaseDifficulty"",
                    ""type"": ""Button"",
                    ""id"": ""df83f16e-5272-441a-b5d3-e4454d21413e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5e73a8f3-a855-481e-9281-482b80103ac0"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "" PC"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7f2f3896-444c-4e5a-9632-ecb7e7e4cb2e"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "" PC"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""45b490d7-fd92-499d-932a-d5bd4d05a957"",
                    ""path"": ""<Sensor>"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""40a43120-a836-49dc-9a40-6f29df915c02"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "" PC"",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""21be45ca-d901-459a-90c2-7a85c468e3a4"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "" PC"",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ee87f229-4ee6-46a9-95ec-73a2b493d60f"",
                    ""path"": ""<Sensor>"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""75b73dac-8020-4985-af3d-26dd75d6a35f"",
                    ""path"": ""<Sensor>"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""ShiftLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""611ff469-ef4f-4477-8dca-02941db50ddf"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "" PC"",
                    ""action"": ""ShiftLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6a73f45f-4156-4e41-944c-bb71c0898a0f"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "" PC"",
                    ""action"": ""ShiftLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3d3d2711-1e02-4c91-bc1a-a64e1368c6ef"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "" PC"",
                    ""action"": ""ShiftRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ae55388f-0c39-40ec-bc67-4b68cf492d09"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "" PC"",
                    ""action"": ""ShiftRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d5ed9063-5942-4e9d-90df-493e0a19fc5b"",
                    ""path"": ""<Sensor>"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""ShiftRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c0a8937d-65ee-43f7-a05c-fe7b40daeb5a"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "" PC"",
                    ""action"": ""Test_IncreaseDifficulty"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": "" PC"",
            ""bindingGroup"": "" PC"",
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
            ""name"": ""Mobile"",
            ""bindingGroup"": ""Mobile"",
            ""devices"": [
                {
                    ""devicePath"": ""<Sensor>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // PlayMode
        m_PlayMode = asset.FindActionMap("PlayMode", throwIfNotFound: true);
        m_PlayMode_Jump = m_PlayMode.FindAction("Jump", throwIfNotFound: true);
        m_PlayMode_Roll = m_PlayMode.FindAction("Roll", throwIfNotFound: true);
        m_PlayMode_ShiftLeft = m_PlayMode.FindAction("ShiftLeft", throwIfNotFound: true);
        m_PlayMode_ShiftRight = m_PlayMode.FindAction("ShiftRight", throwIfNotFound: true);
        m_PlayMode_Test_IncreaseDifficulty = m_PlayMode.FindAction("Test_IncreaseDifficulty", throwIfNotFound: true);
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

    // PlayMode
    private readonly InputActionMap m_PlayMode;
    private IPlayModeActions m_PlayModeActionsCallbackInterface;
    private readonly InputAction m_PlayMode_Jump;
    private readonly InputAction m_PlayMode_Roll;
    private readonly InputAction m_PlayMode_ShiftLeft;
    private readonly InputAction m_PlayMode_ShiftRight;
    private readonly InputAction m_PlayMode_Test_IncreaseDifficulty;
    public struct PlayModeActions
    {
        private @Input m_Wrapper;
        public PlayModeActions(@Input wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_PlayMode_Jump;
        public InputAction @Roll => m_Wrapper.m_PlayMode_Roll;
        public InputAction @ShiftLeft => m_Wrapper.m_PlayMode_ShiftLeft;
        public InputAction @ShiftRight => m_Wrapper.m_PlayMode_ShiftRight;
        public InputAction @Test_IncreaseDifficulty => m_Wrapper.m_PlayMode_Test_IncreaseDifficulty;
        public InputActionMap Get() { return m_Wrapper.m_PlayMode; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayModeActions set) { return set.Get(); }
        public void SetCallbacks(IPlayModeActions instance)
        {
            if (m_Wrapper.m_PlayModeActionsCallbackInterface != null)
            {
                @Jump.started -= m_Wrapper.m_PlayModeActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayModeActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayModeActionsCallbackInterface.OnJump;
                @Roll.started -= m_Wrapper.m_PlayModeActionsCallbackInterface.OnRoll;
                @Roll.performed -= m_Wrapper.m_PlayModeActionsCallbackInterface.OnRoll;
                @Roll.canceled -= m_Wrapper.m_PlayModeActionsCallbackInterface.OnRoll;
                @ShiftLeft.started -= m_Wrapper.m_PlayModeActionsCallbackInterface.OnShiftLeft;
                @ShiftLeft.performed -= m_Wrapper.m_PlayModeActionsCallbackInterface.OnShiftLeft;
                @ShiftLeft.canceled -= m_Wrapper.m_PlayModeActionsCallbackInterface.OnShiftLeft;
                @ShiftRight.started -= m_Wrapper.m_PlayModeActionsCallbackInterface.OnShiftRight;
                @ShiftRight.performed -= m_Wrapper.m_PlayModeActionsCallbackInterface.OnShiftRight;
                @ShiftRight.canceled -= m_Wrapper.m_PlayModeActionsCallbackInterface.OnShiftRight;
                @Test_IncreaseDifficulty.started -= m_Wrapper.m_PlayModeActionsCallbackInterface.OnTest_IncreaseDifficulty;
                @Test_IncreaseDifficulty.performed -= m_Wrapper.m_PlayModeActionsCallbackInterface.OnTest_IncreaseDifficulty;
                @Test_IncreaseDifficulty.canceled -= m_Wrapper.m_PlayModeActionsCallbackInterface.OnTest_IncreaseDifficulty;
            }
            m_Wrapper.m_PlayModeActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Roll.started += instance.OnRoll;
                @Roll.performed += instance.OnRoll;
                @Roll.canceled += instance.OnRoll;
                @ShiftLeft.started += instance.OnShiftLeft;
                @ShiftLeft.performed += instance.OnShiftLeft;
                @ShiftLeft.canceled += instance.OnShiftLeft;
                @ShiftRight.started += instance.OnShiftRight;
                @ShiftRight.performed += instance.OnShiftRight;
                @ShiftRight.canceled += instance.OnShiftRight;
                @Test_IncreaseDifficulty.started += instance.OnTest_IncreaseDifficulty;
                @Test_IncreaseDifficulty.performed += instance.OnTest_IncreaseDifficulty;
                @Test_IncreaseDifficulty.canceled += instance.OnTest_IncreaseDifficulty;
            }
        }
    }
    public PlayModeActions @PlayMode => new PlayModeActions(this);
    private int m_PCSchemeIndex = -1;
    public InputControlScheme PCScheme
    {
        get
        {
            if (m_PCSchemeIndex == -1) m_PCSchemeIndex = asset.FindControlSchemeIndex(" PC");
            return asset.controlSchemes[m_PCSchemeIndex];
        }
    }
    private int m_MobileSchemeIndex = -1;
    public InputControlScheme MobileScheme
    {
        get
        {
            if (m_MobileSchemeIndex == -1) m_MobileSchemeIndex = asset.FindControlSchemeIndex("Mobile");
            return asset.controlSchemes[m_MobileSchemeIndex];
        }
    }
    public interface IPlayModeActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnRoll(InputAction.CallbackContext context);
        void OnShiftLeft(InputAction.CallbackContext context);
        void OnShiftRight(InputAction.CallbackContext context);
        void OnTest_IncreaseDifficulty(InputAction.CallbackContext context);
    }
}
