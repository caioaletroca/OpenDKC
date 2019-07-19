using UnityEngine;

public partial class KongController
{
    #region State Variables

    /// <summary>
    /// The horizontal input axis in absolute value
    /// </summary>
    public float HorizontalValue
    {
        get
        {
            return animator.GetFloat(AnimationParameters.HorizontalValue);
        }
        set
        {
            animator.SetFloat(AnimationParameters.HorizontalValue, value);
        }
    }

    /// <summary>
    /// The vertical input axis value
    /// </summary>
    public float VerticalValue
    {
        get
        {
            return animator.GetFloat(AnimationParameters.VerticalValue);
        }
        set
        {
            animator.SetFloat(AnimationParameters.VerticalValue, value);
        }
    }

    /// <summary>
    /// The current ground distance evaluated right below the character
    /// </summary>
    public float GroundDistance
    {
        get
        {
            return animator.GetFloat(AnimationParameters.GroundDistance);
        }
        set
        {
            animator.SetFloat(AnimationParameters.GroundDistance, value);
        }
    }

    /// <summary>
    /// A flag that represents if the player is sitting on a ground surface
    /// </summary>
    public bool Grounded
    {
        get
        {
            return animator.GetBool(AnimationParameters.Grounded);
        }
        set
        {
            animator.SetBool(AnimationParameters.Grounded, value);
        }
    }

    /// <summary>
    /// A flag that represents if the player is jumping
    /// </summary>
    public bool Jump
    {
        get
        {
            return animator.GetBool(AnimationParameters.Jump);
        }
        set
        {
            animator.SetBool(AnimationParameters.Jump, value);
        }
    }

    /// <summary>
    /// A flag that represents if the player is attacking
    /// </summary>
    public bool Attack
    {
        get
        {
            return animator.GetBool(AnimationParameters.Attack);
        }
        set
        {
            animator.SetBool(AnimationParameters.Attack, value);
        }
    }

    /// <summary>
    /// A flag that represents if the player is running
    /// </summary>
    public bool Run
    {
        get
        {
            return animator.GetBool(AnimationParameters.Run);
        }
        set
        {
            animator.SetBool(AnimationParameters.Run, value);
        }
    }

    /// <summary>
    /// A flag that represents if the player is hanging on a hook
    /// </summary>
    public bool Hook
    {
        get
        {
            return animator.GetBool(AnimationParameters.Hook);
        }
        set
        {
            animator.SetBool(AnimationParameters.Hook, value);
        }
    }

    #endregion

    #region State Methods

    /// <summary>
    /// Registers events to update state variables
    /// </summary>
    public void RegisterStateVariables()
    {
        // HorizontalValue
        PlayerInput.Instance.KongMap.HorizontalAxis.performed += e => HorizontalValue = Mathf.Abs(e.ReadValue<float>());
        PlayerInput.Instance.KongMap.HorizontalAxis.canceled += e => HorizontalValue = 0;

        // VerticalValue
        PlayerInput.Instance.KongMap.VerticalAxis.performed += e => VerticalValue = e.ReadValue<float>();
        PlayerInput.Instance.KongMap.VerticalAxis.canceled += e => VerticalValue = 0;

        // Jump
        PlayerInput.Instance.KongMap.Jump.performed += e =>
        {
            Jump = true;
            Grounded = false;
            Attack = false;
        };
        PlayerInput.Instance.KongMap.Jump.canceled += e => Jump = false;

        // Run
        PlayerInput.Instance.KongMap.Attack.performed += e => Attack = true;
        PlayerInput.Instance.KongMap.Attack.canceled += e => Attack = false;

        // Run
        PlayerInput.Instance.KongMap.Attack.performed += e => Run = true;
        PlayerInput.Instance.KongMap.Attack.canceled += e => Run = false;
    }

    /// <summary>
    /// Updates all the variables in the Update method
    /// </summary>
    public void UpdateStateVariables()
    {
        UpdateGroundDistance();
        UpdateGrounded();
    }

    public void UpdateGrounded()
    {
        var Grounded = false;

        // Check if the player touches the ground again
        Collider2D[] colliders = Physics2D.OverlapCircleAll(MovementSettings.GroundPoint.position, mGroundedRadius, MovementSettings.GroundLayer);
        foreach (var collider in colliders)
            if (collider.gameObject != gameObject)
                Grounded = true;

        // Set the state variable
        this.Grounded = Grounded;
    }

    public void UpdateGroundDistance()
    {
        // Calculate distance to the ground
        RaycastHit2D hit = Physics2D.Raycast(MovementSettings.GroundPoint.transform.position, Vector2.down, 100, MovementSettings.GroundLayer);
        if (hit != null)
            animator.SetFloat(AnimationParameters.GroundDistance, hit.distance);
    }

    #endregion
}
