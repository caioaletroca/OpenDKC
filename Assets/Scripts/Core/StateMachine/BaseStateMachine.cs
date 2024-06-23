using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaseStateMachine<TController> : BaseState<TController> {
    #region Private Properties

    /// <summary>
    /// Current state
    /// </summary>
    BaseState<TController> current;

    /// <summary>
    /// Dictionary containing this state machine states
    /// </summary>
    /// <returns></returns>
    Dictionary<Type, BaseState<TController>> states = new();

    /// <summary>
    /// Dictionary containing this state machine any states
    /// </summary>
    /// <returns></returns>
    Dictionary<Type, BaseState<TController>> anyStates = new();

    #endregion

    #region Constructor

    public BaseStateMachine(TController controller, Animator animator) : base(controller, animator) { }

    #endregion

    #region Public Methods

    /// <summary>
    /// Sets current state
    /// </summary>
    /// <param name="type">Type of the state</param>
    public void SetState(Type type) {
        current = states[type];
        current?.OnStateStart();
    }

    /// <summary>
    /// Gets a state by <paramref name="Type"/>
    /// Gets recursively over the tree
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public BaseState<TController> GetState(Type type) {
        var state = states.GetValueOrDefault(type);

        if(state == null) {
            // Iterate over all states
            foreach(var item in states) {
                // Check if the state is a state machine
                if(item.Value.GetType().IsSubclassOf(typeof(BaseStateMachine<TController>))) {
                    var subStateMachine = (BaseStateMachine<TController>)item.Value;

                    // Recursively search over children
                    var subState = subStateMachine.GetState(type);

                    // If we found the state
                    if(subState != null) {
                        return subState;
                    }
                }
            }
        }

        return state;
    }

    /// <summary>
    /// Adds a new state into the Dictionary
    /// </summary>
    /// <param name="state">State to be added</param>
    public void AddState(BaseState<TController> state) {
        states.Add(state.GetType(), state);
    }

    /// <summary>
    /// Adds a new any state into the Dictionary
    /// </summary>
    /// <param name="state">State to be added</param>
    public void AddAnyState(BaseState<TController> state) {
        anyStates.Add(state.GetType(), state);
    }

    /// <summary>
    /// Get a sub state machine that in any deeper level, contains the state
    /// Returns null if the target state is not found on any level.
    /// </summary>
    /// <param name="type"></param>
    /// <returns>Returns the child sub state machine that contains the state, or null if fails</returns>
    public BaseStateMachine<TController> GetSubStateMachine(Type type) {
        // Iterate over all states
        foreach(var item in states) {
            // Check if the state is a state machine
            if(item.Value.GetType().IsSubclassOf(typeof(BaseStateMachine<TController>))) {
                // If it is, try to get the 
                var subStateMachine = (BaseStateMachine<TController>)item.Value;
                var state = subStateMachine.states.GetValueOrDefault(type);

                if(state != null) {
                    return subStateMachine;
                }

                return subStateMachine.GetSubStateMachine(type);
            }
        }

        return null;
    }

    /// <summary>
    /// Change current state
    /// If the state is not found on this level,
    /// Checks if exists under any deeper level and change state accordantly
    /// </summary>
    /// <param name="state">Next state</param>
    public void ChangeState(IState state) {
        var previousState = current;
        var nextState = states.GetValueOrDefault(state.GetType());

        // Check if the state is found on this level
        if(nextState != null) {
            // Normal execution
            previousState?.OnStateExit();
            nextState?.OnStateStart();

            current = nextState;

            return;
        }
        
        // Searchs over all sub states to find the target state
        nextState = GetSubStateMachine(state.GetType());

        if(nextState == null) {
            Debug.LogError("State " + state + " not found on Machine " + this);
            Debug.Break();
        }

        // Call this method recursively to proper update states over the levels
        ((BaseStateMachine<TController>)nextState).ChangeState(state);

        previousState?.OnStateExit();
        nextState?.OnStateStart();

        current = nextState;
    }

    /// <summary>
    /// Change to a any state
    /// </summary>
    /// <param name="state"></param>
    public void ChangeAnyState(IState state) {
        var previousState = current;
        var nextState = anyStates.GetValueOrDefault(state.GetType());

        previousState?.OnStateExit();
        nextState?.OnStateStart();

        current = nextState;
    }

    #endregion

    #region State Events

    public override void OnStateStart()
    {
        current?.OnStateStart();
    }

    public override void OnStateUpdate() {
        current?.OnStateUpdate();
    }

    public override void OnStateFixedUpdate() {
        foreach(var item in anyStates) {
            var anyTransition = item.Value.GetAnyTransition();
            if(anyTransition != null) {
                // Check if we already transitioned to this state
                if(current == item.Value) {
                    break;
                }

                Debug.Log("Any Transition in " + this + " from " + current + " to " + item.Value);

                // The target state to a any transition is the state itself
                ChangeAnyState(item.Value);
                return;
            }
        }

        var transition = current.GetTransition();
        if(transition != null) {
            Debug.Log("Transition in " + this + " from " + current + " to " + transition.To);
            ChangeState(transition.To);
        }

        current?.OnStateFixedUpdate();
    }

    public override void OnStateExit()
    {
        current = states.First().Value;
    }

    #endregion

    #region Protected Methods

    /// <summary>
    /// Registers in loop all states transitions
    /// </summary>
    protected void RegisterStateTransitions() {
        foreach(var item in anyStates) {
            item.Value.RegisterTransitions(this);
        }
        
        foreach(var item in states) {
            item.Value.RegisterTransitions(this);
        }
    }

    protected void SetCurrent(Type type) {
        current = states[type];
    }

    #endregion
}