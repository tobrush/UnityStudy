using System.Collections.Generic;
using UnityEngine;

//너비 우선 탐색 (BFS : Breadth First Search) = 같은 깊이의 노드부터 차례로 탐색하는 알고리즘

public class BreadthFirstSearch : MonoBehaviour
{
    private int[,] nodes = new int[8, 8]
    {
        // 0  1  2  3  4  5  6  7
        { 0, 1, 1, 1, 0, 0, 0, 0}, // 0
        { 1, 0, 0, 0, 1, 1, 0, 0}, // 1
        { 1, 0, 0, 0, 0, 0, 0, 0}, // 2
        { 1, 0, 0, 0, 0, 0, 1, 0}, // 3
        { 0, 1, 0, 0, 0, 1, 0, 0}, // 4
        { 0, 1, 0, 0, 1, 0, 0, 1}, // 5
        { 0, 0, 0, 1, 0, 0, 0, 0}, // 6
        { 0, 0, 0, 0, 0, 1, 0, 0}  // 7
    };

    private bool[] visited = new bool[8];

    private void Start()
    {
        BFS(0);
    }

    void BFS(int start)
    {
        Queue<int> queue = new Queue<int>();
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            int index = queue.Dequeue();

            if (!visited[index])
            {
                visited[index] = true;
                Debug.Log(index + "번 노드 방문");

                for (int i = 0; i < nodes.GetLength(0); i++)
                {
                    if (nodes[index, i] == 1 && !visited[i])
                    {
                        queue.Enqueue(i);
                    }
                }
            }
        }
    }
}