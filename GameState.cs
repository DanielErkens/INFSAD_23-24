using System;
using System.Collections.Generic;

public class GameState {
    private static GameState? instance;
    public ITurnState TurnState { get; set; }
    public Player Player1 { get; set; }
    public Player Player2 { get; set; }

    public Player[] Players { get; set; }
    public int CurrentTurn  { get; set; }

    // board effects are not part of the assignment 
    // public Stack<CardEffect> Effects { get; set; }
    public Stack<CardEffect> Counters { get; set; }

    private GameState(ITurnState turnState, Player player1, Player player2, int currentTurn, Stack<CardEffect> effects, Stack<CardEffect> counters) {
        TurnState = turnState;
        Player1 = player1;
        Player2 = player2;

        Players = new Player[] {player1, player2};
        CurrentTurn = currentTurn;
        // Effects = effects;
        Counters = counters;
    }

    public static GameState getInstance()
    {
        // If instance is null, create a new instance
        if (instance == null)
        {
            ITurnState initialState = new PreparationState();
            Player[] players = CreatePlayers();
            Player Player1 = players[0];
            Player Player2 = players[1];

            instance = new GameState(initialState, Player1, Player2, 0, new Stack<CardEffect>(), new Stack<CardEffect>());
        }
        // Return the single instance
        return instance;
    }

    private static Player[] CreatePlayers() {
        Player Player1 = new Player("Player 1");
        Player Player2 = new Player("Player 2");
        Player[] players = {Player1, Player2};
        return players;
    }

    // public Stack<CardEffect> getEffects() {
    //     return Effects;
    // }

    // public void addEffects(CardEffect cardEffect) {
    //     Effects.Push(cardEffect);
    // }

    // public void deleteEffect(CardEffect cardEffect) {
    //     List<CardEffect> temp = new List<CardEffect>(Effects);
    //     temp.Remove(cardEffect);
    //     Effects = new Stack<CardEffect>(temp);
    // }

    public void nextTurnState() {
        this.TurnState.PlayPhase();

    }

}