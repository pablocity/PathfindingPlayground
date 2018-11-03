using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{

    public Vector3 WorldPosition;
    public Node Parent;

    public int GridX, GridY;
    public int GCost, HCost;

    public bool IsObstacle = false;

    public Node(Vector3 position, int gridX, int gridY, bool isObstacle = false)
    {
        WorldPosition = position;
        GridX = gridX;
        GridY = gridY;
        IsObstacle = isObstacle;
    }
}
