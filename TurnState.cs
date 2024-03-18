public interface ITurnState { //Implement here state design pattern and Composite pattern
    void PlayPhase();
    void UpdateCardEffectIsActive();
}

public class PreparationState : ITurnState {
    public GameState GameState;
    public PreparationState() {
        GameState = GameState.getInstance();
    }

    public void PlayPhase() {
        System.Console.WriteLine("inside prep phase");

        // reset lands
        foreach(Card card in GameState.Player1.Permanents) {
            if (card is LandCard) {
                ((LandCard)card).turned = false;
            }
        }

        foreach(Card card in GameState.Player2.Permanents) {
            if (card is LandCard) {
                ((LandCard)card).turned = false;
            }
        }
    
        GameState.TurnState = new DrawingState();

    }

    public void UpdateCardEffectIsActive() {}


}

public class DrawingState : ITurnState {
    public GameState GameState;
    public DrawingState() {
        GameState = GameState.getInstance();
    }

    public void PlayPhase() {
        System.Console.WriteLine("inside drawing phase");
        
        GameState.TurnState = GameState.Players[(GameState.CurrentTurn % 2)].Deck.Count <= 0 ? new GameOverState() : new MainState();

    }

    public void UpdateCardEffectIsActive() {}


}

public class MainState : ITurnState { //Implement pub-sub here
    public GameState GameState;
    public MainState() {
        GameState = GameState.getInstance();
    }

    public void PlayPhase() {
        System.Console.WriteLine("inside main phase");

        GameState.TurnState = new EndingState();

    }

    public void UpdateCardEffectIsActive() {}

}

public class EndingState : ITurnState {
    public GameState GameState;
    public EndingState() {
        GameState = GameState.getInstance();
    }

    public void PlayPhase() {
        System.Console.WriteLine("inside ending phase");
        if (GameState.Player1.Hand.Count > 7)
            GameState.Player1.trimCards();

        if(GameState.Player2.Hand.Count > 7)  
            GameState.Player2.trimCards();

        GameState.TurnState = new PreparationState();
    }

    public void UpdateCardEffectIsActive() {}

    public bool checkWinCondition() {return false;}

}

public class GameOverState : ITurnState {
    public GameState GameState;
    public GameOverState() {
        GameState = GameState.getInstance();
    }
    
    public void PlayPhase() {
        System.Console.WriteLine("inside gameover phase");
    }

    public void UpdateCardEffectIsActive() {}

    public bool checkWinCondition() {return false;}
}