using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridModifier : MonoBehaviour
{

    public Vector2Int gridSize;
    public float cellRadius = 1.0f;
    private FlowField flowfield;
    public GridDebug gridDebug;

    private void InitializeFlowField()
    {
        flowfield = new FlowField(cellRadius, gridSize);
        flowfield.CreateGrid();
        gridDebug.SetFlowField(flowfield);
  
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            InitializeFlowField();

            flowfield.CreateCostField();

            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 85.0f);
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
            Cell destinationCell = flowfield.GetCellFromWorldPos(worldMousePos);
            flowfield.CreateIntegrationField(destinationCell);


        }

        
    }


}
