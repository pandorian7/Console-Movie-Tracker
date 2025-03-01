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


// public class MergeSort 
// {
//     public static void Sort<T1, T2>(DynamicArray<T1> arr ,Func<T1, T2> key) where T2 : IComparable<T2>
//     {
//         // implement merge sort
//     }

//     public static void Merge<T1, T2>(DynamicArray<T1> arr ,Func<T1, T2> key) where T2 : IComparable<T2>
//     {
//         int mid = arr.Count / 2;
//         int leftSize = mid;
//         int rightSize = arr.Count - mid;

//         // create two sub-arrays
//         DynamicArray<T1> left = new DynamicArray<T1>(leftSize);
//         DynamicArray<T1> right = new DynamicArray<T1>(rightSize);

//         // copy elements from arr to left and right
//         for (int i = 0; i < leftSize; i++)
//         {
//             left.AddLast(arr.At(i));
//         }

//         for (int i = mid; i < arr.Count; i++)
//         {
//             right.AddLast(arr.At(i));
//         }

//         // merge left and right arrays
//         int i = 0, j = 0, k = 0;
//         while (i < leftSize && j < rightSize)
//         {
//             if (key(left.At(i)).CompareTo(key(right.At(j))) < 0)
//             {
//                 arr.Set(k, left.At(i));
//                 i++;
//             }
//             else
//             {
//                 arr.Set(k, right.At(j));
//                 j++;
//             }
//             k++;
//         }
//     }
// }
