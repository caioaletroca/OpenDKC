public class AnyTransition : ITransition {
    public IState To { get; }
    public IPredicate Condition { get; }

    public AnyTransition(IPredicate condition) {
        Condition = condition;
    }
}