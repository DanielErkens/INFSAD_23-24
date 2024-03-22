public abstract class CardState
{
    protected Card card;

    public CardState(Card card)
    {
        this.card = card;
    }

    // Enters when no function is found in the currecnt card state
    public virtual void draw() { Console.WriteLine("Unable to applyEffect"); }

    public virtual void playCard() { Console.WriteLine("Unable to play card"); }

    public virtual void turnLand() { Console.WriteLine("Unable to turn land"); }

    public virtual void restoreLand() { Console.WriteLine("Unable to reset land"); }

    public virtual void activateCard() { Console.WriteLine("Unable to applyEffect"); }

    public virtual Boolean isInPlay() { return false; }


}

public class InDeck : CardState {

    public InDeck(Card card) : base(card) { }

    public override void draw()
    {
        card.CardState = new InHand(this.card);
    }

}

public class InHand : CardState {

    public InHand(Card card) : base(card) { }

    public override void playCard()
    {
        if(card.ActivationEffect != null) {
            card.ActivationEffect.checkActivationCondition();
        }

        card.CardState = new InPlay(this.card);
    }
}

public class InPlay : CardState {

    public InPlay(Card card) : base(card) { }

    public override void activateCard()
    {
        switch(card)
        {
            case LandCard:
                LandCard land = card as LandCard;
                land.Owner.Energy[land.CardColor] +=1;
                land.turned = true;
                break;
            case SpellCard:
                SpellCard spell = card as SpellCard;
                spell.ActivationEffect.checkActivationCondition();
                break;
            case CreatureCard:
                // TODO add attack as effect and add to counter stack
                CreatureCard creature = card as CreatureCard;
                break;
            default:
                throw new ArgumentException("Unknown card type: ");
        }
    }
}

public class InDiscard : CardState {

    public InDiscard(Card card) : base(card) { }

}

// public class NotPlayedState : CardState {

//     public NotPlayedState(Card card) : base(card) { }

//     public override void activateCard() {
//         // Apply the effect of the card
//         System.Console.WriteLine("Applying effect");
//     }

//     public override void playCard() {
//         this.card.CardState = new PlayedState(this.card);
//     }

//     public override void restoreLand() {
//         System.Console.WriteLine("Resetting");
//     }
// }

// public class TurnedState : CardState {

//     public TurnedState(Card card) : base(card) { }

//     public override void activateCard() {
//         System.Console.WriteLine("Applying effect");
//     }

//     public override void restoreLand() {
//         System.Console.WriteLine("Resetting");
//     }
// }