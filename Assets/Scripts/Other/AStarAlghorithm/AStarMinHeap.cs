using System;
using System.Collections.Generic;

public class AStarMinHeap<T> where T : IComparable<T>
{
    private readonly List<T> heap;

    public AStarMinHeap()
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
            Emptyheap();
        }
        return heap[0];
    }

    // Remove e retorna o menor elemento da heap (a raiz)
    public T Pop()
    {
        if (heap.Count == 0)
        {
            Emptyheap();
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

    private void Emptyheap()
    {
        throw new NotImplementedException("Empty Heap");
    }

}
