public abstract class CardState
{
    protected Card card;

    public CardState(Card card)
    {
        this.card = card;
    }

    // Enters when no function is found in the currecnt card state
    public virtual void applyEffect() { Console.WriteLine("Unable to applyEffect"); }
    public virtual void reset() { Console.WriteLine("Unable to reset"); }
    public virtual void playCard() { Console.WriteLine("Unable to play card"); }
}

public class ActivatedState : CardState {

    public ActivatedState(Card card) : base(card) { }

    public void applyEffect() {
        // Apply the effect of the card
        System.Console.WriteLine("Applying effect");
    }

    public override void reset() {
        System.Console.WriteLine("Resetting");
    }
}

public class DeactivatedState : CardState {

    public DeactivatedState(Card card) : base(card) { }

    public override void applyEffect() {
        // Do nothing
        System.Console.WriteLine("Applying effect");
    }

    public override void reset() {
        System.Console.WriteLine("Resetting");
    }
}

public class DiscardedState : CardState {

    public DiscardedState(Card card) : base(card) { }

    public override void applyEffect() {
        // Remove effect from stack of effects in gamestate
        System.Console.WriteLine("Applying effect");
    }

    public override void reset() {
        System.Console.WriteLine("Resetting");
    }
}

public class PlayedState : CardState {

    public PlayedState(Card card) : base(card) { }

    public override void applyEffect() {
        // Apply the effect of the card
        System.Console.WriteLine("Applying effect");
    }

    public override void reset() {
        System.Console.WriteLine("Resetting");
    }
}

public class NotPlayedState : CardState {

    public NotPlayedState(Card card) : base(card) { }

    public override void applyEffect() {
        // Apply the effect of the card
        System.Console.WriteLine("Applying effect");
    }

    public override void playCard() {
        this.card.CardState = new PlayedState(this.card);
    }

    public override void reset() {
        System.Console.WriteLine("Resetting");
    }
}

public class TurnedState : CardState {

    public TurnedState(Card card) : base(card) { }

    public override void applyEffect() {
        System.Console.WriteLine("Applying effect");
    }

    public override void reset() {
        System.Console.WriteLine("Resetting");
    }
}