using System;

public class FunctionPredicate : IPredicate
{
    readonly Func<bool> function;

    public FunctionPredicate(Func<bool> function) {
        this.function = function;
    }

    public bool Evaluate() => function.Invoke();
}