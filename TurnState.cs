using System.Dynamic;

public interface ITurnState { //Implement here state design pattern and Composite pattern
    void PlayPhase();

    // Play board effects (removed as not part of assignemtn anymore)
    // void UpdateCardEffectIsActive();
}

public class PreparationState : ITurnState {

    public PreparationState() {
    }

    public void PlayPhase() {
        // System.Console.WriteLine("inside prep phase");

        GameState.getInstance().Player1.Energy = new Dictionary<CardColor, int>();
        GameState.getInstance().Player1.Energy.Add(CardColor.Blue, 0);
        GameState.getInstance().Player1.Energy.Add(CardColor.Brown, 0);
        GameState.getInstance().Player1.Energy.Add(CardColor.Red, 0);
        GameState.getInstance().Player1.Energy.Add(CardColor.Green, 0);
        GameState.getInstance().Player1.Energy.Add(CardColor.White, 0);
        GameState.getInstance().Player2.Energy = new Dictionary<CardColor, int>();
        GameState.getInstance().Player2.Energy.Add(CardColor.Blue, 0);
        GameState.getInstance().Player2.Energy.Add(CardColor.Brown, 0);
        GameState.getInstance().Player2.Energy.Add(CardColor.Red, 0);
        GameState.getInstance().Player2.Energy.Add(CardColor.Green, 0);
        GameState.getInstance().Player2.Energy.Add(CardColor.White, 0);

        // reset lands
        foreach(Card card in GameState.getInstance().Player1.Permanents) {
            card.reset();
        }

        foreach(Card card in GameState.getInstance().Player2.Permanents) {
            card.reset();
        }

        // UpdateCardEffectIsActive();
    
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
        // System.Console.WriteLine("inside drawing phase");
        
        // UpdateCardEffectIsActive();

        GameState.getInstance().TurnState = GameState.getInstance().Players[GameState.getInstance().CurrentTurn % 2].Deck.Count <= 0 ? new GameOverState() : new MainState();

    }

    // public void UpdateCardEffectIsActive() {}


}

public class MainState : ITurnState { //Implement pub-sub here
    public MainState() {
    }

    public void PlayPhase() {
        // System.Console.WriteLine("inside main phase");

        // UpdateCardEffectIsActive();

        int temp = GameState.getInstance().getCounterLength(); 
        GameState.getInstance().counter();
        GameState.getInstance().CounterTurn = 0;

        GameState.getInstance().TurnState = new EndingState();

    }

    // public void UpdateCardEffectIsActive() {}

}

public class EndingState : ITurnState {

    public EndingState() {
    }

    public void PlayPhase() {
        while(GameState.getInstance().Counters.Count() > 0) {
            CardEffect resolve = GameState.getInstance().Counters.Pop();
            resolve.applyEffect();
        }

        // System.Console.WriteLine("inside ending phase");
        if (GameState.getInstance().Player1.Hand.Count > 7)
            GameState.getInstance().Player1.trimCards();

        if(GameState.getInstance().Player2.Hand.Count > 7)  
            GameState.getInstance().Player2.trimCards();

        // UpdateCardEffectIsActive();
        
        GameState.getInstance().CurrentTurn += 1;
        GameState.getInstance().TurnState = new PreparationState();
    }

    // public void UpdateCardEffectIsActive() {}

    public bool checkWinCondition() {return false;}

}

public class GameOverState : ITurnState {

    public GameOverState() {
    }
    
    public void PlayPhase() {
        // System.Console.WriteLine("inside gameover phase");
    }

    // public void UpdateCardEffectIsActive() {}

    public bool checkWinCondition() {return false;}
}