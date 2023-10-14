using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class Coordinate : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.red;
    [SerializeField] Color blockColor = Color.green;
    [SerializeField] Color exploreColor = Color.green;
    [SerializeField] Color pathColor = new Color(1f,0.5f,0f);

    TextMeshPro label;
    Vector2Int coordinate = new Vector2Int();
    GridManager gridManager;

    private void Awake()
    {
        label = GetComponent<TextMeshPro>();
        label.enabled = true;
        gridManager = FindObjectOfType<GridManager>();
        DisplayCoorDinate();
    }

    void Update()
    {
        if (!Application.isPlaying)
        { 
            DisplayCoorDinate();
            UpdateNameObject();
            label.enabled = true;
        }
        SetLabelColor();
        ToggleLabel();
    }

    void SetLabelColor()
    {
        if(gridManager == null) { return; }

        Node node = gridManager.GetNode(coordinate);

        if(node == null) { return; }

        if(!node.isWalkable)
        {
            label.color = blockColor;
        }
        else if (node.isPath)
        {
            label.color = pathColor;
        }
        else if (node.isExplored)
        {
            label.color = exploreColor;
        }
        else
        {
            label.color = defaultColor;
        }
    }

    void ToggleLabel()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }

    private void DisplayCoorDinate()
    {
        if(gridManager == null) { return; }
        coordinate.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize);
        coordinate.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize);

        label.text = coordinate.x + "," + coordinate.y;
    }

    void UpdateNameObject()
    {
        transform.parent.name = coordinate.ToString();
    }
}
