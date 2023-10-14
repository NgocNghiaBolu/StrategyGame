using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] bool isPlaceable;

    public bool IsPlaceable { get { return isPlaceable; } }

    GridManager gridManager;
    Vector2Int coordinate = new Vector2Int();

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
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

    private void OnMouseDown()
    {
        if (isPlaceable)
        {
            bool isPlaced = towerPrefab.CreateTower(towerPrefab, transform.position);
            //Debug.Log(transform.name);
            isPlaceable = !isPlaced;// neu da click vao thi k dc click nua
        }
    }
}
