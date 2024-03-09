using System.Collections.Generic;

public enum CardColor
{
    Red,
    Blue,
    Brown,
    White,
    Green,
    Colorless
}

public enum CardKind
{
    Land,
    Spell,
    Creature
}

public enum EffectType
{
    IncreaseAttack,
    IncreaseDefense,
    // Add more effect types as needed
}

public enum Target
{
    Self,
    Other,
    // Add more targets as needed
}

public class Effect
{
    public EffectType Type { get; set; }
    public Target Target { get; set; }
    public int Duration { get; set; }
    public int Value { get; set; }
    
    public bool IsActive { get; private set; }
    private int remainingDuration;

    public Effect(EffectType type, Target target, int duration, int value)
    {
        Type = type;
        Target = target;
        Duration = duration;
        Value = value;
        IsActive = true;
        remainingDuration = duration;
    }

    // Method to apply the effect to the game state
    public void ApplyEffect(GameState gameState)
    {
        // Apply the effect to the game state
        // Logic to modify the game state based on the effect type and target

        // Decrement remaining duration
        remainingDuration--;

        // Check if the effect duration has expired
        if (remainingDuration <= 0)
        {
            IsActive = false; // Deactivate the effect if its duration has expired
        }
    }
}

public interface ICardEffect
{
    void ApplyEffect(GameState gameState);
    Effect GetEffect();
}

public interface ICardState
{
    void ApplyEffect(GameState gameState);
}

public abstract class Card
{
    public string Name { get; protected set; }
    public CardColor Color { get; protected set; }
    public CardKind Kind { get; protected set; }
    public int Copies { get; protected set; }

    public ICardState CardState { get; protected set; }

    public abstract void ApplyEffect(GameState gameState);

    public abstract Effect GetEffect();
}

public class LandCard : Card
{
    public bool IsTurned { get; private set; }

    public LandCard(string name, CardColor color) : base(name, color, CardKind.Land, 3)
    {
        IsTurned = false;
    }

    public override void Activate(Player owner, GameState gameState)
    {
        // Lands have no activation effect
    }

    public void TurnLand(Player owner, GameState gameState)
    {
        // Check if it's the owner's turn and the land hasn't been turned yet
        if (owner.IsTurn && !IsTurned)
        {
            // Mark the land as turned
            IsTurned = true;

            // Obtain energy of the appropriate color
            owner.ObtainEnergy(Color);

            // Prevent the land from being reused until the next beginning of the owner's turn
            owner.AddUsedLand(this);
        }
    }
}


public class SpellCard : Card
{
    public int Cost { get; protected set; }
    public List<ICardEffect> Effects { get; protected set; }
    public bool IsInstantaneous { get; protected set; }

    public SpellCard(string name, CardColor color, int cost, List<ICardEffect> effects, bool isInstantaneous) : base(name, color, CardKind.Spell, 3)
    {
        Cost = cost;
        Effects = effects;
        IsInstantaneous = isInstantaneous;
    }

    public override void Activate(Player owner, GameState gameState)
    {
        if (IsInstantaneous)
        {
            // Apply effects of the instantaneous spell card
            foreach (var effect in Effects)
            {
                effect.ApplyEffect(gameState);
            }
        }
        else
        {
            var permanentCard = new CreatureCard(Name, Color, 0, 0, Effects);
            owner.AddPermanentCard(permanentCard);

            // Apply effects with durations to the game state
            foreach (var effect in Effects)
            {
                if (effect.Duration > 1)
                {
                    // Clone the effect and decrement its duration
                    var clonedEffect = new Effect(effect.Type, effect.Target, effect.Duration - 1, effect.Value);
                    gameState.AddEffect(clonedEffect);
                }
            }
        }
    }
}

public class CreatureCard : Card
{
    public int Attack { get; protected set; }
    public int Defense { get; protected set; }
    public List<ICardEffect> Effects { get; protected set; }

    public CreatureCard(string name, CardColor color, int attack, int defense, List<ICardEffect> effects) : base(name, color, CardKind.Creature, 3)
    {
        Attack = attack;
        Defense = defense;
        Effects = effects;
    }

    public override void Activate(Player owner, GameState gameState)
    {
        // Apply effects of the creature card
        foreach (var effect in Effects)
        {
            effect.ApplyEffect(gameState);
        }
    }
}

public class InstantaneousSpellCard : SpellCard
{
    public InstantaneousSpellCard(string name, CardColor color, int cost, List<ICardEffect> effects) : base(name, color, cost, effects, true)
    {
    }
}

public class ActivatedState : ICardState
{
    public void ApplyEffect(GameState gameState)
    {
        // Apply effect specific to the activated state, if any
    }
}

public class DeactivatedState : ICardState
{
    public void ApplyEffect(GameState gameState)
    {
        // Apply effect specific to the deactivated state, if any
    }
}

public class DiscardedState : ICardState
{
    public void ApplyEffect(GameState gameState)
    {
        // Apply effect specific to the discarded state, if any
    }
}

public class PlayedState : ICardState
{
    public void ApplyEffect(GameState gameState)
    {
        // Apply effect specific to the played state, if any
    }
}

public class Player
{
    public List<Card> Deck { get; set; }
    public List<Card> Hand { get; set; }
    public List<Card> Graveyard { get; set; }
    public List<Card> Permanents { get; set; }
    public List<LandCard> UsedLands { get; set; } // Track lands that have been turned

    public Player()
    {
        Deck = new List<Card>();
        Hand = new List<Card>();
        Graveyard = new List<Card>();
        Permanents = new List<Card>();
        UsedLands = new List<LandCard>();
    }

    public void AddPermanentCard(Card card)
    {
        if (Permanents.Count < 3)
        {
            Permanents.Add(card);
        }
    }

    public void ObtainEnergy(CardColor color)
    {
        // Add energy of the appropriate color to the player's energy reserve
        // Implementation depends on your game's energy management system
    }

    public void AddUsedLand(LandCard land)
    {
        UsedLands.Add(land);
    }

    public bool IsTurn
    {
        get
        {
            // Check if it's the player's turn
            // Implementation depends on your game's turn management system
        }
    }

    public void ResetTurn()
    {
        // Reset the state of lands at the beginning of the turn
        foreach (var land in UsedLands)
        {
            land.IsTurned = false;
        }
        UsedLands.Clear();
    }
}


public class GameState
{
    // Game state implementation
}
