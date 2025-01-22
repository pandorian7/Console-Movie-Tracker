namespace DynamicArray;

public class DynamicArray<T>
{
    private T[] Items { get; set; } = Array.Empty<T>();
    private int Count { get; set; } = 0;

    private  int size;


    public DynamicArray()
    {
        size = 4;
        Items = new T[size];
        Count = 0;
    }

    public void Print()
    {
        for (int i = 0; i < Count; i++)
        {
            Console.WriteLine(Items[i]);
        }
    }

    private void expand()
    {
        size *= 2;
        T[] temp = new T[size];
        for (int i = 0; i < Count; i++)
        {
            temp[i] = Items[i];
        }
        Items = temp;
    }

    private void shrink()
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
            expand();
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
            expand();
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
            shrink();
        }
    }

    public void RemoveLast()
    {
        Count--;
        if (Count <= size / 4)
        {
            shrink();
        }
    }

    



}

