using System.Diagnostics.Contracts;

namespace DSA;

public class DynamicArray<T>
{

    private T[] Items { get; set; } = Array.Empty<T>();
    public int Count { get; private set; } = 0;

    private  int size;


    public DynamicArray()
    {
        size = 4;
        Items = new T[size];
        Count = 0;
    }


    private void Expand()
    {
        size *= 2;
        T[] temp = new T[size];
        for (int i = 0; i < Count; i++)
        {
            temp[i] = Items[i];
        }
        Items = temp;
    }

    private void Shrink()
    {
        size /= 2;
        T[] temp = new T[size];
        for (int i = 0; i < Count; i++)
        {
            temp[i] = Items[i];
        }
        Items = temp;
    }

    public void AddLast(T item)
    {
        Items[Count] = item;
        Count++;
        if (Count == size)
        {
            Expand();
        }
    }


    public void AddAt(int index, T item)
    {
        if (index < 0 || index > Count)
        {
            throw new IndexOutOfRangeException();
        }

        for (int i = Count; i > index; i--)
        {
            Items[i] = Items[i - 1];
        }

        Items[index] = item;
        Count++;
        
        if (Count == size)
        {
            Expand();
        }
    }
    
    public void RemoveAt(int index)
    {
        if (index < 0 || index >= Count)
        {
            throw new IndexOutOfRangeException();
        }

        for (int i = index; i < Count - 1; i++)
        {
            Items[i] = Items[i + 1];
        }

        Count--;
        if (Count <= size / 4)
        {
            Shrink();
        }
    }

    public void RemoveLast()
    {
        Count--;
        if (Count <= size / 4)
        {
            Shrink();
        }
    }

    public T At(int index)
    {
        if (index < 0 || index >= Count)
        {
            throw new IndexOutOfRangeException();
        }

        return Items[index];
    }

    public void Set(int index, T item)
    {
        if (index < 0 || index >= Count)
        {
            throw new IndexOutOfRangeException();
        }

        Items[index] = item;
    }
}

