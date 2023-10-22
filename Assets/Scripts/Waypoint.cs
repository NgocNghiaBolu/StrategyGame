using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] bool isPlaceable;

    public bool IsPlaceable { get { return isPlaceable; } }

    GridManager gridManager;
    PathFinder pathFinder;
    Vector2Int coordinate = new Vector2Int();

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    void Start()
    {
        if(gridManager != null)
        {
            coordinate = gridManager.GetCoordinateFromPosition(transform.position);
            if(!isPlaceable)
            {
                gridManager.BlockNode(coordinate); 
            }
        }
    }

    void OnMouseDown()
    {
        if (gridManager.GetNode(coordinate).isWalkable && !pathFinder.BlockPath(coordinate))
        {
            bool isPlaced = towerPrefab.CreateTower(towerPrefab, transform.position);
            //Debug.Log(transform.name);
            if (isPlaced)
            {
                gridManager.BlockNode(coordinate);
                pathFinder.NotifyReceive();
            }

        }
    }
}
