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

    public Card Owner { get; protected set; }
    public bool IsActive;
    public int TurnsActive { get; protected set; }
    public Target Target { get; protected set; }
    public GameState GameState { get; protected set; }

    protected CardEffect(string name, string activationDescription, int turnPlayed, Card owner, bool isActive, int turnsActive, Target target) {
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

public class attackEffect : CardEffect {
    public attackEffect(int turnPlayed, Card owner, bool isActive, int turnsActive, Target target = Target.Other) : base("Attack", "attacks opponent", turnPlayed, owner, isActive, 1, Target.Other) {

    }

    public override void applyEffect()
    {
        int opponent = GameState.getInstance().CurrentTurn%2 == 0 ? 1 : 0;
        CreatureCard creature = Owner as CreatureCard;
        GameState.getInstance().Players[opponent].takeDamage(creature.Attack);
    }

    public override void checkActivationCondition()
    {
        if (Owner is CreatureCard) {
            GameState.getInstance().Counters.Push(this);
        }
    }
}


public class counterSpell : CardEffect {
    public counterSpell(int turnPlayed, Card owner, bool isActive, int turnsActive, Target target) : base("Counter", "Removes last opponent action", turnPlayed, owner, isActive, turnsActive, target) {

    }

    public override void applyEffect()
    {
        GameState.getInstance().Counters.TryPop(out CardEffect result);
    }

    public override void checkActivationCondition()
    {
        // Check if there is an action to counter

        GameState.getInstance().Counters.Push(this);
    }
}


public class buffCreature : CardEffect {
    public buffCreature(int turnPlayed, Card owner, bool isActive, int turnsActive, Target target) : base("+3 Creature buff", "Buffs creature", turnPlayed, owner, isActive, turnsActive, target) {

    }

    public override void applyEffect() {
        foreach(Card card in Owner.Owner.Permanents) {
            if (card is CreatureCard) {
                CreatureCard temp = card as CreatureCard;
                temp.increaseAttack(3);
                temp.increaseDefense(3); 
            } 
        }
    }
    public override void checkActivationCondition() {
        // Check if effect should still be active 

        // Check if creatures exist to apply effect to
        
        GameState.getInstance().Counters.Push(this);
    }
}

public class CreatureEffect : CardEffect{
    public CreatureEffect(int turnPlayed, Card owner, bool isActive, int turnsActive, Target target) : base("Card remover", "removes a card", turnPlayed, owner, isActive, turnsActive, target) {

    }

    public override void applyEffect()
    {
        Player Victim = Owner.Owner == GameState.getInstance().Player1 ? GameState.getInstance().Player2 : GameState.getInstance().Player1;  

        Victim.discardCard();        
    }

    public override void checkActivationCondition()
    {
        // Check if effect should still be active 

        GameState.getInstance().Counters.Push(this);

    }

}