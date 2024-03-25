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
            instance ??= new CardFactory();
            return instance;
        }
    }

    public Card createCard(Player owner, TypeOfCard type, CardColor color, CardEffect? activationEffect, CardEffect[]? effects, CardType cardType = CardType.Instantaneous, int cost = 0, int attack = 0, int defence = 0) {
        switch(type) {
            case TypeOfCard.land:
                return new LandCard(owner, color.ToString() + "_" + type.ToString(), color, CardType.Permanent, activationEffect, effects);
            case TypeOfCard.spell:
                return new SpellCard(owner, color.ToString() + "_" + type.ToString(), color, cardType, activationEffect, effects, cost);
            case TypeOfCard.creature:
                return new CreatureCard(owner, color.ToString() + "_" + type.ToString(), color, CardType.Permanent, activationEffect, effects, cost, attack >= 0 ? attack : 0, defence > 0 ? defence : 1);
            default:
            //  Throw an exception
                throw new ArgumentException("Unknown card type: " + type);
        }
    }
}

public enum TypeOfCard {
    land,
    spell,
    creature
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
    public Player Owner { get; private set; }
    public string Name { get; set; }
    public CardColor CardColor { get; set; }
    public CardState CardState { get; set; }
    public GameState GameState { get; set; }
    public CardEffect ActivationEffect { get; set; }
    public CardEffect[] Effects { get; set; }
    public CardType CardType { get; set; }

    protected Card(Player owner, string name, CardColor cardColor, CardType cardType, CardEffect? activationEffect, CardEffect[]? effects)
    {
        Owner = owner;
        Name = name;
        CardColor = cardColor;
        CardState = new InDeck(this);
        GameState = GameState.getInstance();
        ActivationEffect = activationEffect;
        Effects = effects ?? new CardEffect[0];
        CardType = cardType;
    }
    public abstract bool activate();

    public abstract bool reset();

    public abstract bool discard();
}

public class LandCard : Card
{
    public bool turned { get; set; }

    public LandCard(Player owner, string name, CardColor cardColor, CardType cardType, CardEffect? activationEffect, CardEffect[]? effects) : base(owner, name, cardColor, cardType, activationEffect, effects)
    {}

    public override bool activate()
    {
        return this.CardState.useCard();
    }

    public override bool reset() {
        return this.CardState.reset();
    }

    public override bool discard()
    {
        return this.CardState.discard();
    }
}


public class SpellCard : Card
{
    public int Cost { get; protected set; }
    public SpellCard(Player owner, string name, CardColor cardColor, CardType cardType, CardEffect? activationEffect, CardEffect[]? effects, int cost) : base(owner, name, cardColor, cardType, activationEffect, effects) 
    {
        Cost = cost;
    }
    
    public override bool activate()
    {
        return this.CardState.useCard();
    }
    public override bool reset() {
        return CardState.reset();
    }

    public override bool discard()
    {
        return this.CardState.discard();
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

    public override bool activate()
    {
        return this.CardState.useCard();
    }

    public void attack() {
        this.CardState.useCard();
    }

    public override bool reset() {
        return this.CardState.reset();
    }

    public void increaseAttack(int buff) {
        this.Attack += buff;
    }

    public void increaseDefense(int buff) {
        this.Defense += buff;
    }

    public bool takeDamage(int dmg) {
        this.Defense -= dmg;
        if (this.Defense <= 0) {
            this.CardState = new InDiscard(this);
            return true;
        }
        return false;
    }

    public override bool discard()
    {
        return this.CardState.discard();
    }
}


// public class GameState
// {
//     // Game state implementation
// }
