// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Input Assets/PlayerControlScheme.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControlScheme : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControlScheme()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControlScheme"",
    ""maps"": [
        {
            ""name"": ""PlayerControls"",
            ""id"": ""e64b6d5f-d0bf-47a1-8d15-f6631a45b62b"",
            ""actions"": [
                {
                    ""name"": ""primaryButton"",
                    ""type"": ""Button"",
                    ""id"": ""b30e47cb-b6b7-45ef-9f24-3ca57caaf267"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""secondaryButton"",
                    ""type"": ""Button"",
                    ""id"": ""5caf8e34-d263-482c-bbb0-a7092d0abd35"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""tertiaryButton"",
                    ""type"": ""Button"",
                    ""id"": ""626d1c68-e3e0-46a5-ab4f-703fe7ea2368"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""utilityButton"",
                    ""type"": ""Button"",
                    ""id"": ""70d5d00f-302e-499c-a2c7-e7d12ae23992"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""movementAxis"",
                    ""type"": ""Value"",
                    ""id"": ""d786c7b4-b325-4fc6-be87-f5978d70229c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""lookAxis"",
                    ""type"": ""Value"",
                    ""id"": ""278de513-46b7-417c-8146-a581f8ab393c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""startButton"",
                    ""type"": ""Button"",
                    ""id"": ""23a9ec10-a774-4d1c-95c8-d6058401157d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""selectButton"",
                    ""type"": ""Button"",
                    ""id"": ""d8af0ca3-c31f-4e7a-b82e-bf89f2c4a7c8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""85ae8fad-2fd8-4fae-ac60-cf9fa2da9079"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""secondaryButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b2645e19-cb35-40ef-9910-8d1cb38742c3"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""secondaryButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""50ea2265-206b-4aec-a4ca-2c5bb829a337"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""tertiaryButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0dc2655c-5be9-440a-aaaf-4828be0b1cec"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""tertiaryButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c6df6acb-8dbf-477d-9cbc-d18a19345cbd"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""utilityButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7d2f3b46-0542-4595-809b-c6595660eca0"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""utilityButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""63854860-5cf3-4121-bb22-51652292f895"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""movementAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""wasd"",
                    ""id"": ""48d90155-05e1-4241-aff5-ca07ab7dfc9b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""movementAxis"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""2e2d783a-8b86-4f8d-853e-71300daf2cde"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""movementAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""4df0ae4f-ce60-4a35-89b2-eb32ee2a522b"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""movementAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""add22ceb-ac2c-4b55-8efa-853d033eb964"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""movementAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""7d127d95-1564-4050-b591-bd62d077b4ca"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""movementAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""e254ec28-5b22-4e78-b37b-bcfedfb72824"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""lookAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""arrowkeys"",
                    ""id"": ""913c2103-e487-416c-924f-97555525e24a"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""lookAxis"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e5b97547-db6d-4e37-83e5-22b25c4c71b2"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""lookAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""1c09300e-f953-46de-bf02-5cc33c02e09c"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""lookAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""37009ca5-2d5c-4701-91c8-4041dba83f34"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""lookAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""52f01af9-bb3c-4899-8c0e-68cae7c03811"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""lookAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""ba47f54f-ea03-48d1-beaf-fc0c10f39048"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""startButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""22aee6f9-c329-4795-a595-b925e49039d7"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""startButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f99d2949-a738-450e-b6fb-c07ef2f0a12d"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""selectButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5feb24bd-615a-42ac-8650-8d4fa06a59f9"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""selectButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""73a85fc4-4e20-42cf-930d-a8a8d744ab53"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""primaryButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e90dfcc1-0702-4cdf-ad8d-dda07d48aab6"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""primaryButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""GamePad"",
            ""bindingGroup"": ""GamePad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // PlayerControls
        m_PlayerControls = asset.FindActionMap("PlayerControls", throwIfNotFound: true);
        m_PlayerControls_primaryButton = m_PlayerControls.FindAction("primaryButton", throwIfNotFound: true);
        m_PlayerControls_secondaryButton = m_PlayerControls.FindAction("secondaryButton", throwIfNotFound: true);
        m_PlayerControls_tertiaryButton = m_PlayerControls.FindAction("tertiaryButton", throwIfNotFound: true);
        m_PlayerControls_utilityButton = m_PlayerControls.FindAction("utilityButton", throwIfNotFound: true);
        m_PlayerControls_movementAxis = m_PlayerControls.FindAction("movementAxis", throwIfNotFound: true);
        m_PlayerControls_lookAxis = m_PlayerControls.FindAction("lookAxis", throwIfNotFound: true);
        m_PlayerControls_startButton = m_PlayerControls.FindAction("startButton", throwIfNotFound: true);
        m_PlayerControls_selectButton = m_PlayerControls.FindAction("selectButton", throwIfNotFound: true);
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

    // PlayerControls
    private readonly InputActionMap m_PlayerControls;
    private IPlayerControlsActions m_PlayerControlsActionsCallbackInterface;
    private readonly InputAction m_PlayerControls_primaryButton;
    private readonly InputAction m_PlayerControls_secondaryButton;
    private readonly InputAction m_PlayerControls_tertiaryButton;
    private readonly InputAction m_PlayerControls_utilityButton;
    private readonly InputAction m_PlayerControls_movementAxis;
    private readonly InputAction m_PlayerControls_lookAxis;
    private readonly InputAction m_PlayerControls_startButton;
    private readonly InputAction m_PlayerControls_selectButton;
    public struct PlayerControlsActions
    {
        private @PlayerControlScheme m_Wrapper;
        public PlayerControlsActions(@PlayerControlScheme wrapper) { m_Wrapper = wrapper; }
        public InputAction @primaryButton => m_Wrapper.m_PlayerControls_primaryButton;
        public InputAction @secondaryButton => m_Wrapper.m_PlayerControls_secondaryButton;
        public InputAction @tertiaryButton => m_Wrapper.m_PlayerControls_tertiaryButton;
        public InputAction @utilityButton => m_Wrapper.m_PlayerControls_utilityButton;
        public InputAction @movementAxis => m_Wrapper.m_PlayerControls_movementAxis;
        public InputAction @lookAxis => m_Wrapper.m_PlayerControls_lookAxis;
        public InputAction @startButton => m_Wrapper.m_PlayerControls_startButton;
        public InputAction @selectButton => m_Wrapper.m_PlayerControls_selectButton;
        public InputActionMap Get() { return m_Wrapper.m_PlayerControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerControlsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerControlsActions instance)
        {
            if (m_Wrapper.m_PlayerControlsActionsCallbackInterface != null)
            {
                @primaryButton.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnPrimaryButton;
                @primaryButton.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnPrimaryButton;
                @primaryButton.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnPrimaryButton;
                @secondaryButton.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnSecondaryButton;
                @secondaryButton.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnSecondaryButton;
                @secondaryButton.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnSecondaryButton;
                @tertiaryButton.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnTertiaryButton;
                @tertiaryButton.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnTertiaryButton;
                @tertiaryButton.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnTertiaryButton;
                @utilityButton.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnUtilityButton;
                @utilityButton.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnUtilityButton;
                @utilityButton.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnUtilityButton;
                @movementAxis.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMovementAxis;
                @movementAxis.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMovementAxis;
                @movementAxis.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMovementAxis;
                @lookAxis.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnLookAxis;
                @lookAxis.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnLookAxis;
                @lookAxis.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnLookAxis;
                @startButton.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnStartButton;
                @startButton.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnStartButton;
                @startButton.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnStartButton;
                @selectButton.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnSelectButton;
                @selectButton.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnSelectButton;
                @selectButton.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnSelectButton;
            }
            m_Wrapper.m_PlayerControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @primaryButton.started += instance.OnPrimaryButton;
                @primaryButton.performed += instance.OnPrimaryButton;
                @primaryButton.canceled += instance.OnPrimaryButton;
                @secondaryButton.started += instance.OnSecondaryButton;
                @secondaryButton.performed += instance.OnSecondaryButton;
                @secondaryButton.canceled += instance.OnSecondaryButton;
                @tertiaryButton.started += instance.OnTertiaryButton;
                @tertiaryButton.performed += instance.OnTertiaryButton;
                @tertiaryButton.canceled += instance.OnTertiaryButton;
                @utilityButton.started += instance.OnUtilityButton;
                @utilityButton.performed += instance.OnUtilityButton;
                @utilityButton.canceled += instance.OnUtilityButton;
                @movementAxis.started += instance.OnMovementAxis;
                @movementAxis.performed += instance.OnMovementAxis;
                @movementAxis.canceled += instance.OnMovementAxis;
                @lookAxis.started += instance.OnLookAxis;
                @lookAxis.performed += instance.OnLookAxis;
                @lookAxis.canceled += instance.OnLookAxis;
                @startButton.started += instance.OnStartButton;
                @startButton.performed += instance.OnStartButton;
                @startButton.canceled += instance.OnStartButton;
                @selectButton.started += instance.OnSelectButton;
                @selectButton.performed += instance.OnSelectButton;
                @selectButton.canceled += instance.OnSelectButton;
            }
        }
    }
    public PlayerControlsActions @PlayerControls => new PlayerControlsActions(this);
    private int m_GamePadSchemeIndex = -1;
    public InputControlScheme GamePadScheme
    {
        get
        {
            if (m_GamePadSchemeIndex == -1) m_GamePadSchemeIndex = asset.FindControlSchemeIndex("GamePad");
            return asset.controlSchemes[m_GamePadSchemeIndex];
        }
    }
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IPlayerControlsActions
    {
        void OnPrimaryButton(InputAction.CallbackContext context);
        void OnSecondaryButton(InputAction.CallbackContext context);
        void OnTertiaryButton(InputAction.CallbackContext context);
        void OnUtilityButton(InputAction.CallbackContext context);
        void OnMovementAxis(InputAction.CallbackContext context);
        void OnLookAxis(InputAction.CallbackContext context);
        void OnStartButton(InputAction.CallbackContext context);
        void OnSelectButton(InputAction.CallbackContext context);
    }
}
