using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AStar
{   
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;


    private static List<GameGridCell> GetNeighbours(GameGridCell currentCell, GameGrid gameGrid)
    {
        int[,] directions = new int[,]
        {
        { -1,  0 }, // Left
        {  1,  0 }, // Right
        {  0, -1 }, // Down
        {  0,  1 }, // Up
        { -1, -1 }, // Down left Diagonal
        { -1,  1 }, // Up left Diagonal
        {  1, -1 }, // Right Down Diagonal
        {  1,  1 }  // Up Right Diagonal
        };

        var neighbours = new List<GameGridCell>(8);  

        for (int i = 0; i < directions.GetLength(0); i++)
        {
            int checkX = currentCell.X + directions[i, 0];
            int checkY = currentCell.Y + directions[i, 1];


            Vector2 gamePosition = gameGrid.GetGamePosition(checkX, checkY);

            // Checa se as coordenadas estão dentro dos limites do grid
            if (gameGrid.IsWithinBounds(checkX, checkY) && gameGrid.CellIsWalkable(gamePosition))
            {
               
               neighbours.Add(gameGrid.getCell(checkX,checkY));  // Referencia diretamente a célula no grid
                
            }
        }

        return neighbours;
    }

    private static List<GameGridCell> FinalPath(GameGridCell startCell, GameGridCell targetCell)
    {
        var path = new List<GameGridCell>();
        var currentCell = targetCell;

        while (currentCell != startCell)
        {
            path.Add(currentCell);
         
            if (currentCell.Parent != null)
            {
                currentCell = currentCell.Parent;
            } 
        }

        path.Reverse();
        return path;
    }

    private static int GetDistance(GameGridCell startCell, GameGridCell targetCell)
    {   
        int xDistance = Mathf.Abs(startCell.X - targetCell.X);
        int yDistance = Mathf.Abs(startCell.Y - targetCell.Y);

        int remaining = Mathf.Abs(xDistance - yDistance);

        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;

    }

    public static List<GameGridCell> FindPath(GameGridCell startCell, GameGridCell targetCell, GameGrid gameGrid)
    {
        var openList = new AStarMinHeap<GameGridCell>();
        var closedList = new List<GameGridCell>();

        startCell.Heruistic = 0;
        startCell.EvaluationFunction = 0;
        startCell.Cost = 0; 
        startCell.Parent = null;

        openList.Add(startCell);

        while (openList.Count > 0)
        {
            var currentCell = openList.Pop();

            if (currentCell.X == targetCell.X && currentCell.Y == targetCell.Y)
            {
                return FinalPath(startCell, targetCell);
            }

            closedList.Add(currentCell);

            foreach (GameGridCell neighbourCell in GetNeighbours(currentCell, gameGrid))
            {
                if (closedList.Contains(neighbourCell) && neighbourCell != null)
                {
                    continue;
                }

                int tentativeCost = currentCell.Cost + GetDistance(currentCell, neighbourCell);


                Debug.Log("Tentative Cost: " + tentativeCost);


                if (tentativeCost < neighbourCell.Cost)
                {
                    neighbourCell.Parent = currentCell;
                    neighbourCell.Cost = tentativeCost;
                    neighbourCell.Heruistic = GetDistance(neighbourCell, targetCell);
                    neighbourCell.EvaluationFunction = neighbourCell.Cost + neighbourCell.Heruistic;

                    if (!openList.Remove(neighbourCell))
                    {
                        openList.Add(neighbourCell);
                    }
                }
            }
        }

        return new List<GameGridCell>();  // Retorna lista vazia se não houver caminho
    }   
}

