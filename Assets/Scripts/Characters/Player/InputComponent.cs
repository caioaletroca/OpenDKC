using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An abstract class component to handle the game Input system
/// </summary>
public abstract class InputComponent : MonoBehaviour
{
    #region Public Types

    /// <summary>
    /// Input type definition
    /// </summary>
    public enum InputType
    {
        MouseAndKeyboard,
        Controller,
    }

    /// <summary>
    /// Xbox Controller Buttons definition
    /// </summary>
    public enum XboxControllerButtons
    {
        None,
        A,
        B,
        X,
        Y,
        Leftstick,
        Rightstick,
        View,
        Menu,
        LeftBumper,
        RightBumper,
    }

    /// <summary>
    /// Xbox Controller Axes definition
    /// </summary>
    public enum XboxControllerAxes
    {
        None,
        LeftstickHorizontal,
        LeftstickVertical,
        DpadHorizontal,
        DpadVertical,
        RightstickHorizontal,
        RightstickVertical,
        LeftTrigger,
        RightTrigger,
    }

    /// <summary>
    /// A base class for the Input system
    /// </summary>
    [Serializable]
    public class InputBase
    {
        #region Public Properties

        /// <summary>
        /// Getter for the Enabled Flag
        /// </summary>
        public bool Enabled
        {
            get { return mEnabled; }
        }

        #endregion

        #region Private Properties

        /// <summary>
        /// A flag that represents if the input is enabled
        /// </summary>
        [SerializeField]
        protected bool mEnabled = true;

        /// <summary>
        /// A flag that represents if the input is released from control
        /// </summary>
        protected bool mGettingInput = true;

        #endregion

        #region Public Methods

        /// <summary>
        /// Enables the input
        /// </summary>
        public void Enable()
        {
            mEnabled = true;
        }

        /// <summary>
        /// Disables the input
        /// </summary>
        public void Disable()
        {
            mEnabled = false;
        }

        /// <summary>
        /// Gain control by the input
        /// </summary>
        public void GainControl()
        {
            mGettingInput = true;
        }

        /// <summary>
        /// Get the current values for the input
        /// </summary>
        /// <param name="inputType">The current input type</param>
        /// <param name="fixedUpdateHappened"></param>
        public virtual void Get(InputType inputType, bool fixedUpdateHappened = false) { }

        /// <summary>
        /// Release the control for the input
        /// </summary>
        /// <param name="resetValues"></param>
        /// <returns></returns>
        public virtual IEnumerator ReleaseControl(bool resetValues) { yield return null; }

        #endregion
    }

    /// <summary>
    /// Button input class for the input system
    /// </summary>
    [Serializable]
    public class InputButton : InputBase
    {
        #region Public Properties

        public KeyCode key;

        public XboxControllerButtons controllerButton;

        public bool Down { get; protected set; }
        public bool Held { get; protected set; }
        public bool Up { get; protected set; }

        #endregion

        #region Private Types

        /// <summary>
        /// String names specifications for the <see cref="XboxControllerButtons"/>
        /// </summary>
        protected static readonly Dictionary<int, string> ButtonsName = new Dictionary<int, string>
        {
            {(int)XboxControllerButtons.A, "A"},
            {(int)XboxControllerButtons.B, "B"},
            {(int)XboxControllerButtons.X, "X"},
            {(int)XboxControllerButtons.Y, "Y"},
            {(int)XboxControllerButtons.Leftstick, "Leftstick"},
            {(int)XboxControllerButtons.Rightstick, "Rightstick"},
            {(int)XboxControllerButtons.View, "View"},
            {(int)XboxControllerButtons.Menu, "Menu"},
            {(int)XboxControllerButtons.LeftBumper, "Left Bumper"},
            {(int)XboxControllerButtons.RightBumper, "Right Bumper"},
        };

        #endregion

        #region Private Properties

        // This is used to change the state of a button (Down, Up) only if at least a FixedUpdate happened between the previous Frame
        // and this one. Since movement are made in FixedUpdate, without that an input could be missed it get press/release between fixedupdate
        bool mAfterFixedUpdateDown;
        bool mAfterFixedUpdateHeld;
        bool mAfterFixedUpdateUp;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="key">The current PC key</param>
        /// <param name="controllerButton">The controller equivalent</param>
        public InputButton(KeyCode key, XboxControllerButtons controllerButton)
        {
            this.key = key;
            this.controllerButton = controllerButton;
        }

        #endregion

        #region Public Methods

        public override void Get(InputType inputType, bool fixedUpdateHappened)
        {
            // If disabled, make all flags false
            if (!mEnabled)
            {
                Down = false;
                Held = false;
                Up = false;
                return;
            }

            // If not in control, return
            if (!mGettingInput)
                return;

            // Get the input values
            if(inputType == InputType.Controller)
            {
                if(fixedUpdateHappened)
                {
                    Down = Input.GetButtonDown(ButtonsName[(int)controllerButton]);
                    Held = Input.GetButton(ButtonsName[(int)controllerButton]);
                    Up = Input.GetButtonUp(ButtonsName[(int)controllerButton]);

                    mAfterFixedUpdateDown = Down;
                    mAfterFixedUpdateHeld = Held;
                    mAfterFixedUpdateUp = Up;
                }
                else
                {
                    Down = Input.GetButtonDown(ButtonsName[(int)controllerButton]) || mAfterFixedUpdateDown;
                    Held = Input.GetButton(ButtonsName[(int)controllerButton]) || mAfterFixedUpdateHeld;
                    Up = Input.GetButtonUp(ButtonsName[(int)controllerButton]) || mAfterFixedUpdateUp;

                    mAfterFixedUpdateDown |= Down;
                    mAfterFixedUpdateHeld |= Held;
                    mAfterFixedUpdateUp |= Up;
                }
            }
            else if(inputType == InputType.MouseAndKeyboard)
            {
                if (fixedUpdateHappened)
                {
                    Down = Input.GetKeyDown(key);
                    Held = Input.GetKey(key);
                    Up = Input.GetKeyUp(key);

                    mAfterFixedUpdateDown = Down;
                    mAfterFixedUpdateHeld = Held;
                    mAfterFixedUpdateUp = Up;
                }
                else
                {
                    Down = Input.GetKeyDown(key) || mAfterFixedUpdateDown;
                    Held = Input.GetKey(key) || mAfterFixedUpdateHeld;
                    Up = Input.GetKeyUp(key) || mAfterFixedUpdateUp;

                    mAfterFixedUpdateDown |= Down;
                    mAfterFixedUpdateHeld |= Held;
                    mAfterFixedUpdateUp |= Up;
                }
            }
        }

        public override IEnumerator ReleaseControl(bool resetValues)
        {
            mGettingInput = false;

            if (!resetValues)
                yield break;

            if (Down)
                Up = true;
            Down = false;
            Held = false;

            mAfterFixedUpdateDown = false;
            mAfterFixedUpdateHeld = false;
            mAfterFixedUpdateUp = false;

            yield return null;

            Up = false;
        }

        #endregion
    }

    /// <summary>
    /// Axis input class for the input system
    /// </summary>
    [Serializable]
    public class InputAxis : InputBase
    {
        #region Public Properties

        public KeyCode positive;
        public KeyCode negative;
        public XboxControllerAxes controllerAxis;

        public float Value { get; protected set; }
        public bool ReceivingInput { get; protected set; }

        #endregion

        #region Private Types

        /// <summary>
        /// String names specifications for the <see cref="XboxControllerAxes"/>
        /// </summary>
        protected readonly static Dictionary<int, string> AxisName = new Dictionary<int, string> {
            {(int)XboxControllerAxes.LeftstickHorizontal, "Leftstick Horizontal"},
            {(int)XboxControllerAxes.LeftstickVertical, "Leftstick Vertical"},
            {(int)XboxControllerAxes.DpadHorizontal, "Dpad Horizontal"},
            {(int)XboxControllerAxes.DpadVertical, "Dpad Vertical"},
            {(int)XboxControllerAxes.RightstickHorizontal, "Rightstick Horizontal"},
            {(int)XboxControllerAxes.RightstickVertical, "Rightstick Vertical"},
            {(int)XboxControllerAxes.LeftTrigger, "Left Trigger"},
            {(int)XboxControllerAxes.RightTrigger, "Right Trigger"},
        };

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="positive">The positive key code</param>
        /// <param name="negative">The negative key code</param>
        /// <param name="controllerAxis">The Xbox Controller equivalent axis</param>
        public InputAxis(KeyCode positive, KeyCode negative, XboxControllerAxes controllerAxis)
        {
            this.positive = positive;
            this.negative = negative;
            this.controllerAxis = controllerAxis;
        }

        #endregion

        #region Public Methods

        public override void Get(InputType inputType, bool fixedUpdateHappened = false)
        {
            // If disabled, make all flags false
            if (!mEnabled)
            {
                Value = 0;
                return;
            }

            // If not in control, return
            if (!mGettingInput)
                return;

            bool positiveHeld = false;
            bool negativeHeld = false;

            if(inputType == InputType.Controller)
            {
                float value = Input.GetAxisRaw(AxisName[(int)controllerAxis]);
                positiveHeld = value > Single.Epsilon;
                negativeHeld = value < -Single.Epsilon;
            }
            else if(inputType == InputType.MouseAndKeyboard)
            {
                positiveHeld = Input.GetKey(positive);
                negativeHeld = Input.GetKey(negative);
            }

            if (positiveHeld == negativeHeld)
                Value = 0;
            else if (positiveHeld)
                Value = 1;
            else
                Value = -1;

            ReceivingInput = positiveHeld || negativeHeld;
        }

        public override IEnumerator ReleaseControl(bool resetValues)
        {
            mGettingInput = false;

            if(resetValues)
            {
                Value = 0;
                ReceivingInput = false;
            }

            yield return null;
        }

        #endregion
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// The current selected input type
    /// </summary>
    public InputType inputType = InputType.MouseAndKeyboard;

    #endregion

    #region Private Properties

    /// <summary>
    /// A flag that represents if a fixed update was happened 
    /// </summary>
    bool mFixedUpdateHappened = false;

    #endregion

    #region Unity Methods

    private void Update()
    {
        // Get the current input values
        GetInputs(mFixedUpdateHappened || Mathf.Approximately(Time.timeScale, 0));

        // Release flag
        mFixedUpdateHappened = false;
    }

    private void FixedUpdate()
    {
        mFixedUpdateHappened = true;
    }

    #endregion

    #region Public Methods

    public abstract void GainControl();

    public abstract void ReleaseControl(bool resetValues = true);

    #endregion

    #region Private Methods

    protected abstract void GetInputs(bool fixedUpdateHappened);

    protected void GainControl(InputButton inputButton)
    {
        inputButton.GainControl();
    }

    protected void GainControl(InputAxis inputAxis)
    {
        inputAxis.GainControl();
    }

    protected void ReleaseControl(InputButton inputButton, bool resetValues)
    {
        StartCoroutine(inputButton.ReleaseControl(resetValues));
    }

    protected void ReleaseControl(InputAxis inputAxis, bool resetValues)
    {
        StartCoroutine(inputAxis.ReleaseControl(resetValues));
    }

    #endregion
}
