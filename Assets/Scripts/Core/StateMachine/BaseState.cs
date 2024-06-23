using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState<TController> : IState {
    #region Protected Variables

    /// <summary>
    /// Main controller of this state
    /// </summary>
    protected TController controller;

    /// <summary>
    /// Animator instance
    /// </summary>
    protected Animator animator;

    /// <summary>
    /// Hash set with stored transitions
    /// </summary>
    /// <value></value>
    protected HashSet<ITransition> Transitions { get; }

    #endregion

    #region Constructor

    public BaseState(TController controller, Animator animator) {
        this.controller = controller;
        this.animator = animator;
        Transitions = new HashSet<ITransition>();
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Adds a new transition
    /// </summary>
    /// <param name="to">Target state</param>
    /// <param name="condition">Condition</param>
    public void AddTransition(IState to, IPredicate condition) {
        Transitions.Add(new Transition(to, condition));
    }

    /// <summary>
    /// Adds a new any transition
    /// </summary>
    /// <param name="condition">Condition</param>
    public void AddAnyTransition(IPredicate condition) {
        Transitions.Add(new AnyTransition(condition));
    }

    /// <summary>
    /// Evaluate all transitions, If one returns true, the method returns the transition.
    /// Else, returns null.
    /// </summary>
    /// <returns></returns>
    public ITransition GetTransition() {
        foreach(var transition in Transitions) {
            if(transition is Transition) {
                if(transition.Condition.Evaluate()) {
                    return transition;
                }
            }
        }

        return null;
    }

    /// <summary>
    /// Evaluate all any transitions, If one returns true, the method returns the transition.
    /// Else, returns null.
    /// </summary>
    /// <returns></returns>
    public ITransition GetAnyTransition() {
        foreach(var transition in Transitions) {
            if(transition is AnyTransition) {
                if(transition.Condition.Evaluate()) {
                    return transition;
                }
            }
        }

        return null;
    }

    public virtual void RegisterTransitions(BaseStateMachine<TController> stateMachine) {}

    #endregion
    
    #region State Events

    public virtual void OnStateStart() {}

    public virtual void OnStateUpdate() {}

    public virtual void OnStateFixedUpdate() {}

    public virtual void OnStateExit() {}

    #endregion
}