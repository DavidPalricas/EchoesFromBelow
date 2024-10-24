using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AStar
{   
    private static List <GameGridCell> openList;
    private static List<GameGridCell> closedList;

    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;


    private static List<GameGridCell> GetNeighbours(GameGridCell currentCell, GameGrid gameGrid)
    {
        // Lista dos deslocamentos para os 8 vizinhos (horizontal, vertical e diagonais)
        int[,] directions = new int[,]
        {
        { -1,  0 }, // Esquerda
        {  1,  0 }, // Direita
        {  0, -1 }, // Baixo
        {  0,  1 }, // Cima
        { -1, -1 }, // Diagonal inferior esquerda
        { -1,  1 }, // Diagonal superior esquerda
        {  1, -1 }, // Diagonal inferior direita
        {  1,  1 }  // Diagonal superior direita
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
        var  openList = new MinHeap<GameGridCell>();
        closedList = new List<GameGridCell>();

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

public class MinHeap<T> where T : IComparable<T>
{
    private List<T> heap;

    public MinHeap()
    {
        heap = new List<T>();
    }

    // Retorna o número de elementos na heap
    public int Count => heap.Count;

    // Retorna o menor elemento (raiz) da heap
    public T Peek()
    {
        if (heap.Count == 0)
        {
            throw new InvalidOperationException("A heap está vazia.");
        }
        return heap[0];
    }

    // Remove e retorna o menor elemento da heap (a raiz)
    public T Pop()
    {
        if (heap.Count == 0)
        {
            throw new InvalidOperationException("A heap está vazia.");
        }

        T root = heap[0];
        heap[0] = heap[heap.Count - 1];
        heap.RemoveAt(heap.Count - 1);
        HeapifyDown(0);
        return root;
    }

    // Adiciona um novo elemento à heap
    public void Add(T item)
    {
        heap.Add(item);
        HeapifyUp(heap.Count - 1);
    }

    // Remove um item específico da heap
    public bool Remove(T item)
    {
        int index = heap.IndexOf(item);
        if (index == -1) return false;

        heap[index] = heap[heap.Count - 1];
        heap.RemoveAt(heap.Count - 1);
        HeapifyDown(index);
        return true;
    }

    // Função para manter a propriedade da Min-Heap ao adicionar um elemento
    private void HeapifyUp(int index)
    {
        while (index > 0)
        {
            int parentIndex = (index - 1) / 2;
            if (heap[index].CompareTo(heap[parentIndex]) >= 0)
            {
                break;
            }

            Swap(index, parentIndex);
            index = parentIndex;
        }
    }

    // Função para manter a propriedade da Min-Heap ao remover a raiz
    private void HeapifyDown(int index)
    {
        int lastIndex = heap.Count - 1;
        while (index < lastIndex)
        {
            int leftChildIndex = 2 * index + 1;
            int rightChildIndex = 2 * index + 2;
            int smallest = index;

            if (leftChildIndex <= lastIndex && heap[leftChildIndex].CompareTo(heap[smallest]) < 0)
            {
                smallest = leftChildIndex;
            }

            if (rightChildIndex <= lastIndex && heap[rightChildIndex].CompareTo(heap[smallest]) < 0)
            {
                smallest = rightChildIndex;
            }

            if (smallest == index)
            {
                break;
            }

            Swap(index, smallest);
            index = smallest;
        }
    }

    private void Swap(int i, int j)
    {
        T temp = heap[i];
        heap[i] = heap[j];
        heap[j] = temp;
    }
}

