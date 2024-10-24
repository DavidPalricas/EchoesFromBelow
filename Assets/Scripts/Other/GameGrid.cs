using UnityEngine;

/// <summary>
/// The GameGrid class is responsible for creating the game's grid and checking if its cells are walkable.
/// </summary>
public class GameGrid
{
    /// <summary>
    /// The cellSize variable is responsible for storing the size of the grid's cells.
    /// </summary>
    private readonly float cellSize;

    /// <summary>
    /// The originPosition variable is responsible for storing the position of the grid's origin.
    /// </summary>
    private readonly Vector2 originPosition;

    /// <summary>
    /// The cells variable is responsible for storing the grid's cells.
    /// </summary>
    private readonly GameGridCell[, ] cells;


    public int gridSize;


    private readonly int width,height;


    /// <summary>
    /// Initializes a new instance of the <see cref="GameGrid"/> class.
    /// It is responsible for setting the grid's cell size and origin position.
    /// And creating the grid calling the CreatingGameGrid method.
    /// </summary>
    /// <param name="width">The width variable stores the grid's width.</param>
    /// <param name="height">The height variable stores the grid's height.</param>
    /// <param name="cellSize">The cellSize variable is responsible for storing the size of the grid's cells..</param>
    /// <param name="originPosition">The originPosition variable is responsible for storing the position of the grid's origin.</param>
    public GameGrid(int width, int height, float cellSize, Vector2 originPosition)
    {
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        var gridArray = new int[width, height];
        var debugTextArray = new TextMesh[width, height];


        this.width = width;
        this.height = height;

        gridSize = width * height;

        cells = new GameGridCell[width, height];

        CreatingGameGrid(gridArray, debugTextArray);
    }


    /// <summary>
    /// The CreatingGameGrid method is responsible for creating the game's grid.
    /// It sets the grid's cells as walkable or not, and creates a TextMesh to show the values of the grid.
    /// </summary>
    /// <param name="gridArray">The grid array.</param>
    /// <param name="debugTextArray">The debug text array.</param>
    private void CreatingGameGrid(int [, ] gridArray, TextMesh[, ] debugTextArray )
    {
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                Vector2 worldPosition = GetGamePosition(x, y);

                bool isWalkable = CellIsWalkable(worldPosition);

                gridArray[x, y] = isWalkable ? 1 : 0;

                if (isWalkable)
                {
                    cells[x, y] = new GameGridCell(x, y, isWalkable);
                }

                debugTextArray[x, y] = CreateGameText(gridArray[x, y].ToString(), worldPosition + new Vector2(cellSize, cellSize) * 0.5f);

                Debug.DrawLine(GetGamePosition(x, y), GetGamePosition(x + 1, y), Color.white, 100f);

                Debug.DrawLine(GetGamePosition(x, y), GetGamePosition(x, y + 1), Color.white, 100f);
            }
        }

        // Desenha as bordas finais da grid
        //Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f); // Linha horizontal superior
        //Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);  // Linha vertical direita
    }

    /// <summary>
    /// The CreateGameText method is responsible for creating a TextMesh to show the values of the grid.
    /// </summary>
    /// <param name="text">The text variable stores the string value to show on the grid.</param>
    /// <param name="gamePosition">The gamePosition stores the cell position in the game.</param>
    /// <returns></returns>
    private TextMesh CreateGameText(string text, Vector2 gamePosition)
    {
        var gameObject = new GameObject("GridText", typeof(TextMesh));
        gameObject.transform.position = gamePosition;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.text = text;
        textMesh.fontSize = 128;
        textMesh.characterSize = 0.1f;
        textMesh.anchor = TextAnchor.MiddleCenter;
        textMesh.alignment = TextAlignment.Center;
        textMesh.color = Color.white;

        return textMesh;
    }

    /// <summary>
    /// The GetGamePosition method is responsible for getting the position of a cell in the game.
    /// </summary>
    /// <param name="x">The x varaible stores the x coordinate of a cell.</param>
    /// <param name="y">The y varaible stores the y coordinate of a cell.</param>
    /// <returns>Returns the game postion of the cell</returns>
    public Vector2 GetGamePosition(int x, int y)
    {
        return (new Vector2(x, y) * cellSize + originPosition + new Vector2(cellSize, cellSize) * 0.5f);
    }

    /// <summary>
    /// The CellIsWalkable method is responsible for checking if a cell is walkable.
    /// The method uses Physics2D.OverlapBox to check if there is any obstacle in the cell.
    /// If there is an obstacle, the cell is not walkable.
    /// </summary>
    /// <param name="gamePosition">The gamePosition variable stores the cell's position in the game.</param>
    /// <returns>
    ///   <c>true</c> if the specified world position is walkable; otherwise, <c>false</c>.
    /// </returns>
    public bool CellIsWalkable(Vector2 gamePosition)
    {
        // Defines the size of the cell as a collision area
        var cellSizeVector = new Vector2(cellSize, cellSize);

        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(gamePosition, cellSizeVector, 0);

        foreach (Collider2D collider in hitColliders)
        {
            if (collider.CompareTag("Object"))
            {
                return false; 

            }
        }

        return true;
    }


    /// <summary>
    /// The GetCell method is responsible for getting the cell of a game object.
    /// </summary>
    /// <param name="gameObjectPosition">The gameObjectPosition stores the game object's position.</param>
    /// <returns></returns>
    public GameGridCell GetCell (Vector2 gameObjectPosition)
    {
        // Alines the grid with the origin position
        Vector2 adjustedGamePosition = gameObjectPosition/cellSize - originPosition - new Vector2(cellSize, cellSize) * 0.5f;

        // Convert the world position to the grid position
        int x = Mathf.FloorToInt(adjustedGamePosition.x / cellSize);
        int y = Mathf.FloorToInt(adjustedGamePosition.y / cellSize);

        // Check if the cell is inside the grid 
        if (x >= 0 && x < cells.GetLength(0) && y >= 0 && y < cells.GetLength(1))
        {
            
            return cells[x, y];
        }
        else
        {
            return null;
        }
    }



    public GameGridCell getCell(int x, int y)
    {
        return cells[x, y];
    }


    public bool IsWithinBounds(int x, int y)
    {
        return x >= 0 && x < width && y >= 0 && y < height;
    }
}