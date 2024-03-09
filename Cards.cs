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

public interface ICardEffect
{
    void ApplyEffect(GameState gameState);
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

    protected Card(string name, CardColor color, CardKind kind, int copies)
    {
        Name = name;
        Color = color;
        Kind = kind;
        Copies = copies;
        CardState = new DeactivatedState();
    }

    public abstract void Activate(Player owner, GameState gameState);
}

public class LandCard : Card
{
    public LandCard(string name, CardColor color) : base(name, color, CardKind.Land, 3) { }

    public override void Activate(Player owner, GameState gameState)
    {
        // Lands have no activation effect
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
            // Create a permanent card from the spell card
            var permanentCard = new CreatureCard(Name, Color, 0, 0, Effects);
            owner.AddPermanentCard(permanentCard);
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

    public Player()
    {
        Deck = new List<Card>();
        Hand = new List<Card>();
        Graveyard = new List<Card>();
        Permanents = new List<Card>();
    }

    public void AddPermanentCard(Card card)
    {
        if (Permanents.Count < 3)
        {
            Permanents.Add(card);
        }
    }
}

public class GameState
{
    // Game state implementation
}
