using System;
using System.Collections.Generic;
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
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public BaseState<TController> GetStateByType(Type type) {
        return states[type];
    }

    /// <summary>
    /// Adds a new state into the Dictionary
    /// </summary>
    /// <param name="state">State to be added</param>
    public void AddState(BaseState<TController> state) {
        states.Add(state.GetType(), state);
    }

    #endregion

    #region State Events

    public override void OnStateUpdate() {
        var transition = current.GetTransition();
        if(transition != null) {
            ChangeState(transition.To);
        }

        current?.OnStateUpdate();
    }

    public override void OnStateFixedUpdate() {
        current?.OnStateFixedUpdate();
    }

    #endregion

    #region Protected Methods

    /// <summary>
    /// Registers in loop all states transitions
    /// </summary>
    protected void RegisterStateTransitions() {
        foreach(var item in states) {
            item.Value.RegisterTransitions(this);
        }
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Change current state
    /// </summary>
    /// <param name="state">Next state</param>
    void ChangeState(IState state) {
        var previousState = current;
        var nextState = states[state.GetType()];

        previousState?.OnStateExit();
        nextState?.OnStateStart();

        current = states[state.GetType()];
    }

    #endregion
}