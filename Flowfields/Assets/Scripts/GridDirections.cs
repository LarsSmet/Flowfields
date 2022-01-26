using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;


public class GridDirection
{
    public readonly Vector2Int Vector;

    private GridDirection(int x, int y)
    {
        Vector = new Vector2Int(x, y);
    }

    public static implicit operator Vector2Int(GridDirection direction)
    {
        return direction.Vector;
    }

    //Make each direction
    public static readonly GridDirection none = new GridDirection(0, 0);
    public static readonly GridDirection north = new GridDirection(0, 1);
    public static readonly GridDirection south = new GridDirection(0, -1);
    public static readonly GridDirection east = new GridDirection(1, 0);
    public static readonly GridDirection west = new GridDirection(-1, 0);
    public static readonly GridDirection northEast = new GridDirection(1, 1);
    public static readonly GridDirection northWest = new GridDirection(-1, 1);
    public static readonly GridDirection southEast = new GridDirection(1, -1);
    public static readonly GridDirection southWest = new GridDirection(-1, -1);

    public static GridDirection GetDirectionFromV2I(Vector2Int vector)
    {
        return CardinalAndIntercardinalDirections.DefaultIfEmpty(none).FirstOrDefault(direction => direction == vector);
    }

 

    public static readonly List<GridDirection> CardinalDirections = new List<GridDirection>
    {
        north,
        east,
        south,
        west
    };

    public static readonly List<GridDirection> CardinalAndIntercardinalDirections = new List<GridDirection>
    {
        north,
        northEast,
        east,
        southEast,
        south,
        southWest,
        west,
        northWest
    };

    //used for the vectors
    public static readonly List<GridDirection> AllDirections = new List<GridDirection>
    {
        none,
        north,
        northEast,
        east,
        southEast,
        south,
        southWest,
        west,
        northWest
    };
}
