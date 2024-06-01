using System.ComponentModel;

public class Player
{
    public string Name { get; set; }
    public int Lives { get; set; }
    public List<Card> Deck { get; set; }
    public List<Card> Hand { get; set; }
    public List<Card> DiscardPile { get; set; }
    public List<Card> Permanents { get; set; }
    public Dictionary<CardColor, int> Energy = new Dictionary<CardColor, int>();
    public GameState? GameState { get; set; }

    public Player(string name)
    {
        Name = name;
        Lives = 10;
        Deck = new List<Card>();
        Hand = new List<Card>();
        DiscardPile = new List<Card>();
        Permanents = new List<Card>();
        Energy.Add(CardColor.Blue, 0);
        Energy.Add(CardColor.Brown, 0);
        Energy.Add(CardColor.Green, 0);
        Energy.Add(CardColor.Red, 0);
        Energy.Add(CardColor.White, 0);
        GameState = null;
    }

    public bool isGameStateSet()
    {
        return GameState != null;
    }

    public void getCardFromDeck() {
        Card receivedCard = Deck.First();
        Hand.Add(receivedCard);
        receivedCard.activate();
        Deck.RemoveAt(0);
    }

    public void resetLands() {
        foreach(Card card in Permanents) {
            card.reset();
        }
    }

    public void playCard() {
        Card CardToPlay = Hand.First();

        if (CardToPlay.CardType == CardType.Permanent) {
            Permanents.Add(CardToPlay);
        }
        else {
            DiscardPile.Add(CardToPlay);
        }

        CardToPlay.activate();
        Hand.RemoveAt(0);
    }

    public void useLand(CardColor color, int total) {
        int current = 0;
        for(int i = Permanents.Count - 1; i >= 0; i--) {
            Card card = Permanents[i];

            if (card is LandCard) {
                LandCard temp = card as LandCard;
                if(temp.CardColor == color && current < total) {
                    current +=1;
                    bool activated = temp.activate();
                }
            }

        }
    }

    public void useCreature() {
        for(int i = Permanents.Count - 1; i >= 0; i--) {
            Card card = Permanents[i];

            if (card is CreatureCard) {
                CreatureCard temp = card as CreatureCard;

                temp.attack();
                break;
            }

        }
    }

    public bool payEnergy(CardColor color, int cost) {
        if ( color == CardColor.Colourless && Energy.Values.Sum() > cost) {
            // assumes only amount of lands needed are turned
            foreach (var Key in Energy.Keys) {
                Energy[Key] = 0;
            }
            return true;
        }
        else if( Energy[color] >= cost ) {
            Energy[color] -= cost;
            return true;
        }
        return false;
    }

    // user input required for playturn
    public void playTurn() {
        // get user input

        // play turn
        // I.E. passTurn() or attack()

        // turns are hard coded in program.cs
    }

    public void passTurn() {
        // remove self from counter 
        GameState.getInstance().counterHandler -= GameState.getInstance().Players[(GameState.getInstance().CurrentTurn + GameState.getInstance().CounterTurn) % 2].playTurn; 
        return;
    }

    public void attack() {
        // remove self from counter 
        GameState.getInstance().counterHandler -= GameState.getInstance().Players[(GameState.getInstance().CurrentTurn + GameState.getInstance().CounterTurn) % 2].playTurn;

        GameState.getInstance().CounterTurn +=1;

        // add opponent to counter 
        GameState.getInstance().counterHandler += GameState.getInstance().Players[(GameState.getInstance().CurrentTurn + GameState.getInstance().CounterTurn + 1) % 2].playTurn; 
        return;
    }


    public void takeDamage(int damage) {
        for(int i = Permanents.Count - 1; i >= 0; i--) {
            Card card = Permanents[i];

            if (card is CreatureCard) {
                CreatureCard temp = card as CreatureCard;
                bool dead = temp.takeDamage(damage);

                if (dead) {
                    Permanents.RemoveAt(i);
                }
                break;
            }

        }

        Lives -= damage;
    }

    public void discardCard() {
        Card cardToDiscard = Hand.Last();
        cardToDiscard.discard();
        Hand.RemoveAt(Hand.Count -1);
    }

    public void trimCards() {
        while (Hand.Count > 7) {
            discardCard();
        }
    }
}