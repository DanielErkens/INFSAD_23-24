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

    public void playLand(LandCard LandToPlay) {
        // Play the land card
        // Implementation depends on your game's land management system
        if(!isGameStateSet()) {
            GameState = GameState.getInstance();
        }
    }

    public void turnLand(LandCard LandToTurn) {
        // Turn the land card
        // Implementation depends on your game's land management system
        if(!isGameStateSet()) {
            GameState = GameState.getInstance();
        }
    }

    public void playCard(Card CardToPlay) {
        // Play the spell card
        // Implementation depends on your game's card management system
        if(!isGameStateSet()) {
            GameState = GameState.getInstance();
        }
    }

    public void passTurn() {
        // Pass the turn
        // Implementation depends on your game's turn management system
        if(!isGameStateSet()) {
            GameState = GameState.getInstance();
        }
    }

    public void takeDamage(int damage) {
        // Take damage
        // Implementation depends on your game's damage management system
        if(!isGameStateSet()) {
            GameState = GameState.getInstance();
        }
        Lives -= damage;
    }

    public void discardCard(Card CardToDiscard) {
        // Discard the card
        // Implementation depends on your game's card management system
        if(!isGameStateSet()) {
            GameState = GameState.getInstance();
        }
    }
}