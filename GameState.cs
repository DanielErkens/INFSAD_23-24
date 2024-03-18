using System;
using System.Collections.Generic;

public class GameState {
    private static GameState? instance;
    public ITurnState TurnState { get; set; }
    public Player Player1 { get; set; }
    public Player Player2 { get; set; }

    public Player[] Players { get; set; }
    public int CurrentTurn  { get; set; }
    public Stack<CardEffect> Effects { get; set; }
    public Stack<Card> Counters { get; set; }

    private GameState(ITurnState turnState, Player[] players, int currentTurn, Stack<CardEffect> effects) {
        TurnState = turnState;
        Players = players;
        CurrentTurn = currentTurn;
        Effects = effects;
    }

    public static GameState getInstance()
    {
        // If instance is null, create a new instance
        if (instance == null)
        {
            ITurnState initialState = new PreparationState();
            Player[] players = CreatePlayers();
            instance = new GameState(initialState, players, 0, new Stack<CardEffect>());
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

    public Stack<CardEffect> getEffects() {
        return Effects;
    }

    public void addEffects(CardEffect cardEffect) {
        Effects.Push(cardEffect);
    }

    public void deleteEffect(CardEffect cardEffect) {
        List<CardEffect> temp = new List<CardEffect>(Effects);
        temp.Remove(cardEffect);
        Effects = new Stack<CardEffect>(temp);
    }

    public void playGame() {
        this.TurnState.PlayPhase();

    }

}