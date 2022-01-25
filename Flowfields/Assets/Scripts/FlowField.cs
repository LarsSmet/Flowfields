using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowField
{
    // 2d array of cells -> grid
    public Cell[,] grid { get; private set; }
    public Vector2Int gridSize { get; private set; }
    public float cellRadius { get; private set; }

    private float cellDiameter;

    public Cell destionationCell;

    public FlowField(float _cellRadius, Vector2Int _gridSize)
    {
        cellRadius = _cellRadius;
        cellDiameter = _cellRadius * 2;
        gridSize = _gridSize;

    }

   

    public void CreateGrid()
    {
        grid = new Cell[gridSize.x, gridSize.y];
        Vector3 position;

        for(int col = 0; col < gridSize.x; col++)
        {

            for(int row = 0; row < gridSize.y; row++)
            {
                //make cell, y postion is 0 -> working in 2d top down
                position = new Vector3(col * cellDiameter + cellRadius, 0, row * cellDiameter + cellRadius);
                grid[col, row] = new Cell(position, new Vector2Int(col, row));
                

            }

        }

    }

    public void CreateCostField()
    {
        //https://docs.unity3d.com/ScriptReference/Physics.OverlapBox.html

        Vector3 cellHalfExtends = Vector3.one * cellRadius;

        int terrainMask = LayerMask.GetMask("Mud", "Wall");

        //go over each cell, if it hits a certain terrain type it increases it cost based on which type

        foreach( Cell currentCell in grid)
        {
            Collider[] obstacles = Physics.OverlapBox(currentCell.worldPosition, cellHalfExtends, Quaternion.identity, terrainMask);
            bool hasIncreasedCost = false;

            foreach(Collider col in obstacles)
            {
                if (col.gameObject.layer == LayerMask.NameToLayer("Wall") )
                {
                    

                    currentCell.IncreaseCost(300);
                    continue;
                }
                else if(hasIncreasedCost == false && col.gameObject.layer == LayerMask.NameToLayer("Mud"))
                {
                    currentCell.IncreaseCost(3);
                    //prevent increasing the same cell that have multiple muds // can change to mudincreased cost if want to add more values
                    hasIncreasedCost = true;
                }
            }

           
        }
    }


    public void CreateIntegrationField(Cell _destinationCell)
    {

        destionationCell = _destinationCell;

        _destinationCell.cost = 0;
        destionationCell.bestCost = 0;


        Queue<Cell> cellsToCheck = new Queue<Cell>();

        //put destinationCell on the queue
        cellsToCheck.Enqueue(destionationCell);

        while(cellsToCheck.Count > 0)
        {
            Cell currentCell = cellsToCheck.Dequeue();
            List<Cell> currentNeighbors = GetNeigbors(currentCell.gridIndex, GridDirection.CardinalDirections);

            foreach (Cell currentNeighbor in currentNeighbors)
            {
                if(currentNeighbor.cost == currentNeighbor.maxCostIncrease )
                {
                    continue;
                }
                //set the bestcost of the neighbor to current cell + neighbor cost -> makes us find the shortest path
                if(currentNeighbor.cost + currentCell.bestCost < currentNeighbor.bestCost)
                {
                    currentNeighbor.bestCost = currentNeighbor.cost + currentCell.bestCost;
                    cellsToCheck.Enqueue(currentNeighbor);
                }
            }

        }
    }

    private List<Cell> GetNeigbors(Vector2Int nodeIndex, List<GridDirection> directions)
    {
        List<Cell> neighborCells = new List<Cell>();

        foreach(Vector2Int currentDirection in directions)
        {
            Cell newNeighbor = GetCellAtRelativePos(nodeIndex, currentDirection);
            if(newNeighbor != null)
            {
                neighborCells.Add(newNeighbor);
            }

        }

        return neighborCells;

    }

    private Cell GetCellAtRelativePos(Vector2Int originPos, Vector2Int relativePos)
    {
        Vector2Int finalPos = originPos + relativePos;

        if(finalPos.x < 0 || finalPos.x >= gridSize.x || finalPos.y < 0 || finalPos.y >= gridSize.y)
        {
            return null;
        }
        else
        {
            return grid[finalPos.x, finalPos.y];
        }
    }

    public Cell GetCellFromWorldPos(Vector3 worldPos)
    {
        float percentX = worldPos.x / (gridSize.x * cellDiameter);
        float percentY = worldPos.z / (gridSize.y * cellDiameter);

        //clamps value between 0 and 1
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.Clamp(Mathf.FloorToInt((gridSize.x) * percentX), 0, gridSize.x - 1);
        int y = Mathf.Clamp(Mathf.FloorToInt((gridSize.y) * percentY), 0, gridSize.y - 1);

        
        return grid[x,y];

    }

}
