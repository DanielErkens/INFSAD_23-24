using System;

public class Program {

    public static void init_board() {
        GameState gameState = GameState.getInstance();

        //Set the gameState for the players
        gameState.Players[0].GameState = gameState;
        gameState.Players[1].GameState = gameState;

        // subscribe
        // gameState.counterHandler += gameState.Players[(gameState.CurrentTurn + gameState.CounterTurn + 1) % 2].playTurn; 
        // unsubscribe
        // gameState.counterHandler -= gameState.Players[(gameState.CurrentTurn + gameState.CounterTurn + 1) % 2].attack; 
    }

    public static void init_decks() {
        GameState gameState = GameState.getInstance();
        Card temp;
        // Arnold

        // Deck
        for (int i=0; i<30; i++) {
            gameState.Player1.Deck.Add(CardFactory.Instance.createCard(gameState.Player1, TypeOfCard.land, CardColor.White, null, null, cost: 1));
        }

        // 2 Blue lands
        temp = CardFactory.Instance.createCard(gameState.Player1, TypeOfCard.land, CardColor.Blue, null, null);
        temp.CardState = new InHand(temp);
        gameState.Player1.Hand.Add(temp);

        temp = CardFactory.Instance.createCard(gameState.Player1, TypeOfCard.land, CardColor.Blue, null, null);
        temp.CardState = new InHand(temp);
        gameState.Player1.Hand.Add(temp);

        // 1 green land
        temp = CardFactory.Instance.createCard(gameState.Player1, TypeOfCard.land, CardColor.Green, null, null);
        temp.CardState = new InHand(temp);
        gameState.Player1.Hand.Add(temp);

        // 1 blue creature
        temp = CardFactory.Instance.createCard(gameState.Player1, TypeOfCard.creature, CardColor.Blue, null, null, cost: 2, attack: 2, defence: 2);
        temp.CardState = new InHand(temp);
        temp.ActivationEffect = new CreatureEffect(gameState.CurrentTurn, temp, false, 0, Target.Other);
        gameState.Player1.Hand.Add(temp);

        // 1 green spell
        temp = CardFactory.Instance.createCard(gameState.Player1, TypeOfCard.spell, CardColor.Green, null, null, cost: 1);
        temp.CardState = new InHand(temp);
        temp.ActivationEffect = new buffCreature(gameState.CurrentTurn, temp, false, 1, Target.Self);
        gameState.Player1.Hand.Add(temp);

        // 1 blue instant counter
        temp = CardFactory.Instance.createCard(gameState.Player1, TypeOfCard.spell, CardColor.Blue, null, null, cost: 1);
        temp.CardState = new InHand(temp);
        temp.ActivationEffect = new counterSpell(gameState.CurrentTurn, temp, false, 1, Target.both);
        gameState.Player1.Hand.Add(temp);

        // Filler cards
        temp = CardFactory.Instance.createCard(gameState.Player1, TypeOfCard.land, CardColor.White, null, null, cost: 1);
        temp.CardState = new InHand(temp);
        gameState.Player1.Hand.Add(temp);



        // Bryce

        // Deck
        for (int i=0; i<30; i++) {
            gameState.Player2.Deck.Add(CardFactory.Instance.createCard(gameState.Player2, TypeOfCard.land, CardColor.White, null, null, cost: 1));
        }

        // 1 red land
        temp = CardFactory.Instance.createCard(gameState.Player2, TypeOfCard.land, CardColor.Red, null, null);
        temp.CardState = new InHand(temp);
        gameState.Player2.Hand.Add(temp);

        // 1 red instant counter
        temp = CardFactory.Instance.createCard(gameState.Player2, TypeOfCard.spell, CardColor.Red, null, null, cost: 1);
        temp.CardState = new InHand(temp);
        temp.ActivationEffect = new counterSpell(gameState.CurrentTurn, temp, false, 1, Target.both);
        gameState.Player2.Hand.Add(temp);

        for (int i=0; i<6; i++) {
            Card temphand = CardFactory.Instance.createCard(gameState.Player2, TypeOfCard.land, CardColor.White, null, null, cost: 1);
            temphand.CardState = new InHand(temphand);
            gameState.Player2.Hand.Add(temphand);
        }
    }

    public static void play_turns() {
        // some notes
        // playcard() gets first entry in list so cards need bo be put in order
        // Attack() and Passturn() need to be used to allow for counter attacks. this would normally go through user interaction

        GameState gameState = GameState.getInstance();

        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine($"Turn 1: {CurrentPlayer().Name} starts");
        // Arnold
        // preparation phase -> drawing phase
        gameState.nextTurnState(); 
        // Get card from deck  
        CurrentPlayer().getCardFromDeck();
        Console.WriteLine($"{CurrentPlayer().Name} draws a card");
        Console.WriteLine($"{CurrentPlayer().Name} has {CurrentPlayer().Hand.Count} cards in hand and {CurrentPlayer().Deck.Count} cards in deck");

        // drawing phase -> main/ attack phase
        gameState.nextTurnState();
        // Play 2 lands (blue) 
        CurrentPlayer().playCard();
        CurrentPlayer().playCard();
        Console.WriteLine($"{CurrentPlayer().Name} plays 2 blue lands");
        Console.WriteLine($"{CurrentPlayer().Name} has {CurrentPlayer().Hand.Count} cards in hand");
        // main/ attack phase -> ending phase
        gameState.nextTurnState();
        Console.WriteLine($"{CurrentPlayer().Name} doesn't attack this turn");
        Console.WriteLine($"{CurrentPlayer().Name} ends turn");
        Console.WriteLine($"End situation: {CurrentPlayer().Name} has {CurrentPlayer().Hand.Count} cards in hand and {CurrentPlayer().Deck.Count} cards in deck");
        Console.WriteLine($"{CurrentPlayer().Name} has {CurrentPlayer().Lives} lifes left and has {CurrentPlayer().Permanents.Count} permanents in play");
        Console.WriteLine();
        Console.WriteLine();
        // ending phase -> preperation pahse
        gameState.nextTurnState();


        // switch turn to Bryce
        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine($"Turn 1: {CurrentPlayer().Name}");
        // preparation phase -> drawing phase
        gameState.nextTurnState();
        // Get card from deck
        CurrentPlayer().getCardFromDeck();
        Console.WriteLine($"{CurrentPlayer().Name} draws a card");
        Console.WriteLine($"{CurrentPlayer().Name} has {CurrentPlayer().Hand.Count} cards in hand and {CurrentPlayer().Deck.Count} cards in deck");
        // drawing phase -> main/ attack phase
        gameState.nextTurnState();
        // Play 1 land {red}
        CurrentPlayer().playCard();
        Console.WriteLine($"{CurrentPlayer().Name} plays a red land");
        Console.WriteLine($"{CurrentPlayer().Name} has {CurrentPlayer().Hand.Count} cards in hand");
        // main/ attack phase -> ending phase
        gameState.nextTurnState();
        Console.WriteLine($"{CurrentPlayer().Name} doesn't attack this turn");
        Console.WriteLine($"{CurrentPlayer().Name} ends turn");
        Console.WriteLine($"End situation: {CurrentPlayer().Name} has {CurrentPlayer().Hand.Count} cards in hand and {CurrentPlayer().Deck.Count} cards in deck");
        Console.WriteLine($"{CurrentPlayer().Name} has {CurrentPlayer().Lives} lifes left and has {CurrentPlayer().Permanents.Count} permanents in play");
        Console.WriteLine();
        Console.WriteLine();
        // ending phase -> preperation pahse
        gameState.nextTurnState();

        // Turn 2

        // Arnold
        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine($"Turn 2: {CurrentPlayer().Name}");
        // preparation phase -> drawing phase
        gameState.nextTurnState(); 
        // Get card from deck
        CurrentPlayer().getCardFromDeck();
        Console.WriteLine($"{CurrentPlayer().Name} draws a card");
        Console.WriteLine($"{CurrentPlayer().Name} has {CurrentPlayer().Hand.Count} cards in hand and {CurrentPlayer().Deck.Count} cards in deck");
        // drawing phase -> main/ attack phase
        gameState.nextTurnState(); 
        // Play 1 lands (green)
        CurrentPlayer().playCard();
        Console.WriteLine($"{CurrentPlayer().Name} plays a green land");
        Console.WriteLine($"{CurrentPlayer().Name} has {CurrentPlayer().Hand.Count} cards in hand");
        // Turn 2 lands (blue)
        CurrentPlayer().useLand(CardColor.Blue, 2);
        Console.WriteLine($"{CurrentPlayer().Name} turns 2 blue lands");
        Console.WriteLine($"{CurrentPlayer().Name} has 2 blue energy");
        // Play Creature (blue)
        CurrentPlayer().playCard();
        Console.WriteLine($"{CurrentPlayer().Name} pays 2 blue energy and plays a blue creature with 2/2");
        Console.WriteLine($"{CurrentPlayer().Name} has {CurrentPlayer().Energy[CardColor.Blue]} energy");
        // main/ attack phase -> ending phase
        gameState.nextTurnState();
        Console.WriteLine($"{CurrentPlayer().Name} doesn't attack this turn");
        Console.WriteLine($"{CurrentPlayer().Name} ends turn");
        Console.WriteLine($"End situation: {CurrentPlayer().Name} has {CurrentPlayer().Hand.Count} cards in hand and {CurrentPlayer().Deck.Count} cards in deck");
        Console.WriteLine($"{CurrentPlayer().Name} has {CurrentPlayer().Lives} lifes left and has {CurrentPlayer().Permanents.Count} permanents in play");
        Console.WriteLine();
        Console.WriteLine();
        // ending phase -> preperation pahse
        gameState.nextTurnState();
        

        // Bryce
        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine($"Turn 2: {CurrentPlayer().Name}");
        // preparation phase -> drawing phase
        gameState.nextTurnState();
        // Get card from deck
        CurrentPlayer().getCardFromDeck();
        Console.WriteLine($"{CurrentPlayer().Name} draws a card");
        Console.WriteLine($"{CurrentPlayer().Name} has {CurrentPlayer().Hand.Count} cards in hand and {CurrentPlayer().Deck.Count} cards in deck");
        // drawing phase -> main/ attack phase
        gameState.nextTurnState();
        Console.WriteLine($"{CurrentPlayer().Name} doesn't play any cards");
        // main/ attack phase -> ending phase
        gameState.nextTurnState();
        Console.WriteLine($"{CurrentPlayer().Name} doesn't attack this turn");
        Console.WriteLine($"{CurrentPlayer().Name} ends turn");
        Console.WriteLine($"End situation: {CurrentPlayer().Name} has {CurrentPlayer().Hand.Count} cards in hand and {CurrentPlayer().Deck.Count} cards in deck");
        Console.WriteLine($"{CurrentPlayer().Name} has {CurrentPlayer().Lives} lifes left and has {CurrentPlayer().Permanents.Count} permanents in play");
        Console.WriteLine();
        Console.WriteLine();
        // ending phase -> preperation pahse
        gameState.nextTurnState();
        


        // Turn 3
        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine($"Turn 3: {CurrentPlayer().Name}");
        // preparation phase -> drawing phase
        gameState.nextTurnState(); 
        // Get card from deck
        CurrentPlayer().getCardFromDeck();
        Console.WriteLine($"{CurrentPlayer().Name} draws a card");
        Console.WriteLine($"{CurrentPlayer().Name} has {CurrentPlayer().Hand.Count} cards in hand and {CurrentPlayer().Deck.Count} cards in deck");
        // drawing phase -> main/ attack phase
        gameState.nextTurnState(); 
        // Play attack creature (blue)
        CurrentPlayer().useCreature();
        Console.WriteLine($"{CurrentPlayer().Name} attacks with the 2/2 creature");
        // Cast spell (green) adds +3/+3
        CurrentPlayer().useLand(CardColor.Green, 1);
        Console.WriteLine($"{CurrentPlayer().Name} turns 1 green land");
        Console.WriteLine($"{CurrentPlayer().Name} has 1 green energy");
        CurrentPlayer().playCard();
        Console.WriteLine($"{CurrentPlayer().Name} uses 1 green energy and plays a green spell that gives +3/+3 to the 2/2 blue creature");
        Console.WriteLine($"{CurrentPlayer().Name} has {CurrentPlayer().Energy[CardColor.Green]} energy");
        // main/ attack phase -> counter/ attack phase
        CurrentPlayer().attack();
        // counter by player 2
        CurrentPlayer().useLand(CardColor.Red, 1);
        Console.WriteLine($"{CurrentPlayer().Name} turns 1 red land");
        Console.WriteLine($"{CurrentPlayer().Name} has 1 red energy");
        CurrentPlayer().playCard();
        Console.WriteLine($"{CurrentPlayer().Name} uses 1 red energy and plays a red instant counter spell");
        Console.WriteLine($"{CurrentPlayer().Name} has {CurrentPlayer().Energy[CardColor.Red]} energy");
        // main/ attack phase -> counter/ attack phase
        CurrentPlayer().attack();
        // counter by player 1
        CurrentPlayer().useLand(CardColor.Blue, 1);
        Console.WriteLine($"{CurrentPlayer().Name} turns 1 blue land");
        Console.WriteLine($"{CurrentPlayer().Name} has 1 blue energy");
        CurrentPlayer().playCard();
        Console.WriteLine($"{CurrentPlayer().Name} uses 1 blue energy and plays a blue instant counter spell");
        Console.WriteLine($"{CurrentPlayer().Name} has {CurrentPlayer().Energy[CardColor.Blue]} energy");
        // player 2 no longer counters and lets it play out
        CurrentPlayer().passTurn();
        // main/ attack phase -> ending phase
        gameState.nextTurnState();
        Console.WriteLine($"{CurrentPlayer().Name} ends turn");

        // ending phase -> preperation pahse
        gameState.nextTurnState();
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine($"End situation {GameState.getInstance().Players[0].Name}:");
        Console.WriteLine($"{GameState.getInstance().Players[0].Name} deals 5 damage to Bryce with the 5/5 creature");
        Console.WriteLine($"{GameState.getInstance().Players[0].Name} has {GameState.getInstance().Players[0].Hand.Count} cards in hand and {GameState.getInstance().Players[0].Deck.Count} cards in deck");
        Console.WriteLine($"{GameState.getInstance().Players[0].Name} has {GameState.getInstance().Players[0].Lives} lifes left and has {GameState.getInstance().Players[0].Permanents.Count} permanents in play");
        Console.WriteLine();
        Console.WriteLine();

        Console.WriteLine($"End situation {GameState.getInstance().Players[1].Name}:");
        Console.WriteLine($"{GameState.getInstance().Players[1].Name} has {GameState.getInstance().Players[1].Hand.Count} cards in hand and {GameState.getInstance().Players[1].Deck.Count} cards in deck");
        Console.WriteLine($"{GameState.getInstance().Players[1].Name} has {GameState.getInstance().Players[1].Lives} lifes left and has {GameState.getInstance().Players[1].Permanents.Count} permanents in play");
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("-------------------------------------------------");
    }

    public static Player CurrentPlayer() {
        GameState gameState = GameState.getInstance();

        Player p = gameState.Players[(gameState.CurrentTurn + gameState.CounterTurn) % 2]; 
        return p;
    }

    public static void Main(String[] args) {
        init_board();
        init_decks();
        play_turns();

    }

}