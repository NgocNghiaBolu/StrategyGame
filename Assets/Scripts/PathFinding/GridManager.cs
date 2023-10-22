using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Tooltip("World Grif Size - UnityEditor")]
    public int unityGridSize = 10;
    public int UnityGridSize { get{ return unityGridSize; } }

    [SerializeField] Vector2Int gridSize;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> Grid { get { return grid; } }

    private void Awake()
    {
        CreateGrid();
    }

    public Node GetNode(Vector2Int coordinate)
    {
        if (grid.ContainsKey(coordinate))//neu grid chua phan tu voi key(coodinate)
        {
            return grid[coordinate];//tra ve coordinates tuong ung
        }
        return null;// khong co node nao tim thay tai coodinate
    }

    public void BlockNode(Vector2Int coodinate)
    {
        if (grid.ContainsKey(coodinate))
        {
            grid[coodinate].isWalkable = false;
        }
    }

    public void ResetNode()
    {
        foreach(KeyValuePair<Vector2Int, Node> entry in grid)
        {
            entry.Value.connectedTo = null;
            entry.Value.isExplored = false;
            entry.Value.isPath = false;
        }
    }

    public Vector2Int GetCoordinateFromPosition(Vector3 pos)
    {
        Vector2Int coordinates = new Vector2Int();
        coordinates.x = Mathf.RoundToInt(pos.x) / unityGridSize;
        coordinates.y = Mathf.RoundToInt(pos.z) / unityGridSize;

        return coordinates;
    }

    public Vector3 GetPositionFromCoordinate(Vector2Int coordinate)
    {
        Vector3 pos = new Vector3();
        pos.x = coordinate.x * unityGridSize;
        pos.z = coordinate.y * unityGridSize;

        return pos;
    }

    public void CreateGrid()
    {
        for(int x = 0; x < gridSize.x; x++)
        {
            for(int y = 0; y < gridSize.y; y++)
            {
                Vector2Int coodina = new Vector2Int(x, y);
                grid.Add(coodina, new Node(coodina, true));
                Debug.Log(grid[coodina].coordinate + " = " + grid[coodina].isWalkable);
            }
        }
    }
}
