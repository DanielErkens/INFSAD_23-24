using System.Collections.Generic;

// Enum representing different colors of cards
public enum CardColor
{
    Red,
    Blue,
    Brown,
    White,
    Green
}

// Enum representing different types of cards
public enum CardType
{
    Land,
    Spell,
    Creature,
    Instantaneous
}

// Interface for card effects
public interface ICardEffect
{
    void ApplyEffect(GameState gameState);
}

// Interface for card states
public interface ICardState
{
    void ApplyEffect(GameState gameState);
}

// Base class for all cards
public abstract class Card
{
    public string Name { get; protected set; }
    public CardColor Color { get; protected set; }
    public CardType Type { get; protected set; }

    public ICardState CardState { get; protected set; }

    protected Card(string name, CardColor color, CardType type)
    {
        Name = name;
        Color = color;
        Type = type;
        CardState = new DeactivatedState(); // Initialize with a default state
    }

    // Abstract method for activating the card
    public abstract void Activate(Player owner, GameState gameState);
}

// Class for land cards
public class LandCard : Card
{
    public LandCard(string name, CardColor color) : base(name, color, CardType.Land) { }

    public override void Activate(Player owner, GameState gameState)
    {
        // Lands have no activation effect
    }
}

// Class for spell cards
public class SpellCard : Card
{
    public int Cost { get; protected set; }
    public List<ICardEffect> Effects { get; protected set; }

    public SpellCard(string name, CardColor color, int cost, List<ICardEffect> effects) : base(name, color, CardType.Spell)
    {
        Cost = cost;
        Effects = effects;
    }

    public override void Activate(Player owner, GameState gameState)
    {
        // Apply effects of the spell card
        foreach (var effect in Effects)
        {
            effect.ApplyEffect(gameState);
        }
    }
}

// Class for creature cards
public class CreatureCard : Card
{
    public int Attack { get; protected set; }
    public int Defense { get; protected set; }
    public List<ICardEffect> Effects { get; protected set; }

    public CreatureCard(string name, CardColor color, int attack, int defense, List<ICardEffect> effects) : base(name, color, CardType.Creature)
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

// Class for instantaneous spell cards
public class InstantaneousSpellCard : SpellCard
{
    public InstantaneousSpellCard(string name, CardColor color, int cost, List<ICardEffect> effects) : base(name, color, cost, effects)
    {
        Type = CardType.Instantaneous;
    }
}

// Concrete class representing the activated state of a card
public class ActivatedState : ICardState
{
    public void ApplyEffect(GameState gameState)
    {
        // Apply effect specific to the activated state, if any
    }
}

// Concrete class representing the deactivated state of a card
public class DeactivatedState : ICardState
{
    public void ApplyEffect(GameState gameState)
    {
        // Apply effect specific to the deactivated state, if any
    }
}

// Concrete class representing the discarded state of a card
public class DiscardedState : ICardState
{
    public void ApplyEffect(GameState gameState)
    {
        // Apply effect specific to the discarded state, if any
    }
}

// Concrete class representing the effect of a card being played
public class PlayedState : ICardState
{
    public void ApplyEffect(GameState gameState)
    {
        // Apply effect specific to the played state, if any
    }
}

