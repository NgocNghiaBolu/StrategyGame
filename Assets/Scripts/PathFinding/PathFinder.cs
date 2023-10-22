using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoordinate;
    public Vector2Int StartCoordinate { get { return startCoordinate; } }

    [SerializeField] Vector2Int destinateCoordinate;
    public Vector2Int DestinateCoordinate { get { return destinateCoordinate; } }

    Node startNode;
    Node destinationNode;
    Node currentSearchNode;

    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();
    Queue<Node> frontier = new Queue<Node>();

    Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
    GridManager gridManager;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        if(gridManager != null)
        {
            grid = gridManager.Grid;
            startNode = gridManager.Grid[startCoordinate];
            destinationNode = gridManager.Grid[destinateCoordinate];
        }
    }

    void Start()
    {

        GetNewPath();
    }

    public List<Node> GetNewPath()
    {
        return GetNewPath(startCoordinate);
    }

    public List<Node> GetNewPath(Vector2Int coordinate)
    {
        gridManager.ResetNode();
        BreadthFirstSearch(coordinate);
        return BuildPath();
    }

    void ExploreNeighbor()
    {
        List<Node> neighbors = new List<Node>();//create list all neighbor 

        foreach(Vector2Int direction in directions)
        {
            Vector2Int neighborCoordi = currentSearchNode.coordinate + direction;//xac dinh vi tri toa do cua cac Node lan can
             
            if (gridManager.Grid.ContainsKey(neighborCoordi))//kra coi cac o lan can co Grid chua, neu chua thi them vao dsach neighBor
            {
                neighbors.Add(gridManager.GetNode(neighborCoordi));
            }
        }

        foreach(Node neighBor in neighbors)
        {
            if(!reached.ContainsKey(neighBor.coordinate) && neighBor.isWalkable)
            {
                neighBor.connectedTo = currentSearchNode;
                reached.Add(neighBor.coordinate, neighBor);//dsach cac nut da duoc duyet
                frontier.Enqueue(neighBor);// tao thanh Bien Gioi va them vao Queue
            }
        }
    }

    void BreadthFirstSearch(Vector2Int coordinate)
    {
        startNode.isWalkable = true;
        destinationNode.isWalkable = true;

        frontier.Clear();
        reached.Clear();

        bool isRunning = true;

        frontier.Enqueue(grid[coordinate]);//them startNode vao Frontier
        reached.Add(coordinate, grid[coordinate]);//startNode sau khi dc duyet thi cho vao reached

        while(frontier.Count > 0 && isRunning)//da co Bien Gioi xac dinh duoc va Chay
        {
            currentSearchNode = frontier.Dequeue();//moi lan Lap thi lay mot nut tu Front de ktra
            currentSearchNode.isExplored = true;//sau khi ktra thi danh dau Da Duoc Ktra
            ExploreNeighbor();//Tiep Tuc Xac dinh cac O con lai de ktra
            if(currentSearchNode.coordinate == destinateCoordinate)//neu den O Cuoi r thi Run false
            {
                isRunning = false;
            }
        }
    }

    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = destinationNode;// cho currentNode la dich den 

        path.Add(currentNode);//them vao dsach path
        currentNode.isPath = true;//currentNode da thanh mot Thanh Phan cua Duong di

        while(currentNode.connectedTo != null)//ktra coi ben canh currentNode co nut nao khong
        {
            currentNode = currentNode.connectedTo;  
            path.Add(currentNode);// co thi cho vo dsach path luon
            currentNode.isPath = true;// tiep tuc la mot Thanh Pha  cua duong di
        }

        path.Reverse();// dao nguoc lai dsach path tu Dich ve Goc
        return path;
    }

    public bool BlockPath(Vector2Int coordinate)
    {
        if (grid.ContainsKey(coordinate))
        {
            bool previousState = grid[coordinate].isWalkable;
            grid[coordinate].isWalkable = false;
            List<Node> newPath = GetNewPath();
            grid[coordinate].isWalkable = previousState;

            if(newPath.Count <= 0.5)
            {
                GetNewPath();
                return true;
            }
        }
        return false;
    }

    public void NotifyReceive()
    {
        BroadcastMessage("RecalculatePath",false, SendMessageOptions.DontRequireReceiver);
    }

}
