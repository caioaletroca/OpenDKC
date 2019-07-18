using UnityEngine;

/// <summary>
/// The player input system
/// </summary>
public class PlayerInputOld : InputComponent
{
    #region Singleton

    /// <summary>
    /// A singleton instance for the class
    /// </summary>
    public static PlayerInput Instance => new PlayerInput();

    #endregion

    #region Inputs List

    public InputButton Pause = new InputButton(KeyCode.Escape, XboxControllerButtons.Menu);
    public InputButton Attack = new InputButton(KeyCode.Keypad4, XboxControllerButtons.Menu);
    public InputButton Jump = new InputButton(KeyCode.Keypad5, XboxControllerButtons.Menu);
    public InputAxis Horizontal = new InputAxis(KeyCode.D, KeyCode.A, XboxControllerAxes.LeftstickHorizontal);
    public InputAxis Vertical = new InputAxis(KeyCode.W, KeyCode.S, XboxControllerAxes.LeftstickVertical);

    #endregion

    #region Private Properties

    /// <summary>
    /// A flag that represents if the debug menu is opened
    /// </summary>
    protected bool mDebugMode = false;

    #endregion

    #region Private Methods

    protected override void GetInputs(bool fixedUpdateHappened)
    {
        Pause.Get(inputType, fixedUpdateHappened);
        Attack.Get(inputType, fixedUpdateHappened);
        Jump.Get(inputType, fixedUpdateHappened);
        Horizontal.Get(inputType, fixedUpdateHappened);
        Vertical.Get(inputType, fixedUpdateHappened);

        // Opens the debug menu
        if(Input.GetKeyDown(KeyCode.F12))
        {
            mDebugMode = !mDebugMode;
        }
    }

    public override void GainControl()
    {
        GainControl(Pause);
        GainControl(Attack);
        GainControl(Jump);
        GainControl(Horizontal);
        GainControl(Vertical);
    }

    public override void ReleaseControl(bool resetValues = true)
    {
        ReleaseControl(Pause, resetValues);
        ReleaseControl(Attack, resetValues);
        ReleaseControl(Jump, resetValues);
        ReleaseControl(Horizontal, resetValues);
        ReleaseControl(Vertical, resetValues);
    }

    private void OnGUI()
    {
        // Draws the debug menu
        if(mDebugMode)
        {
            GUILayout.BeginArea(new Rect(30, Screen.height - 100, 200, 100));

            GUILayout.BeginVertical("box");
            GUILayout.Label("Press F12 to close");

            // TODO: Draw debug menu stuff

            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }

    #endregion
}
