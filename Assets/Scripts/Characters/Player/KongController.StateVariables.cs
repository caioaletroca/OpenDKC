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
        get => animator.GetFloat(AnimationParameters.VerticalValue);
        set => animator.SetFloat(AnimationParameters.VerticalValue, value);
    }

    /// <summary>
    /// The vertical speed value
    /// </summary>
    public float VerticalSpeed
    {
        get => animator.GetFloat(AnimationParameters.VerticalSpeed);
        set => animator.SetFloat(AnimationParameters.VerticalSpeed, value);
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

    /// <summary>
    /// A flag that represents if the player is spawning
    /// </summary>
    public bool Spawn
    {
        get => animator.GetBool(AnimationParameters.Spawn);
        set => animator.SetBool(AnimationParameters.Spawn, value);
    }

    /// <summary>
    /// A flag that represents if the player is dead
    /// </summary>
    public bool Die
    {
        get => animator.GetBool(AnimationParameters.Die);
        set => animator.SetBool(AnimationParameters.Die, value);
    }

    #endregion

    #region State Methods

    /// <summary>
    /// Registers events to update state variables
    /// </summary>
    public void RegisterStateVariables()
    {
        // Get the player input before awake
        var inputMap = FindObjectOfType<InputController>().KongMap;

        // HorizontalValue
        inputMap.HorizontalAxis.performed += e => HorizontalValue = Mathf.Abs(e.ReadValue<float>());
        inputMap.HorizontalAxis.canceled += e => HorizontalValue = 0;

        // VerticalValue
        inputMap.VerticalAxis.performed += e => VerticalValue = e.ReadValue<float>();
        inputMap.VerticalAxis.canceled += e => VerticalValue = 0;

        // Jump
        inputMap.Jump.performed += e =>
        {
            Jump = true;
            Grounded = false;
            Attack = false;
        };
        inputMap.Jump.canceled += e => Jump = false;

        // Run
        inputMap.Attack.performed += e => Attack = true;
        inputMap.Attack.canceled += e => Attack = false;

        // Run
        inputMap.Attack.performed += e => Run = true;
        inputMap.Attack.canceled += e => Run = false;
    }

    /// <summary>
    /// Updates all the variables in the Update method
    /// </summary>
    public void UpdateStateVariables()
    {
        UpdateGroundDistance();
        UpdateGrounded();
        UpdateVerticalSpeed();
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

    public void UpdateVerticalSpeed()
    {
        // Only when player dies, saving process power
        if (Die)
            VerticalSpeed = mRigidBody2D.velocity.y;
    }

    #endregion
}
