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
            gameState.Player1.Deck.Add(CardFactory.Instance.createCard(gameState.Player1, CardPlaceHolder.land, CardColor.White, null, null, cost: 1));
        }

        // 2 Blue lands
        temp = CardFactory.Instance.createCard(gameState.Player1, CardPlaceHolder.land, CardColor.Blue, null, null);
        temp.CardState = new InHand(temp);
        gameState.Player1.Hand.Add(temp);

        temp = CardFactory.Instance.createCard(gameState.Player1, CardPlaceHolder.land, CardColor.Blue, null, null);
        temp.CardState = new InHand(temp);
        gameState.Player1.Hand.Add(temp);

        // 1 green land
        temp = CardFactory.Instance.createCard(gameState.Player1, CardPlaceHolder.land, CardColor.Green, null, null);
        temp.CardState = new InHand(temp);
        gameState.Player1.Hand.Add(temp);

        // 1 blue creature
        temp = CardFactory.Instance.createCard(gameState.Player1, CardPlaceHolder.creature, CardColor.Blue, null, null, cost: 2, attack: 2, defence: 2);
        temp.CardState = new InHand(temp);
        temp.ActivationEffect = new CreatureEffect(gameState.CurrentTurn, temp, false, 0, Target.Other);
        gameState.Player1.Hand.Add(temp);

        // 1 green spell
        temp = CardFactory.Instance.createCard(gameState.Player1, CardPlaceHolder.spell, CardColor.Green, null, null, cost: 1);
        temp.CardState = new InHand(temp);
        temp.ActivationEffect = new buffCreature(gameState.CurrentTurn, temp, false, 1, Target.Self);
        gameState.Player1.Hand.Add(temp);

        // 1 blue instant counter
        temp = CardFactory.Instance.createCard(gameState.Player1, CardPlaceHolder.spell, CardColor.Blue, null, null, cost: 1);
        temp.CardState = new InHand(temp);
        temp.ActivationEffect = new counterSpell(gameState.CurrentTurn, temp, false, 1, Target.both);
        gameState.Player1.Hand.Add(temp);

        // Filler cards
        temp = CardFactory.Instance.createCard(gameState.Player1, CardPlaceHolder.land, CardColor.White, null, null, cost: 1);
        temp.CardState = new InHand(temp);
        gameState.Player1.Hand.Add(temp);



        // Bryce

        // Deck
        for (int i=0; i<30; i++) {
            gameState.Player2.Deck.Add(CardFactory.Instance.createCard(gameState.Player2, CardPlaceHolder.land, CardColor.White, null, null, cost: 1));
        }

        // 1 red land
        temp = CardFactory.Instance.createCard(gameState.Player2, CardPlaceHolder.land, CardColor.Red, null, null);
        temp.CardState = new InHand(temp);
        gameState.Player2.Hand.Add(temp);

        // 1 red instant counter
        temp = CardFactory.Instance.createCard(gameState.Player2, CardPlaceHolder.spell, CardColor.Red, null, null, cost: 1);
        temp.CardState = new InHand(temp);
        temp.ActivationEffect = new counterSpell(gameState.CurrentTurn, temp, false, 1, Target.both);
        gameState.Player2.Hand.Add(temp);

        for (int i=0; i<6; i++) {
            Card temphand = CardFactory.Instance.createCard(gameState.Player2, CardPlaceHolder.land, CardColor.White, null, null, cost: 1);
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

        // TODO
        // Cards can have up to 3 copies in each deck, no more.
        // Only instantaneous cards can be cast outside the owner turn
        // Cards (beside lands) contains an activation effect or cost, and some permanent effect.
        // Temporary effect example: an instantaneous spell costs 3 energy, is a green spell that gives +3/+3 at the creature till the preparation phase. At the preparation phase of the owner, the creature will cease to have the effect of this spell.
        // Lands can be used only after passing the first [preparation] phase.
        // Defending creatures

        // current turn calculations should also consider counter turn to make sure its the correct player

        // design patterns to implement

        // 1. **Effects Waiting for Certain Conditions:**
        //    - **Best Suited Pattern**: Observer Pattern
        //    - **Explanation**: The Observer pattern is well-suited for implementing effects that wait for certain conditions because it establishes a one-to-many relationship between the subject (the game state) 
        //      and its observers (the effects). Each effect can observe the game state and be notified when the conditions it's waiting for are met. 
        //      This allows for a direct and specific relationship between the effects and the game state.
        //    - **Implementation**: Each effect could register itself as an observer of the game state. 
        //      When the game state changes (e.g., a player takes an action that affects the conditions), it notifies all registered observers. 
        //      Effects waiting for specific conditions can then react accordingly when they receive the notification.

        // 2. **Counter Attack System:**
        //    - **Best Suited Pattern**: Pub-Sub Pattern
        //    - **Explanation**: The Pub-Sub pattern is well-suited for implementing a counter-attack system because it allows for decoupling between the initiator of the attack (the attacker)
        //      and the potential responders (the defenders). The attacker can publish an attack event to which potential defenders subscribe. 
        //      This asynchronous communication allows for flexibility in how the defenders respond.
        //    - **Implementation**: When a player initiates an attack, the attack event is published to a central event bus. 
        //      Players who are eligible to counter-attack can subscribe to this event. 
        //      If a player decides to counter-attack, they can then publish their counter-attack event to which the original attacker and other relevant players can subscribe.

        // In summary, the Observer pattern is best suited for effects waiting for certain conditions because it establishes a direct relationship between the effects and the game state. 
        // On the other hand, the Pub-Sub pattern is best suited for the counter-attack system because it allows for asynchronous communication between the attacker and potential defenders, 
        // promoting loose coupling and flexibility in how players respond to attacks.
    }

}