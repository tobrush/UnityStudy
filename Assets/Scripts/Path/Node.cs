using System;
using UnityEngine;

public class Node : IComparable<Node>
{
    public Node parent;
    public Vector3 pos;

    public float nodeTotalCost; // G
    public float estimateCost; // H

    public bool isObstacle;

    public Node()
    {
        parent = null;
        nodeTotalCost = 0;
        estimateCost = 0;
        isObstacle = false;
    }

    public Node(Vector3 pos)
    {
        this.pos = pos;
        parent = null;
        nodeTotalCost = 0;
        estimateCost = 0;
        isObstacle = false;
    }

    public void MarkAsObstacle()
    {
        isObstacle = true;
    }

    // F = G + H
    public float GetFCost()
    {
        return nodeTotalCost + estimateCost;
    }

    public int CompareTo(Node node)
    {
        float myF = GetFCost();
        float otherF = node.GetFCost();

        if (myF < otherF) return -1;
        if (myF > otherF) return 1;

        if (estimateCost < node.estimateCost)
            return -1;
        if (estimateCost > node.estimateCost)
            return 1;

        return 0;
    }
}