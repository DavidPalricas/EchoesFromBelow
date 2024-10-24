using System;

/// <summary>
/// The GameGridCell class is responsible for storing the game grid's cells.
/// </summary>
public class GameGridCell : IComparable<GameGridCell>
{
    /// <summary>
    ///  Defines the variable X to store the cell's x position and a getter to access it.
    /// </summary>
    public int X { get; }

    /// <summary>
    /// Defines the variable X to store the cell's x position and a getter to access it.
    /// </summary>
    public int Y { get; }

    /// <summary>
    /// Defines the variable IsWalkable to store if the cell is walkable or not and a getter and setter to access it.
    /// </summary>
    public bool IsWalkable { get; set; }

    /// <summary>
    /// Defines the variable Heruistic to store the cell's heuristic value and a getter and setter to access it.
    /// </summary>
    public int Heruistic { get; set; }


    public int Cost { get; set; }

    public int EvaluationFunction { get; set; }

    /// <summary>
    /// Defines the variable Parent to store the cell's Parent and a getter and setter to access it.
    /// </summary>
    public GameGridCell Parent  { get; set;}

    /// <summary>
    /// Initializes a new instance of the <see cref="GameGridCell"/> class.
    /// </summary>
    /// <param name="x">The x variable stores the cell's x position.</param>
    /// <param name="y">The y variable stores the cell's x position.</param>
    /// <param name="isWalkable">The isWalkable variable indicates if a cell is walkable or not.</param>
    public GameGridCell(int x, int y, bool isWalkable) 
    {
        X = x;
        Y = y;
        IsWalkable = isWalkable;

        Cost = int.MaxValue;
    }

    public int CompareTo(GameGridCell other)
    {
        if (EvaluationFunction == other.EvaluationFunction)
        {
            return Heruistic.CompareTo(other.Heruistic);  // Usamos a heurística como desempate
        }
        return EvaluationFunction.CompareTo(other.EvaluationFunction);
    }
}

