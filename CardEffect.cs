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

    public Card BaseCard { get; protected set; }
    public bool IsActive;
    public int TurnsActive { get; protected set; }
    public Target Target { get; protected set; }
    public GameState GameState { get; protected set; }

    protected CardEffect(string name, string activationDescription, int turnPlayed, Card baseCard, bool isActive, int turnsActive, Target target) {
        Name = name;
        ActivationDescription = activationDescription;
        TurnPlayed = turnPlayed;
        BaseCard = baseCard;
        IsActive = isActive;
        TurnsActive = turnsActive;
        Target = target;
        GameState = GameState.getInstance();
    }

    public abstract void applyEffect();
    public abstract void checkActivationCondition();
}

public class AttackEffect : CardEffect {
    public AttackEffect(int turnPlayed, Card BaseCard, bool isActive, int turnsActive, Target target = Target.Other) : base("Attack", "attacks opponent", turnPlayed, BaseCard, isActive, 1, Target.Other) {

    }

    public override void applyEffect()
    {
        int opponent = GameState.getInstance().CurrentTurn%2 == 0 ? 1 : 0;
        CreatureCard creature = BaseCard as CreatureCard;
        int actualAttack = creature.Attack;
        if(GameState.Effects.Any(effect => effect is HalfDamageEffect)) {
            actualAttack = (creature.Attack + 1) / 2;
            Console.WriteLine($"Half damage effect in play thanks to the artifacts effect. Creatures damage reduced from {creature.Attack} to {actualAttack}");
        }
        
        GameState.getInstance().Players[opponent].takeDamage(actualAttack);
    }

    public override void checkActivationCondition()
    {
        if (BaseCard is CreatureCard) {
            GameState.getInstance().Counters.Push(this);
        }
    }
}


public class CounterSpell : CardEffect {
    public CounterSpell(int turnPlayed, Card BaseCard, bool isActive, int turnsActive, Target target) : base("Counter", "Removes last opponent action", turnPlayed, BaseCard, isActive, turnsActive, target) {

    }

    public override void applyEffect()
    {
        GameState.getInstance().Counters.TryPop(out CardEffect result);
        Console.WriteLine($"{BaseCard.CardColor.ToString()} counter spell played. {result.BaseCard.CardColor.ToString()} {result.Name} removed");
    }

    public override void checkActivationCondition()
    {
        // Check if there is an action to counter

        GameState.getInstance().Counters.Push(this);
    }
}


public class BuffCreature : CardEffect {
    public BuffCreature(int turnPlayed, Card BaseCard, bool isActive, int turnsActive, Target target) : base("+3 Creature buff", "Buffs creature", turnPlayed, BaseCard, isActive, turnsActive, target) {

    }

    public override void applyEffect() {
        foreach(Card card in BaseCard.Owner.Permanents) {
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
    public CreatureEffect(int turnPlayed, Card BaseCard, bool isActive, int turnsActive, Target target) : base("Card remover", "removes a card", turnPlayed, BaseCard, isActive, turnsActive, target) {

    }

    public override void applyEffect()
    {
        Player Victim = BaseCard.Owner == GameState.getInstance().Player1 ? GameState.getInstance().Player2 : GameState.getInstance().Player1;  

        Victim.discardCard();   
    }

    public override void checkActivationCondition()
    {
        // Check if effect should still be active 

        GameState.getInstance().Counters.Push(this);
        Player Victim = BaseCard.Owner == GameState.getInstance().Player1 ? GameState.getInstance().Player2 : GameState.getInstance().Player1;  
        Console.WriteLine($"{Victim.Name} has discarded a card thanks to the effect of {BaseCard.Name}");     //Check

    }

}

public class DefenselessEffect : CardEffect{
    public DefenselessEffect(int turnPlayed, Card BaseCard, bool isActive, int turnsActive, Target target = Target.both) : base("No defenses", "defense on creatures is ignored", turnPlayed, BaseCard, isActive, turnsActive, target) {
        
    }
    
    public override void applyEffect() {
        return;
    }
    
    public override void checkActivationCondition() {
        GameState.getInstance().Effects.Add(this);
    }

}

public class HalfDamageEffect : CardEffect{
    public HalfDamageEffect(int turnPlayed, Card BaseCard, bool isActive, int turnsActive, Target target) : base("half damage half fun", "deal and take half damage", turnPlayed, BaseCard, isActive, turnsActive, target) {

    }
    public override void applyEffect() {
        Console.WriteLine("Half dammage effect in effect");
        return;
    }
    
    public override void checkActivationCondition() {
        GameState.getInstance().Effects.Add(this);
    }

}
public class SkipDrawEffect : CardEffect{

    public SkipDrawEffect(int turnPlayed, Card BaseCard, bool isActive, int turnsActive, Target target) : base("skip drawing", "opponent skips drawing phase", turnPlayed, BaseCard, isActive, turnsActive, target) {

    }
    
    public override void applyEffect() {
        if( this.TurnsActive < 1 ) {
            GameState.getInstance().Effects.Remove(this);
            Console.WriteLine($"Skipdraw effect from the artefact already has been played. Skip drawing phase effect is removed from artefact");     //Check

            return;
        }
        this.TurnsActive--;
        return;
    }
    
    public override void checkActivationCondition() {
        GameState.getInstance().Effects.Add(this);
    }

}