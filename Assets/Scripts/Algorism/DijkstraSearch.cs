using UnityEngine;

//다익스트라 알고리즘 = 최단거리를 효율적으로 찾을 수 있는 알고리즘

public class DijkstraSearch : MonoBehaviour
{
    private int[,] nodes = new int[6, 6]
    {
       // 0  1  2  3  4  5
        { 0, 1, 2, 0, 4, 0 }, // 0
        { 1, 0, 0, 0, 0, 8 }, // 1
        { 2, 0, 0, 3, 0, 0 }, // 2
        { 0, 0, 3, 0, 0, 0 }, // 3
        { 4, 0, 0, 0, 0, 2 }, // 4
        { 0, 8, 0, 0, 2, 0 }, // 5
    };

    void Start()
    {
        int start = 0;
        int[] dist;
        int[] prev; // 이전 노드 번호

        Dijkstra(start, out dist, out prev);

        // 결과 출력
        for (int i = 0; i < nodes.GetLength(0); i++)
        {
            Debug.Log($"{start}에서 {i}까지 최단 거리: {dist[i]}, 경로: {GetPath(i, prev)}");
        }
    }

    void Dijkstra(int start, out int[] dist, out int[] prev)
    {
        int n = nodes.GetLength(0);
        dist = new int[n];
        prev = new int[n];
        bool[] visited = new bool[n];

        // 초기화
        for (int i = 0; i < n; i++)
        {
            dist[i] = int.MaxValue; // 2,147,483,647
            prev[i] = -1;
            visited[i] = false;
        }

        dist[start] = 0; // 자기 자신의 최단 거리는 0
        for (int cnt = 0; cnt < n; cnt++)
        {
            int u = -1; // 최단거리 노드 인덱스
            int min = int.MaxValue; // 최단거리 노드 가중치

            // 방문하지 않은 노드 중 최소 거리 선택
            for (int j = 0; j < n; j++)
            {
                if (!visited[j] && dist[j] < min)
                {
                    min = dist[j];
                    u = j;
                }
            }

            if (u == -1)
                break; // 남은 노드 없음

            visited[u] = true;

            // 인접 노드 업데이트
            for (int k = 0; k < n; k++)
            {
                if (nodes[u, k] > 0 && !visited[k])
                {
                    int newDist = dist[u] + nodes[u, k];
                    if (newDist < dist[k])
                    {
                        dist[k] = newDist;
                        prev[k] = u;
                    }
                }
            }
        }
    }

    // 경로 문자열 복원 함수
    string GetPath(int end, int[] prev)
    {
        if (prev[end] == -1)
            return end.ToString();

        return
            GetPath(prev[end], prev) + " → " + end;
    }
}