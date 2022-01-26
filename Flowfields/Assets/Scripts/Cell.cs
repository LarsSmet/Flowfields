using UnityEngine;

public class Cell 
{

    public Vector3 worldPosition;
    public Vector2Int gridIndex;

    public int cost;
    
    public int maxCostIncrease;

    public int bestCost;

    public int maxBestCost;

    public GridDirection bestDirection;

    public Cell(Vector3 _worldPosition, Vector2Int _gridIndex)
    {
        worldPosition = _worldPosition;
        gridIndex = _gridIndex;
        cost = 1;
        maxCostIncrease = 300;
        maxBestCost = 100000;
        bestCost = maxBestCost;
        bestDirection = GridDirection.none;
    }   

    public void IncreaseCost(int increase)
    {
        if(cost == maxCostIncrease)
        {
            //if cost is alrdy at max cost, do nothing
            return;
        }

        if(cost + increase >= maxCostIncrease)
        {
            //you can't go higher than the max cost
            cost = maxCostIncrease;
        }
        else
        {
            cost += increase;
        }

    }
}
