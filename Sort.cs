using System;
using MovieTracker;

namespace DSA;

public class InsertionSort
{
    public static void Sort<T1, T2>(DynamicArray<T1> arr , Func<T1, T2> key) where T2 : IComparable<T2>
    {
        for(int i=1; i<arr.Count; i++)
        {
            T1 element = arr.At(i);
            int j = i - 1;
            while(j >= 0 && key(element).CompareTo(key(arr.At(j))) < 0)
            {
                arr.Set(j + 1, arr.At(j));
                j--;
            }
            arr.Set(j + 1, element) ;
        }
    }
}


public class SelectionSort
{
    public static void Sort<T1, T2>(DynamicArray<T1> arr , Func<T1, T2> key) where T2 : IComparable<T2>
    {
        for(int i=0; i<arr.Count; i++)
        {
            int minIndex = i;
            for(int j=i+1; j<arr.Count ; j++)
            {
                if(key(arr.At(j)).CompareTo(key(arr.At(minIndex))) < 0)
                {
                    minIndex = j;
                }
            }
            T1 temp = arr.At(i);
            arr.Set(i, arr.At(minIndex));
            arr.Set(minIndex, temp);
        }
    }
}
