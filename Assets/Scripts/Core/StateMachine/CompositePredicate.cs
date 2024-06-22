using System.Linq;

public class CompositePredicate : IPredicate
{
    #region Types

    public enum Operation {
        AND,
        OR
    }

    #endregion

    #region Private Properties

    /// <summary>
    /// List of all nested predicates
    /// </summary>
    readonly IPredicate[] predicates;

    /// <summary>
    /// Operation to perform, defaults to AND
    /// </summary>
    readonly Operation operation;

    #endregion

    #region Constructor

    public CompositePredicate(IPredicate[] predicates, Operation operation = Operation.AND)
    {
        this.predicates = predicates;
        this.operation = operation;
    }

    #endregion

    #region Public Methods

    public bool Evaluate()
    {
        if(operation == Operation.OR) {
            return predicates.Any(item => item.Evaluate());
        }

        return predicates.All((item) => item.Evaluate());
    }

    #endregion
}