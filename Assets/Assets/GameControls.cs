// GENERATED AUTOMATICALLY FROM 'Assets/GameControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class GameControls : IInputActionCollection
{
    private InputActionAsset asset;
    public GameControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameControls"",
    ""maps"": [
        {
            ""name"": ""KongMap"",
            ""id"": ""1782ffb3-6f19-44d6-b0ed-53c64f66b300"",
            ""actions"": [
                {
                    ""name"": ""Pause"",
                    ""id"": ""ddbc815f-b9d5-4226-b808-5196f76a25ac"",
                    ""expectedControlLayout"": ""Button"",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""Horizontal Axis"",
                    ""id"": ""7e5fe108-b0f8-4837-a809-f3eaf6bb6ac9"",
                    ""expectedControlLayout"": ""Axis"",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""Jump"",
                    ""id"": ""1131dcf3-20a1-4c50-a7e6-7c44a4ae1076"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""Attack"",
                    ""id"": ""57c1bd6b-ed48-4b59-8056-5ed7636742c2"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""Vertical Axis"",
                    ""id"": ""c467672d-33cd-4313-9e26-5b124b1fde0b"",
                    ""expectedControlLayout"": ""Axis"",
                    ""continuous"": true,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""SideButtons"",
                    ""id"": ""7ff8e25c-8058-4a9e-a705-32dd9d6ad164"",
                    ""expectedControlLayout"": """",
                    ""continuous"": true,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""f49ca4d5-9c6c-4521-9ef8-95cd646797f5"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal Axis"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""349546cd-0bf3-4185-803a-1e83559361ea"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal Axis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""ea00f40c-e89f-4857-981f-69a077cd8784"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal Axis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""47da9928-6646-49c8-a7b3-edccebe1a561"",
                    ""path"": ""<Keyboard>/numpad5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""9c442124-bc67-4948-b485-31e49e6fe709"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""336d8c73-9e69-45f1-b53e-9af1f3c370f5"",
                    ""path"": ""<Keyboard>/numpad4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""f74a468b-ccfe-4474-a347-c57ea206e65d"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical Axis"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""183cb062-464b-46ae-b343-6135b33e4e47"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical Axis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""5955b331-b949-4395-b7f9-2b440018bda1"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical Axis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""470b434f-9f99-42ce-ba93-9557d3d3b5af"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SideButtons"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""a27fa2fc-bd86-4d9e-bd18-b4d5f0a8d5c0"",
                    ""path"": ""<Keyboard>/numpad7"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SideButtons"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                }
            ]
        },
        {
            ""name"": ""BarrelMap"",
            ""id"": ""204ba88c-5deb-445a-9c9b-d957128c8ba2"",
            ""actions"": [
                {
                    ""name"": ""HorizontalAxis"",
                    ""id"": ""115ddc59-4719-4f09-9f7d-aec9441a60dc"",
                    ""expectedControlLayout"": ""Axis"",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""609b6705-9d12-4adf-bfa3-8765c825a93d"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalAxis"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""00b531ef-3731-4689-9012-780ec6b04307"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""6bb769dc-8025-4a22-914c-59aa24189aa0"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // KongMap
        m_KongMap = asset.GetActionMap("KongMap");
        m_KongMap_Pause = m_KongMap.GetAction("Pause");
        m_KongMap_HorizontalAxis = m_KongMap.GetAction("Horizontal Axis");
        m_KongMap_Jump = m_KongMap.GetAction("Jump");
        m_KongMap_Attack = m_KongMap.GetAction("Attack");
        m_KongMap_VerticalAxis = m_KongMap.GetAction("Vertical Axis");
        m_KongMap_SideButtons = m_KongMap.GetAction("SideButtons");
        // BarrelMap
        m_BarrelMap = asset.GetActionMap("BarrelMap");
        m_BarrelMap_HorizontalAxis = m_BarrelMap.GetAction("HorizontalAxis");
    }

    ~GameControls()
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

    public ReadOnlyArray<InputControlScheme> controlSchemes
    {
        get => asset.controlSchemes;
    }

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

    // KongMap
    private InputActionMap m_KongMap;
    private IKongMapActions m_KongMapActionsCallbackInterface;
    private InputAction m_KongMap_Pause;
    private InputAction m_KongMap_HorizontalAxis;
    private InputAction m_KongMap_Jump;
    private InputAction m_KongMap_Attack;
    private InputAction m_KongMap_VerticalAxis;
    private InputAction m_KongMap_SideButtons;
    public struct KongMapActions
    {
        private GameControls m_Wrapper;
        public KongMapActions(GameControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Pause { get { return m_Wrapper.m_KongMap_Pause; } }
        public InputAction @HorizontalAxis { get { return m_Wrapper.m_KongMap_HorizontalAxis; } }
        public InputAction @Jump { get { return m_Wrapper.m_KongMap_Jump; } }
        public InputAction @Attack { get { return m_Wrapper.m_KongMap_Attack; } }
        public InputAction @VerticalAxis { get { return m_Wrapper.m_KongMap_VerticalAxis; } }
        public InputAction @SideButtons { get { return m_Wrapper.m_KongMap_SideButtons; } }
        public InputActionMap Get() { return m_Wrapper.m_KongMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled { get { return Get().enabled; } }
        public InputActionMap Clone() { return Get().Clone(); }
        public static implicit operator InputActionMap(KongMapActions set) { return set.Get(); }
        public void SetCallbacks(IKongMapActions instance)
        {
            if (m_Wrapper.m_KongMapActionsCallbackInterface != null)
            {
                Pause.started -= m_Wrapper.m_KongMapActionsCallbackInterface.OnPause;
                Pause.performed -= m_Wrapper.m_KongMapActionsCallbackInterface.OnPause;
                Pause.canceled -= m_Wrapper.m_KongMapActionsCallbackInterface.OnPause;
                HorizontalAxis.started -= m_Wrapper.m_KongMapActionsCallbackInterface.OnHorizontalAxis;
                HorizontalAxis.performed -= m_Wrapper.m_KongMapActionsCallbackInterface.OnHorizontalAxis;
                HorizontalAxis.canceled -= m_Wrapper.m_KongMapActionsCallbackInterface.OnHorizontalAxis;
                Jump.started -= m_Wrapper.m_KongMapActionsCallbackInterface.OnJump;
                Jump.performed -= m_Wrapper.m_KongMapActionsCallbackInterface.OnJump;
                Jump.canceled -= m_Wrapper.m_KongMapActionsCallbackInterface.OnJump;
                Attack.started -= m_Wrapper.m_KongMapActionsCallbackInterface.OnAttack;
                Attack.performed -= m_Wrapper.m_KongMapActionsCallbackInterface.OnAttack;
                Attack.canceled -= m_Wrapper.m_KongMapActionsCallbackInterface.OnAttack;
                VerticalAxis.started -= m_Wrapper.m_KongMapActionsCallbackInterface.OnVerticalAxis;
                VerticalAxis.performed -= m_Wrapper.m_KongMapActionsCallbackInterface.OnVerticalAxis;
                VerticalAxis.canceled -= m_Wrapper.m_KongMapActionsCallbackInterface.OnVerticalAxis;
                SideButtons.started -= m_Wrapper.m_KongMapActionsCallbackInterface.OnSideButtons;
                SideButtons.performed -= m_Wrapper.m_KongMapActionsCallbackInterface.OnSideButtons;
                SideButtons.canceled -= m_Wrapper.m_KongMapActionsCallbackInterface.OnSideButtons;
            }
            m_Wrapper.m_KongMapActionsCallbackInterface = instance;
            if (instance != null)
            {
                Pause.started += instance.OnPause;
                Pause.performed += instance.OnPause;
                Pause.canceled += instance.OnPause;
                HorizontalAxis.started += instance.OnHorizontalAxis;
                HorizontalAxis.performed += instance.OnHorizontalAxis;
                HorizontalAxis.canceled += instance.OnHorizontalAxis;
                Jump.started += instance.OnJump;
                Jump.performed += instance.OnJump;
                Jump.canceled += instance.OnJump;
                Attack.started += instance.OnAttack;
                Attack.performed += instance.OnAttack;
                Attack.canceled += instance.OnAttack;
                VerticalAxis.started += instance.OnVerticalAxis;
                VerticalAxis.performed += instance.OnVerticalAxis;
                VerticalAxis.canceled += instance.OnVerticalAxis;
                SideButtons.started += instance.OnSideButtons;
                SideButtons.performed += instance.OnSideButtons;
                SideButtons.canceled += instance.OnSideButtons;
            }
        }
    }
    public KongMapActions @KongMap
    {
        get
        {
            return new KongMapActions(this);
        }
    }

    // BarrelMap
    private InputActionMap m_BarrelMap;
    private IBarrelMapActions m_BarrelMapActionsCallbackInterface;
    private InputAction m_BarrelMap_HorizontalAxis;
    public struct BarrelMapActions
    {
        private GameControls m_Wrapper;
        public BarrelMapActions(GameControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @HorizontalAxis { get { return m_Wrapper.m_BarrelMap_HorizontalAxis; } }
        public InputActionMap Get() { return m_Wrapper.m_BarrelMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled { get { return Get().enabled; } }
        public InputActionMap Clone() { return Get().Clone(); }
        public static implicit operator InputActionMap(BarrelMapActions set) { return set.Get(); }
        public void SetCallbacks(IBarrelMapActions instance)
        {
            if (m_Wrapper.m_BarrelMapActionsCallbackInterface != null)
            {
                HorizontalAxis.started -= m_Wrapper.m_BarrelMapActionsCallbackInterface.OnHorizontalAxis;
                HorizontalAxis.performed -= m_Wrapper.m_BarrelMapActionsCallbackInterface.OnHorizontalAxis;
                HorizontalAxis.canceled -= m_Wrapper.m_BarrelMapActionsCallbackInterface.OnHorizontalAxis;
            }
            m_Wrapper.m_BarrelMapActionsCallbackInterface = instance;
            if (instance != null)
            {
                HorizontalAxis.started += instance.OnHorizontalAxis;
                HorizontalAxis.performed += instance.OnHorizontalAxis;
                HorizontalAxis.canceled += instance.OnHorizontalAxis;
            }
        }
    }
    public BarrelMapActions @BarrelMap
    {
        get
        {
            return new BarrelMapActions(this);
        }
    }
    public interface IKongMapActions
    {
        void OnPause(InputAction.CallbackContext context);
        void OnHorizontalAxis(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnVerticalAxis(InputAction.CallbackContext context);
        void OnSideButtons(InputAction.CallbackContext context);
    }
    public interface IBarrelMapActions
    {
        void OnHorizontalAxis(InputAction.CallbackContext context);
    }
}
