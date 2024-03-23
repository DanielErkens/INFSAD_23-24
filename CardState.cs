using System.Runtime.InteropServices;

public abstract class CardState
{
    protected Card card;

    public CardState(Card card)
    {
        this.card = card;
    }

    // Enters when no function is found in the currecnt card state
    public virtual void useCard() { Console.WriteLine("Unable to activate"); }

    public virtual void reset() { Console.WriteLine("Unable to reset land"); }

    public virtual void discard() { Console.WriteLine("Unable to discard"); }

}

public class InDeck : CardState {

    public InDeck(Card card) : base(card) { }

    public override void useCard()
    {
        card.CardState = new InHand(this.card);
    }

    public override void discard()
    {
        card.CardState = new InDiscard(card);
    }

}

public class InHand : CardState {

    public InHand(Card card) : base(card) { }

    public override void useCard()
    {
        if(card.ActivationEffect != null) {
            card.ActivationEffect.checkActivationCondition();
        }

        // check cost

        switch(card)
        {
            case LandCard:
                LandCard land = card as LandCard;
                card.CardState = new InPlay(this.card);
                break;

            case SpellCard:
                SpellCard spell = card as SpellCard;
                if(card.Owner.payEnergy(spell.CardColor, spell.Cost)) {
                    card.CardState = new InPlay(this.card);
                }
                else {
                    Console.WriteLine("Unable to play. you're broke");
                }
                break;

            case CreatureCard:
                CreatureCard creature = card as CreatureCard;
                if(card.Owner.payEnergy(creature.CardColor, creature.Cost)) {
                    card.CardState = new InPlay(this.card);
                }
                else {
                    Console.WriteLine("Unable to play. you're broke");
                }
                break;

            default:
                throw new ArgumentException("Unknown card type: ");
        }

    }

    public override void discard()
    {
        card.CardState = new InDiscard(card);
    }
}

public class InPlay : CardState {

    public InPlay(Card card) : base(card) { }

    public override void useCard()
    {
        switch(card)
        {
            case LandCard:
                LandCard land = card as LandCard;

                if (!land.turned) {
                    land.Owner.Energy[land.CardColor] +=1;
                }

                land.turned = true;

                break;

            case SpellCard:

                SpellCard spell = card as SpellCard;

                foreach(CardEffect f in spell.Effects) {
                    f.checkActivationCondition();
                }

                if (spell.CardType == CardType.Instantaneous) {
                    spell.CardState = new InDiscard(card);
                }

                break;

            case CreatureCard:
            
                CreatureCard creature = card as CreatureCard;

                GameState.getInstance().Counters.Push( new attackEffect( GameState.getInstance().CurrentTurn, this.card, false, 0 ) );

                break;

            default:
                throw new ArgumentException("Unknown card type: ");
        }
    }

    public override void reset()
    {
        if (card is LandCard) {
            ((LandCard)card).turned = false;
        }
        else {
            base.reset();
        }
    }
}

public class InDiscard : CardState {

    public InDiscard(Card card) : base(card) { }

}