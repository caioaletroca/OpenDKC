using System;

public class FunctionPredicate : IPredicate
{
    #region Private Properties

    readonly Func<bool> function;

    #endregion

    #region Constructor

    public FunctionPredicate(Func<bool> function) {
        this.function = function;
    }

    #endregion

    #region Public Methods

    public bool Evaluate() => function.Invoke();

    #endregion
}