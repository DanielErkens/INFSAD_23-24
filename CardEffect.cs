using System.Runtime;
using System.Security;

public enum Target
{
    Self,
    Other,
    both,
}

public abstract class CardEffect {
    public string Name { get; protected set; }
    public string ActivationDescription { get; protected set; }
    public int TurnPlayed { get; protected set; }
    public Player Owner { get; protected set; }
    public bool IsActive;
    public int TurnsActive { get; protected set; }
    public Target Target { get; protected set; }
    public GameState GameState { get; protected set; }

    protected CardEffect(string name, string activationDescription, int turnPlayed, Player owner, bool isActive, int turnsActive, Target target) {
        Name = name;
        ActivationDescription = activationDescription;
        TurnPlayed = turnPlayed;
        Owner = owner;
        IsActive = isActive;
        TurnsActive = turnsActive;
        Target = target;
        GameState = GameState.getInstance();
    }

    public abstract void applyEffect();
    public abstract void checkActivationCondition();
}