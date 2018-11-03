using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridMap : MonoBehaviour {

    public Vector2 GridSize;
    public LayerMask Obstacles;

    public float NodeDiameter = 1;

    public Node[,] grid;
    public List<Node> path;

    Vector3 bottomLeft;

    int nodeGridSizeX, nodeGridSizeY;

    public float NodeRadius { get { return NodeDiameter / 2; } }

    public void Start()
    {
        nodeGridSizeX = Mathf.RoundToInt(GridSize.x / NodeDiameter);
        nodeGridSizeY = Mathf.RoundToInt(GridSize.y / NodeDiameter);

        CreateGrid();
    }

    private void CreateGrid()
    {
        grid = new Node[nodeGridSizeX, nodeGridSizeY];

        float bottomLeftX = transform.position.x - GridSize.x / 2;
        float bottomLeftY = transform.position.z - GridSize.y / 2;

        bottomLeft = new Vector3(bottomLeftX, 0, bottomLeftY);

        for (int x = 0; x < nodeGridSizeX; x++)
        {
            for (int y = 0; y < nodeGridSizeY; y++)
            {
                Vector3 worldNodePosition = bottomLeft + Vector3.right * (x * NodeDiameter + NodeRadius)
                    + Vector3.forward * (y * NodeDiameter + NodeRadius);

                bool isObstacle = Physics.CheckSphere(worldNodePosition, NodeDiameter / 2, Obstacles);

                grid[x, y] = new Node(worldNodePosition, x, y, isObstacle);
            }
        }
    }


    public float GizmoOffset;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(GridSize.x, 1, GridSize.y));

        if (grid != null)
        {
            foreach (Node n in grid)
            {
                Color color = n.IsObstacle ? Color.red : Color.green;

                if (path != null && path.Contains(n))
                    color = Color.blue;

                Gizmos.color = color;

                Gizmos.DrawCube(n.WorldPosition, Vector3.one * (NodeDiameter - GizmoOffset));
            }
        }

    }

}
