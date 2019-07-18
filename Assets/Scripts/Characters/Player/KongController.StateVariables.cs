using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public partial class KongController
{
    #region State Variables

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
        };
        PlayerInput.Instance.KongMap.Jump.canceled += e => Jump = false;

        // Run
        PlayerInput.Instance.KongMap.Attack.performed += e => Attack = true;
        PlayerInput.Instance.KongMap.Attack.canceled += e => Attack = false;

        // Run
        PlayerInput.Instance.KongMap.Attack.performed += e => Run = true;
        PlayerInput.Instance.KongMap.Attack.canceled += e => Run = false;
    }

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
