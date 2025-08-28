using System.Collections.Generic;
using System;

public class BoardTicTacToe : Board
{
    private readonly int[,] board;
    private const int ROWS = 3;
    private const int COLS = 3;

    public BoardTicTacToe()
    {
        this.player = 1;
        board = new int[ROWS, COLS];
    }

    private BoardTicTacToe(int[,] board, int player)
    {
        this.board = board;
        this.player = player;
    }

    public override Move[] GetMoves()
    {
        var moves = new List<Move>();
        for (int i = 0; i < ROWS; i++)
        {
            for (int j = 0; j < COLS; j++)
            {
                if (board[i, j] == 0)
                {
                    moves.Add(new MoveTicTac(j, i, player));
                }
            }
        }
        return moves.ToArray();
    }

    public override Board MakeMove(Move m)
    {
        MoveTicTac move = m as MoveTicTac;
        int nextPlayer = (move.player == 1) ? 2 : 1;

        int[,] boardCopy = new int[ROWS, COLS];
        Array.Copy(board, boardCopy, board.Length);

        boardCopy[move.y, move.x] = move.player;

        return new BoardTicTacToe(boardCopy, nextPlayer);
    }

    public override bool IsGameOver()
    {
        return CheckWinner() != 0;
    }

    public override int CheckWinner()
    {
        // 가로/세로 확인
        for (int i = 0; i < 3; i++)
        {
            if (board[i, 0] != 0 && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2]) return board[i, 0];
            if (board[0, i] != 0 && board[0, i] == board[1, i] && board[1, i] == board[2, i]) return board[0, i];
        }
        // 대각선 확인
        if (board[0, 0] != 0 && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2]) return board[0, 0];
        if (board[0, 2] != 0 && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0]) return board[0, 2];

        // 무승부 확인
        bool isDraw = true;
        for (int i = 0; i < ROWS; i++)
        {
            for (int j = 0; j < COLS; j++)
            {
                if (board[i, j] == 0)
                {
                    isDraw = false;
                    break;
                }
            }
        }
        if (isDraw) return 3;

        return 0; // 게임 진행 중
    }

    public int GetCell(int row, int col)
    {
        return board[row, col];
    }
}