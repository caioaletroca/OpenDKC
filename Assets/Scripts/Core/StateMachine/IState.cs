public interface IState {
    void OnStateStart();

    void OnStateUpdate();

    void OnStateFixedUpdate();

    void OnStateExit();
}