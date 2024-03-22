﻿using System;

public class Program {
    public static void init_board() {
        GameState gameState = GameState.getInstance();

        //Set the gameState for the players
        gameState.Players[0].GameState = gameState;
        gameState.Players[1].GameState = gameState;
    }

    public static void init_decks() {
        GameState gameState = GameState.getInstance();
        // Arnold

        // 2 Blue lands
        gameState.Player1.Hand.Add(CardFactory.Instance.createCard(gameState.Player1, CardPlaceHolder.land, CardColor.Blue, CardType.Permanent, null, null));
        gameState.Player1.Hand.Add(CardFactory.Instance.createCard(gameState.Player1, CardPlaceHolder.land, CardColor.Blue, CardType.Permanent, null, null));

        // 1 green land
        gameState.Player1.Hand.Add(CardFactory.Instance.createCard(gameState.Player1, CardPlaceHolder.land, CardColor.Green, CardType.Permanent, null, null));

        // 1 blue creature
        Card temp = CardFactory.Instance.createCard(gameState.Player1, CardPlaceHolder.creature, CardColor.Blue, CardType.Permanent, null, null, 2, 2, 2);
        temp.ActivationEffect = new CreatureEffect(gameState.CurrentTurn, temp, false, 0, Target.Other);
        gameState.Player1.Hand.Add(temp);

        // 1 green spell
        gameState.Player1.Hand.Add(CardFactory.Instance.createCard(gameState.Player1, CardPlaceHolder.spell, CardColor.Green, CardType.Instantaneous, null, null, 1));

        // 1 blue instant counter
        gameState.Player1.Hand.Add(CardFactory.Instance.createCard(gameState.Player1, CardPlaceHolder.spell, CardColor.Blue, CardType.Instantaneous, null, null, 1));



        // Bryce
        // 1 red land
        gameState.Player2.Hand.Add(CardFactory.Instance.createCard(gameState.Player2, CardPlaceHolder.land, CardColor.Red, CardType.Permanent, null, null));

        // 1 red instant counter
        gameState.Player2.Hand.Add(CardFactory.Instance.createCard(gameState.Player2, CardPlaceHolder.spell, CardColor.Red, CardType.Instantaneous, null, null, 1));

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
        init_decks();
        play_turns();

        //Use turnState states to play the turns
        // gameState.TurnState.PlayPhase();
        // gameState.TurnState.UpdateCardEffectIsActive();
        //Check win condition
    }
}