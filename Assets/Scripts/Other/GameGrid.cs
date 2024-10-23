using UnityEngine;

public class GameGrid
{
    private readonly float cellSize;
    private readonly Vector2 originPosition;

    private readonly GameGridCell[,] nodes;

    public GameGrid(int width, int height, float cellSize, Vector2 originPosition)
    {
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        var gridArray = new int[width, height];
        var debugTextArray = new TextMesh[width, height];


        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                Vector2 worldPosition = GetWorldPosition(x, y);

                bool isWalkable = CellIsWalkable(worldPosition);

                gridArray[x, y] = isWalkable ? 1 : 0;

                //nodes[x, y] = new GameGridCell(x, y, isWalkable);

                debugTextArray[x, y] = CreateWorldText(gridArray[x, y].ToString(), worldPosition + new Vector2(cellSize, cellSize) * 0.5f);

                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);

                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);



            }
        }

        // Desenha as bordas finais da grid
        //Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f); // Linha horizontal superior
        //Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);  // Linha vertical direita
    }



    private bool CellIsWalkable(Vector2 worldPosition)
    {
        // Definir o tamanho da célula como uma área de colisão
        var cellSizeVector = new Vector2(cellSize, cellSize);

        // Usar Physics2D.OverlapBox para verificar se há algum obstáculo na célula


        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(worldPosition, cellSizeVector, 0);

        // Verificar se algum dos colisores encontrados tem a tag "Object", que representa os obstáculos
        foreach (Collider2D collider in hitColliders)
        {
            if (collider.CompareTag("Object"))
            {
                return false; // A célula não é caminhável se houver algum obstáculo
            }
        }

        return true;
    }

    // Converte a posição da grid para posição no mundo
    private Vector2 GetWorldPosition(int x, int y)
    {
        return (new Vector2(x, y) * cellSize + originPosition + new Vector2(cellSize, cellSize) * 0.5f);
    }



    // Cria um TextMesh para mostrar os valores da grid
    private TextMesh CreateWorldText(string text, Vector2 worldPos)
    {
        var gameObject = new GameObject("GridText", typeof(TextMesh));
        gameObject.transform.position = worldPos;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.text = text;
        textMesh.fontSize = 128;
        textMesh.characterSize = 0.1f;
        textMesh.anchor = TextAnchor.MiddleCenter;
        textMesh.alignment = TextAlignment.Center;
        textMesh.color = Color.white;

        return textMesh;
    }
}