public interface ITurnState { //Implement here state design pattern and Composite pattern
    void PlayPhase();
    void UpdateCardEffectIsActive();
}

public class PreparationState : ITurnState {

    public PreparationState() {
    }

    public void PlayPhase() {
        System.Console.WriteLine("inside prep phase");

        // reset lands
        foreach(Card card in GameState.getInstance().Player1.Permanents) {
            card.reset();
            // if (card is LandCard) {
            //     ((LandCard)card).turned = false;
            // }
        }

        foreach(Card card in GameState.getInstance().Player2.Permanents) {
            // if (card is LandCard) {
            //     ((LandCard)card).turned = false;
            // }
        }

        UpdateCardEffectIsActive();
    
        GameState.getInstance().TurnState = new DrawingState();

    }

    public void UpdateCardEffectIsActive() {
        // effects into play
    }


}

public class DrawingState : ITurnState {
    public DrawingState() {
    }

    public void PlayPhase() {
        System.Console.WriteLine("inside drawing phase");
        
        UpdateCardEffectIsActive();

        GameState.getInstance().TurnState = GameState.getInstance().Players[(GameState.getInstance().CurrentTurn % 2)].Deck.Count <= 0 ? new GameOverState() : new MainState();

    }

    public void UpdateCardEffectIsActive() {}


}

public class MainState : ITurnState { //Implement pub-sub here
    public MainState() {
    }

    public void PlayPhase() {
        System.Console.WriteLine("inside main phase");

        UpdateCardEffectIsActive();

        GameState.getInstance().TurnState = new EndingState();

    }

    public void UpdateCardEffectIsActive() {}

}

public class EndingState : ITurnState {

    public EndingState() {
    }

    public void PlayPhase() {
        System.Console.WriteLine("inside ending phase");
        if (GameState.getInstance().Player1.Hand.Count > 7)
            GameState.getInstance().Player1.trimCards();

        if(GameState.getInstance().Player2.Hand.Count > 7)  
            GameState.getInstance().Player2.trimCards();

        UpdateCardEffectIsActive();
        
        GameState.getInstance().CurrentTurn += 1;
        GameState.getInstance().TurnState = new PreparationState();
    }

    public void UpdateCardEffectIsActive() {}

    public bool checkWinCondition() {return false;}

}

public class GameOverState : ITurnState {

    public GameOverState() {
    }
    
    public void PlayPhase() {
        System.Console.WriteLine("inside gameover phase");
    }

    public void UpdateCardEffectIsActive() {}

    public bool checkWinCondition() {return false;}
}