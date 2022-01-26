using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;



public enum FlowFieldDisplayType { None, AllIcons, DestinationIcon, CostField, IntegrationField };

public class GridDebug : MonoBehaviour
{
    public GridModifier gridModifier;
    public bool displayGrid;

    public FlowFieldDisplayType curDisplayType;

    private Vector2Int gridSize;
    private float cellRadius;
    private FlowField flowField;

    private Sprite[] ffIcons;

    private void Start()
    {
        ffIcons = Resources.LoadAll<Sprite>("Sprites/FFicons");

    }

    public void SetFlowField(FlowField newFlowField)
    {
        flowField = newFlowField;
        cellRadius = newFlowField.cellRadius;
        gridSize = newFlowField.gridSize;
    }

    public void DrawFlowField()
    {
        ClearCellDisplay();

        switch (curDisplayType)
        {
            case FlowFieldDisplayType.AllIcons:
                DisplayAllCells();

                break;

            case FlowFieldDisplayType.DestinationIcon:
                DisplayDestinationCell();
                break;

            default:
                break;
        }
    }

    private void DisplayAllCells()
    {
        if (flowField == null) 
        {
            return;
        }
        foreach (Cell currentCell in flowField.grid)
        {
            DisplayCell(currentCell);
  
        }
    }

    private void DisplayDestinationCell()
    {
        if (flowField == null)
        { 
            return;
        }

        DisplayCell(flowField.destionationCell);
    }

    private void DisplayCell(Cell cell)
    {
        
        GameObject iconGO = new GameObject();

        SpriteRenderer iconSR = iconGO.AddComponent<SpriteRenderer>();
        iconGO.transform.parent = transform;
        Vector3 scale = new Vector3(3, 3, 3.0f);
        iconGO.transform.localScale = scale;
        iconGO.transform.position = cell.worldPosition;
  
        if (cell.cost == 0)
        {
            iconSR.sprite = ffIcons[3];
            Quaternion newRot = Quaternion.Euler(90, 0, 0);
            iconGO.transform.rotation = newRot;
       
        }
        else if (cell.cost == cell.maxCostIncrease)
        {
            iconSR.sprite = ffIcons[2];
            Quaternion newRot = Quaternion.Euler(90, 0, 0);
            iconGO.transform.rotation = newRot;
           
        }
        else if (cell.bestDirection == GridDirection.north)
        {
            iconSR.sprite = ffIcons[0];
            Quaternion newRot = Quaternion.Euler(90, 0, 0);
            iconGO.transform.rotation = newRot;
        }
        else if (cell.bestDirection == GridDirection.south)
        {
            iconSR.sprite = ffIcons[0];
            Quaternion newRot = Quaternion.Euler(90, 180, 0);
            iconGO.transform.rotation = newRot;
        }
        else if (cell.bestDirection == GridDirection.east)
        {
            iconSR.sprite = ffIcons[0];
            Quaternion newRot = Quaternion.Euler(90, 90, 0);
            iconGO.transform.rotation = newRot;
        }
        else if (cell.bestDirection == GridDirection.west)
        {
            iconSR.sprite = ffIcons[0];
            Quaternion newRot = Quaternion.Euler(90, 270, 0);
            iconGO.transform.rotation = newRot;
        }
        else if (cell.bestDirection == GridDirection.northEast)
        {
            iconSR.sprite = ffIcons[1];
            Quaternion newRot = Quaternion.Euler(90, 0, 0);
            iconGO.transform.rotation = newRot;
        }
        else if (cell.bestDirection == GridDirection.northWest)
        {
            iconSR.sprite = ffIcons[1];
            Quaternion newRot = Quaternion.Euler(90, 270, 0);
            iconGO.transform.rotation = newRot;
        }
        else if (cell.bestDirection == GridDirection.southEast)
        {
            iconSR.sprite = ffIcons[1];
            Quaternion newRot = Quaternion.Euler(90, 90, 0);
            iconGO.transform.rotation = newRot;
        }
        else if (cell.bestDirection == GridDirection.southWest)
        {
            iconSR.sprite = ffIcons[1];
            Quaternion newRot = Quaternion.Euler(90, 180, 0);
            iconGO.transform.rotation = newRot;
        }
        else
        {
            iconSR.sprite = ffIcons[0];
        }
    }

    public void ClearCellDisplay()
    {
        foreach (Transform t in transform)
        {
            GameObject.Destroy(t.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        if (displayGrid)
        {
            if (flowField == null)
            {
                DrawGrid(gridModifier.gridSize, Color.yellow, gridModifier.cellRadius);
            }
            else
            {
                DrawGrid(gridSize, Color.black, cellRadius);
            }
        }

        if (flowField == null) 
        {
            return;
        }

        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.alignment = TextAnchor.MiddleCenter;
       
        style.normal.textColor = Color.black;
        style.fontSize = 7;

        switch (curDisplayType)
        {
            case FlowFieldDisplayType.CostField:

                foreach (Cell curCell in flowField.grid)
                {
                    Handles.Label(curCell.worldPosition, curCell.cost.ToString(), style);
          
                }
                break;

            case FlowFieldDisplayType.IntegrationField:

                foreach (Cell curCell in flowField.grid)
                {
                    Handles.Label(curCell.worldPosition, curCell.bestCost.ToString(), style);
                }
                break;

            default:
                break;
        }

    }

    private void DrawGrid(Vector2Int drawGridSize, Color drawColor, float drawCellRadius)
    {
        Gizmos.color = drawColor;
        for (int x = 0; x < drawGridSize.x; x++)
        {
            for (int y = 0; y < drawGridSize.y; y++)
            {
                Vector3 center = new Vector3(drawCellRadius * 2 * x + drawCellRadius, 0, drawCellRadius * 2 * y + drawCellRadius);
                Vector3 size = Vector3.one * drawCellRadius * 2;
                //y is 0
                Gizmos.DrawWireCube(center, new Vector3(size.x,0, size.z));
                
            }
        }
    }
}
