using System.Runtime.InteropServices;

public abstract class CardState
{
    protected Card card;

    public CardState(Card card)
    {
        this.card = card;
    }

    // Enters when no function is found in the currecnt card state
    public virtual bool useCard() { Console.WriteLine("Unable to activate"); return false; }

    public virtual bool reset() { Console.WriteLine("Unable to reset land"); return false;}

    public virtual bool discard() { Console.WriteLine("Unable to discard"); return false;}

}

public class InDeck : CardState {

    public InDeck(Card card) : base(card) { }

    public override bool useCard()
    {
        card.CardState = new InHand(this.card);
        return true;
    }

    public override bool discard()
    {
        card.CardState = new InDiscard(card);
        return true;
    }

}

public class InHand : CardState {

    public InHand(Card card) : base(card) { }

    public override bool useCard()
    {
        // if(card.ActivationEffect != null) {
        //     card.ActivationEffect.checkActivationCondition();
        // }

        switch(card)
        {
            case LandCard:
                LandCard land = card as LandCard;
                card.CardState = new InPlay(this.card);
                return true;

            case SpellCard:
                SpellCard spell = card as SpellCard;
                if(card.Owner.payEnergy(spell.CardColor, spell.Cost)) {
                    // effect array is never checked for spells
                    if(card.ActivationEffect != null) {
                        card.ActivationEffect.checkActivationCondition();
                    }

                    if (spell.CardType == CardType.Instantaneous) {
                        spell.CardState = new InDiscard(card);
                    }
                    else {
                        card.CardState = new InPlay(this.card);
                    }
                    return true;
                }
                else {
                    Console.WriteLine("Unable to play. you're broke");
                }
                break;

            case ArtefactCard:
                ArtefactCard artefact = card as ArtefactCard;
                // payment needs to accept any color
                if(card.Owner.payEnergy(artefact.CardColor, artefact.Cost)) {
                    
                    // activation effect is never checked for artefacts
                    foreach(CardEffect eff in card.Effects){
                        eff.checkActivationCondition();
                    }

                    card.CardState = new InPlay(this.card);

                    return true;
                }
                else {
                    Console.WriteLine("Unable to play. you're broke");
                }
                break;

            case CreatureCard:
                CreatureCard creature = card as CreatureCard;
                if(card.Owner.payEnergy(creature.CardColor, creature.Cost)) {
                    card.CardState = new InPlay(this.card);
                    if(card.ActivationEffect != null) {
                        card.ActivationEffect.checkActivationCondition();
                    }
                    return true;
                }
                else {
                    Console.WriteLine("Unable to play. you're broke");
                }
                break;

            default:
                throw new ArgumentException("Unknown card type: ");
        }
        return false;

    }

    public override bool discard()
    {
        card.CardState = new InDiscard(card);
        return true;
    }
}

public class InPlay : CardState {

    public InPlay(Card card) : base(card) { }

    public override bool useCard()
    {
        switch(card)
        {
            case LandCard:
                LandCard land = card as LandCard;

                if (!land.turned) {
                    land.Owner.Energy[land.CardColor] +=1;
                }

                land.turned = true;
                return true;

            case SpellCard:

                SpellCard spell = card as SpellCard;

                foreach(CardEffect f in spell.Effects) {
                    f.checkActivationCondition();
                }

                if (spell.CardType == CardType.Instantaneous) {
                    spell.CardState = new InDiscard(card);
                }

                return true;

            case ArtefactCard:
                ArtefactCard artefact = card as ArtefactCard;
                return true;

            case CreatureCard:
            
                CreatureCard creature = card as CreatureCard;

                GameState.getInstance().Counters.Push( new AttackEffect( GameState.getInstance().CurrentTurn, this.card, false, 0 ) );

                return true;

            default:
                return false;
                throw new ArgumentException("Unknown card type: ");
        }
    }

    public override bool reset()
    {
        if (card is LandCard) {
            ((LandCard)card).turned = false;
            return true;
        }
        else {
            return false;
        }
    }
}

public class InDiscard : CardState {

    public InDiscard(Card card) : base(card) { }

}