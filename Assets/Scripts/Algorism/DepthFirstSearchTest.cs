using System.Collections.Generic;
using UnityEngine;

//깊이 우선 탐색 (DFS : Depth First Search) = 한 갈래를 끝까지 탐색하는 알고리즘

public class DepthFirstSearchTest : MonoBehaviour
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
        DFS_Stack(0);
    }

    void DFS_Stack(int start)
    {
        Stack<int> stack = new Stack<int>();
        stack.Push(start);

        while (stack.Count > 0)
        {
            int index = stack.Pop();

            if (!visited[index])
            {
                visited[index] = true;
                Debug.Log(index + "번 노드 방문");

                // 역순은 그냥 순서를 정하는 것
                for (int i = nodes.GetLength(0) - 1; i >= 0; i--)
                {
                    if (nodes[index, i] == 1 && !visited[i])
                        stack.Push(i);
                }
            }
        }
    }
}