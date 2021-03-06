// GENERATED AUTOMATICALLY FROM 'Assets/Game/Inputs/PlayerInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""e6b4bdfa-11e7-4a90-bfcd-c026614b3fe3"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""bfc0c98a-90aa-4084-b841-0bd9b2861dc7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Save"",
                    ""type"": ""Button"",
                    ""id"": ""2e8402b8-77a7-4217-a39b-f664f4972e2b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Load"",
                    ""type"": ""Button"",
                    ""id"": ""3ff8e9d2-69e6-44a5-b77f-0dc5884434a4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""OpenMainMenu"",
                    ""type"": ""Button"",
                    ""id"": ""52f7a239-61da-4a73-9ba5-38c5ad71ebb0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DeleteSave"",
                    ""type"": ""Button"",
                    ""id"": ""de144586-5bfb-4fa8-bbe9-8da0cb20a5a8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""14c3ea26-1a59-4ea6-a668-3529f09e9ba6"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e3d594f1-02cd-4299-913f-d9e801d55a10"",
                    ""path"": ""<Keyboard>/f9"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Save"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""444df00a-efa6-4820-a5bc-f349b2cfe2a8"",
                    ""path"": ""<Keyboard>/f10"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Load"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""84033e2d-ca78-4acc-8a2f-e65ed3010af0"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenMainMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""75ee5982-20c6-4e53-9504-8e04364f3816"",
                    ""path"": ""<Keyboard>/f12"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DeleteSave"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_Save = m_Player.FindAction("Save", throwIfNotFound: true);
        m_Player_Load = m_Player.FindAction("Load", throwIfNotFound: true);
        m_Player_OpenMainMenu = m_Player.FindAction("OpenMainMenu", throwIfNotFound: true);
        m_Player_DeleteSave = m_Player.FindAction("DeleteSave", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_Save;
    private readonly InputAction m_Player_Load;
    private readonly InputAction m_Player_OpenMainMenu;
    private readonly InputAction m_Player_DeleteSave;
    public struct PlayerActions
    {
        private @PlayerInputActions m_Wrapper;
        public PlayerActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @Save => m_Wrapper.m_Player_Save;
        public InputAction @Load => m_Wrapper.m_Player_Load;
        public InputAction @OpenMainMenu => m_Wrapper.m_Player_OpenMainMenu;
        public InputAction @DeleteSave => m_Wrapper.m_Player_DeleteSave;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Save.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSave;
                @Save.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSave;
                @Save.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSave;
                @Load.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLoad;
                @Load.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLoad;
                @Load.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLoad;
                @OpenMainMenu.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOpenMainMenu;
                @OpenMainMenu.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOpenMainMenu;
                @OpenMainMenu.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOpenMainMenu;
                @DeleteSave.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDeleteSave;
                @DeleteSave.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDeleteSave;
                @DeleteSave.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDeleteSave;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Save.started += instance.OnSave;
                @Save.performed += instance.OnSave;
                @Save.canceled += instance.OnSave;
                @Load.started += instance.OnLoad;
                @Load.performed += instance.OnLoad;
                @Load.canceled += instance.OnLoad;
                @OpenMainMenu.started += instance.OnOpenMainMenu;
                @OpenMainMenu.performed += instance.OnOpenMainMenu;
                @OpenMainMenu.canceled += instance.OnOpenMainMenu;
                @DeleteSave.started += instance.OnDeleteSave;
                @DeleteSave.performed += instance.OnDeleteSave;
                @DeleteSave.canceled += instance.OnDeleteSave;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnSave(InputAction.CallbackContext context);
        void OnLoad(InputAction.CallbackContext context);
        void OnOpenMainMenu(InputAction.CallbackContext context);
        void OnDeleteSave(InputAction.CallbackContext context);
    }
}
