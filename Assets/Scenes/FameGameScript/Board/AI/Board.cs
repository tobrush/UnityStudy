using UnityEngine;

public abstract class Board
{
    protected int player;

    public Board()
    {
        this.player = 1;
    }

    public virtual Move[] GetMoves()
    {
        return new Move[0];
    }

    public abstract Board MakeMove(Move m);

    public virtual bool IsGameOver()
    {
        return CheckWinner() != 0;
    }

    public virtual int GetCurrentPlayer()
    {
        return player;
    }

    public virtual float Evaluate(int forPlayer)
    {
        int winner = CheckWinner();

        if (winner == 0 || winner == 3)
            return 0;
        if (winner == forPlayer)
            return 1;

        return -1;
    }

    public abstract int CheckWinner();
}