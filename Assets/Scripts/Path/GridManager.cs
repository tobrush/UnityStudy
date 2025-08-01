using System.Collections.Generic;
using UnityEngine;

public class GridManager : Singleton<GridManager>
{
    public GameObject[] obstacles;
    public Node[,] nodes { get; set; }

    public int numOfRows;
    public int numOfColumns;
    public float gridCellSize;

    private Vector3 origin = new Vector3();
    public Vector3 Origin
    {
        get { return origin; }
    }

    protected override void Awake()
    {
        base.Awake();

        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        CalculateObstacles();
    }

    private void CalculateObstacles()
    {
        nodes = new Node[numOfRows, numOfColumns];

        int index = 0;
        for (int i = 0; i < numOfColumns; i++)
        {
            for (int j = 0; j < numOfRows; j++)
            {
                Vector3 cellPos = GetGridCellCenter(index);
                Node node = new Node(cellPos);
                nodes[i, j] = node;
                index++;
            }
        }

        if (obstacles != null && obstacles.Length > 0)
        {
            foreach (GameObject obstacle in obstacles)
            {
                int indexCell = GetGridIndex(obstacle.transform.position);
                int row = GetRow(indexCell);
                int col = GetColumn(indexCell);
                nodes[row, col].MarkAsObstacle();
            }
        }
    }

    public Vector3 GetGridCellCenter(int index)
    {
        Vector3 cellPosition = GetGridCellPosition(index);
        cellPosition.x += gridCellSize / 2f;
        cellPosition.z += gridCellSize / 2f;

        return cellPosition;
    }

    public Vector3 GetGridCellPosition(int index)
    {
        int row = GetRow(index);
        int col = GetColumn(index);
        float xPosInGrid = col * gridCellSize;
        float zPosInGrid = row * gridCellSize;

        return Origin + new Vector3(xPosInGrid, 0, zPosInGrid);
    }

    public int GetGridIndex(Vector3 pos)
    {
        if (!isInBounds(pos))
            return -1;

        pos += Origin;
        int col = (int)(pos.x / gridCellSize);
        int row = (int)(pos.z / gridCellSize);

        return row * numOfColumns + col;
    }

    public bool isInBounds(Vector3 pos)
    {
        float width = numOfColumns * gridCellSize;
        float height = numOfRows * gridCellSize;

        return pos.x >= Origin.x && pos.x <= Origin.x + width && pos.z >= Origin.z && pos.z <= Origin.z + height;
    }

    public int GetRow(int index)
    {
        int row = index / numOfColumns;
        return row;
    }

    public int GetColumn(int index)
    {
        int col = index % numOfColumns;
        return col;
    }

    public void GetNeighbors(Node node, List<Node> neighbors)
    {
        int nodeIndex = GetGridIndex(node.pos);
        int row = GetRow(nodeIndex);
        int col = GetColumn(nodeIndex);


        // 아래
        int leftNodeRow = row - 1;
        int leftNodeColumn = col;
        AssignNeighbor(leftNodeRow, leftNodeColumn, neighbors);

        // 위
        leftNodeRow = row + 1;
        leftNodeColumn = col;
        AssignNeighbor(leftNodeRow, leftNodeColumn, neighbors);

        // 오른쪽
        leftNodeRow = row;
        leftNodeColumn = col + 1;
        AssignNeighbor(leftNodeRow, leftNodeColumn, neighbors);

        // 왼쪽
        leftNodeRow = row;
        leftNodeColumn = col - 1;
        AssignNeighbor(leftNodeRow, leftNodeColumn, neighbors);
    }

    private void AssignNeighbor(int row, int col, List<Node> neighbors)
    {
        if (row != -1 && col != -1 && row < numOfRows && col < numOfColumns)
        {
            Node nodeToAdd = nodes[row, col];
            if (!nodeToAdd.isObstacle)
                neighbors.Add(nodeToAdd);
        }
    }

    void OnDrawGizmos()
    {
        DebugDrawGrid(transform.position, numOfRows, numOfColumns, gridCellSize, Color.blue);



        Vector3 cellSize = new Vector3(gridCellSize, 1f, gridCellSize);
        if (obstacles != null && obstacles.Length > 0)
        {
            foreach (GameObject data in obstacles)
                Gizmos.DrawCube(GetGridCellCenter(GetGridIndex(data.transform.position)), cellSize);
        }
    }

    public void DebugDrawGrid(Vector3 origin, int numRows, int numCols, float cellSize, Color color)
    {
        float width = numCols * cellSize;
        float height = numRows * cellSize;

        for (int i = 0; i < numRows + 1; i++)
        {
            Vector3 startPos = origin + i * cellSize * new Vector3(0, 0, 1);
            Vector3 endPos = startPos + width * new Vector3(1, 0, 0);
            Debug.DrawLine(startPos, endPos, color);
        }

        for (int i = 0; i < numCols + 1; i++)
        {
            Vector3 startPos = origin + i * cellSize * new Vector3(1, 0, 0);
            Vector3 endPos = startPos + height * new Vector3(0, 0, 1);
            Debug.DrawLine(startPos, endPos, color);
        }
    }

    public void ResetNodes()
    {
        for (int i = 0; i < numOfColumns; i++)
        {
            for (int j = 0; j < numOfRows; j++)
            {
                Node node = nodes[i, j];
                node.parent = null;
                node.nodeTotalCost = 0f;
                node.estimateCost = 0f;
            }
        }
    }
}