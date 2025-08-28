using System.Collections.Generic;
using UnityEngine;

public class Single_BoardTicTacToe
{
    public int[,] board;
    private const int ROWS = 3, COLS = 3;

    public int player;

    public Single_BoardTicTacToe()
    {
        player = 1;
        board = new int[ROWS, COLS];
    }

    // �� �� �ִ� ĭ Ȯ���ϴ� ���
    public List<Single_Move> GetMoves()
    {
        var moves = new List<Single_Move>();

        for (int i = 0; i < COLS; i++)
        {
            for (int j = 0; j < ROWS; j++)
            {
                if (board[i, j] == 0) // �� ĭ
                {
                    moves.Add(new Single_Move(j, i, player));
                }
            }
        }

        return moves;
    }

    // ĭ�� �����ϴ� ���
    public void MakeMove(Single_Move move)
    {
        if (board[move.y, move.x] != 0) // ���� ���� �� ����
            return;

        board[move.y, move.x] = move.player; // Player = 1 or 2

        this.player = (move.player) == 1 ? 2 : 1; // ���� �̵��� Player�� 1�̶�� �� ���� �÷��̾�� 2 ����
    }

    // 0: ������, 1: Player1 �¸�, 2: Player2 �¸�, 3: ���º�
    public int CheckWinner()
    {
        // ���� Ȯ��
        for (int i = 0; i < ROWS; i++)
        {
            if (board[i, 0] != 0 && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
            {
                return board[i, 0];
            }
        }

        // ���� Ȯ��
        for (int j = 0; j < COLS; j++)
        {
            if (board[0, j] != 0 && board[0, j] == board[1, j] && board[1, j] == board[2, j])
            {
                return board[0, j];
            }
        }

        if (board[0, 0] != 0 && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
        {
            return board[0, 0];
        }

        if (board[0, 2] != 0 && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
        {
            return board[0, 2];
        }

        // ���º�
        if (GetMoves().Count == 0)
            return 3;

        // ������
        return 0;
    }

    public bool IsGameOver()
    {
        return CheckWinner() != 0;
    }

}