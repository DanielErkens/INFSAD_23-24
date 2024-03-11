using System.Collections.Generic;

public enum CardColor
{
    Red,
    Blue,
    Brown,
    White,
    Green,
}

public enum CardType 
{
    Instantaneous,
    Permanent,
}

public abstract class Card
{
    public string Name { get; set; }
    public CardColor CardColor { get; set; }
    public CardState CardState { get; set; }
    public GameState GameState { get; set; }
    public CardEffect ActivationEffect { get; set; }
    public CardEffect[] Effects { get; set; }
    public CardType CardType { get; set; }

    protected Card(string name, CardColor cardColor, CardState cardState, GameState gameState, CardEffect activationEffect, CardEffect[] effects, CardType cardType)
    {
        Name = name;
        CardColor = cardColor;
        CardState = cardState;
        GameState = gameState;
        ActivationEffect = activationEffect;
        Effects = effects;
        CardType = cardType;
    }
    public abstract void activate();
}

public class LandCard : Card
{
    public LandCard(string name, CardColor cardColor, CardState cardState, GameState gameState, CardEffect activationEffect, CardEffect[] effects, CardType cardType) : base(name, cardColor, cardState, gameState, activationEffect, effects, cardType)
    {}

    public override void activate()
    {
        // Change turned state here
    }
}


public class SpellCard : Card
{
    public int Cost { get; protected set; }
    public SpellCard(string name, CardColor cardColor, CardState cardState, GameState gameState, CardEffect activationEffect, CardEffect[] effects, CardType cardType, int cost) : base(name, cardColor, cardState, gameState, activationEffect, effects, cardType) 
    {
        Cost = cost;
    }
    
    public override void activate()
    {
        
    }
}

public class CreatureCard : Card
{
    public int Attack { get; protected set; }
    public int Defense { get; protected set; }
    public int Cost { get; protected set; }

    public CreatureCard(string name, CardColor cardColor, CardState cardState, GameState gameState, CardEffect activationEffect, CardEffect[] effects, CardType cardType, int attack, int defense, int cost) : base(name, cardColor, cardState, gameState, activationEffect, effects, cardType)
    {
        Attack = attack;
        Defense = defense;
        Cost = cost;
    }

    public override void activate()
    {
        // Apply effects of the creature card
    }
}


// public class GameState
// {
//     // Game state implementation
// }
