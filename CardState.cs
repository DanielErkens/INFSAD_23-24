public interface CardState {
    public void applyEffect();
}

public class ActivatedState : CardState {
    public void applyEffect() {
        // Apply the effect of the card
    }
}

public class DeactivatedState : CardState {
    public void applyEffect() {
        // Do nothing
    }
}

public class DiscardedState : CardState {
    public void applyEffect() {
        // Remove effect from stack of effects in gamestate
    }
}

public class PlayedState : CardState {
    public void applyEffect() {
        // Apply the effect of the card
    }
}

public class TurnedState : CardState {
    public void applyEffect() {
        // Change the state of the card
    }
}