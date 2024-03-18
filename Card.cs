using System.Collections.Generic;


public class CardFactory {

    private static CardFactory instance;

    private CardFactory()
    {
    }

    public static CardFactory Instance
    {
        get
        {
            if (instance == null)
                instance = new CardFactory();
            return instance;
        }
    }

    public Card createCard(Player owner, string type, CardColor color, CardType cardType, CardEffect? activationEffect, CardEffect[]? effects, int cost = 0, int attack = 0, int defence = 0) {
        switch(type.ToLower()) {
            case "land":
                return new LandCard(owner, color.ToString() + "_" + type.ToLower(), color, cardType, activationEffect, effects);
            case "spell":
                return new SpellCard(owner, color.ToString() + "_" + type.ToLower(), color, cardType, activationEffect, effects, cost);
            case "creature":
                return new CreatureCard(owner, color.ToString() + "_" + type.ToLower(), color, cardType, activationEffect, effects, cost, attack, defence);
            default:
            //  Throw an exception
                throw new ArgumentException("Unknown card type: " + type);
        }
    }
}

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
    public Player owner { get; private set; }
    public string Name { get; set; }
    public CardColor CardColor { get; set; }
    public CardState CardState { get; set; }
    public GameState GameState { get; set; }
    public CardEffect ActivationEffect { get; set; }
    public CardEffect[] Effects { get; set; }
    public CardType CardType { get; set; }

    protected Card(Player owner, string name, CardColor cardColor, CardType cardType, CardEffect? activationEffect, CardEffect[]? effects)
    {
        owner = owner;
        Name = name;
        CardColor = cardColor;
        CardState = new NotPlayedState(this);
        GameState = GameState.getInstance();
        ActivationEffect = activationEffect;
        Effects = effects ?? new CardEffect[0];
        CardType = cardType;
    }
    public abstract void activate();
}

public class LandCard : Card
{
    public bool turned { get; set; }

    public LandCard(Player owner, string name, CardColor cardColor, CardType cardType, CardEffect? activationEffect, CardEffect[]? effects) : base(owner, name, cardColor, cardType, activationEffect, effects)
    {}

    public override void activate()
    {
        // Change turned state here
    }

    public void reset() {
        this.CardState.reset();
    }
}


public class SpellCard : Card
{
    public int Cost { get; protected set; }
    public SpellCard(Player owner, string name, CardColor cardColor, CardType cardType, CardEffect? activationEffect, CardEffect[]? effects, int cost) : base(owner, name, cardColor, cardType, activationEffect, effects) 
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

    public CreatureCard(Player owner, string name, CardColor cardColor, CardType cardType, CardEffect? activationEffect, CardEffect[]? effects, int cost, int attack, int defense) : base(owner, name, cardColor, cardType, activationEffect, effects)
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
