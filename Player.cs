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

    public bool payEnergy(CardColor color, int cost) {
        if( Energy[color] >= cost ) {
            Energy[color] -= cost;
            return true;
        }
        return false;
    }

    public void passTurn() {
        // Pass the turn
        // Implementation depends on your game's turn management system
        if(!isGameStateSet()) {
            GameState = GameState.getInstance();
        }
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
        // Discard the card
        // Implementation depends on your game's card management system
        if(!isGameStateSet()) {
            GameState = GameState.getInstance();
            Card cardToDiscard = Hand.Last();
            cardToDiscard.discard();
            Hand.RemoveAt(Hand.Count -1);
        }
    }

    public void trimCards() {
        while (Deck.Count > 7) {
            discardCard();
        }
    }
}