using System;

public class Program {

    public static void init_board() {
        GameState gameState = GameState.getInstance();

        //Set the gameState for the players
        gameState.Players[0].GameState = gameState;
        gameState.Players[1].GameState = gameState;
    }

    public static void init_decks() {
        // TODO card state needs to reflect 
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
        temp.ActivationEffect = new CreatureEffect(gameState.CurrentTurn, temp, false, 0, Target.Other);
        gameState.Player1.Hand.Add(temp);

        // 1 green spell
        temp = CardFactory.Instance.createCard(gameState.Player1, CardPlaceHolder.spell, CardColor.Green, null, null, cost: 1);
        temp.CardState = new InHand(temp);
        gameState.Player1.Hand.Add(temp);

        // 1 blue instant counter
        temp = CardFactory.Instance.createCard(gameState.Player1, CardPlaceHolder.spell, CardColor.Blue, null, null, cost: 1);
        temp.CardState = new InHand(temp);
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
        gameState.Player2.Hand.Add(temp);

        for (int i=0; i<6; i++) {
            gameState.Player2.Hand.Add(CardFactory.Instance.createCard(gameState.Player2, CardPlaceHolder.land, CardColor.White, null, null, cost: 1));
        }
    }

    public static void play_turns() {
        GameState gameState = GameState.getInstance();
    // Player A turn 1: 2 blue lands

    // Player B turn 1: Plays red land

    // Player A turn 2: Draws card (blue land)
    // Plays land (Green Land)
    // Plays blue card of 2 energy (Blue 2/2 creature)
    // Doesnt attack
    // Add immediate play affect where opponent discards a random card from their hand

    // Player B turn 2: Draws card (Unknown), but could make it the 1 red energy spell

    // Player A turn 3: Draws card
    // Attacks with blue 2/2 creature
    // Gets 1 green energy from land (must be typo since 2 other lands are blue).
    // With the 1 green energy he plays a green spell which adds +3/+3 to the 2/2 blue creature

    // Player B turn 3: 
    // Gets red energy from land
    // Casts instantaneous red spell and stops the green spell from player A (creature still attacking but at +2/+2, then immediately remove affect since its instantaneous)

    // Player A turn 3:
    // Gets 1 blue energy from land
    // Casts instantaneous blue spell to stop the red spell from player B (affect gets deleted from red spell. No red affect deleted since its instantaneous, so only affect is +3/+3)
    // Blue creature is +5/+5, attacks and deals 5 damage to player B (only affect in gameState is +3/+3 since its not mentioned its instantaneous) 



    }

    public static void Main(String[] args) {
        init_board();
        // starting state
        init_decks();
        play_turns();

        //Use turnState states to play the turns
        // gameState.TurnState.PlayPhase();
        // gameState.TurnState.UpdateCardEffectIsActive();
        //Check win condition
    }
}