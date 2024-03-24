using System;
using System.Collections.Generic;

public class GameState {
    private static GameState? instance;
    public ITurnState TurnState { get; set; }
    public Player Player1 { get; set; }
    public Player Player2 { get; set; }

    public Player[] Players { get; set; }
    public int CurrentTurn  { get; set; }
    public int CounterTurn { get; set; }
    public delegate void attackHandler();
    public event attackHandler counterHandler;

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
        // Return the singleton instance
        return instance;
    }

    private static Player[] CreatePlayers() {
        Player Player1 = new Player("Arold");
        Player Player2 = new Player("Bryce");
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
        // event handler here???
        this.TurnState.PlayPhase();

    }

    public void counter() {
        // check if last player attacked
        if (counterHandler != null) {
            // Console.WriteLine(counterHandler.GetInvocationList().Length);
            // Notify opponent they can counter
            counterHandler();   
            TurnState = new MainState();
        }


        // unsub the player that is about to counter
        // counterHandler -=Players[(CurrentTurn + CounterTurn) % 2].playTurn; 

        // sub the other user for counter
        // counterHandler +=Players[(CurrentTurn + CounterTurn + 1) % 2].playTurn; 
    }

    public int getCounterLength() {
        return counterHandler != null ? counterHandler.GetInvocationList().Length : 0;
    }

}