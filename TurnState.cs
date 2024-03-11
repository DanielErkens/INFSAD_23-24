public interface ITurnState { //Implement here state design pattern and Composite pattern
    void PlayPhase();
    void UpdateCardEffectIsActive();
}

public class PreparationState : ITurnState {
    public GameState GameState;
    public PreparationState() {
        GameState = GameState.getInstance();
    }

    public void PlayPhase() {}

    public void UpdateCardEffectIsActive() {}
}

public class DrawingState : ITurnState {
    public GameState GameState;
    public DrawingState() {
        GameState = GameState.getInstance();
    }
    public void PlayPhase() {}
    public void UpdateCardEffectIsActive() {}
}

public class MainState : ITurnState { //Implement pub-sub here
    public GameState GameState;
    public MainState() {
        GameState = GameState.getInstance();
    }
    public void PlayPhase() {}
    public void UpdateCardEffectIsActive() {}
}

public class EndingState : ITurnState {
    public GameState GameState;
    public EndingState() {
        GameState = GameState.getInstance();
    }
    public void PlayPhase() {}
    public void UpdateCardEffectIsActive() {}
    public bool checkWinCondition() {return false;}
}