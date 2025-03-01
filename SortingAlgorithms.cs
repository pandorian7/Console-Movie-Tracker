using System;

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

public class BubbleSort {
    public static void Sort<T1, T2>(DynamicArray<T1> arr, Func<T1, T2> key) where T2 : IComparable<T2> {
        int n = arr.Count;
        bool swapped;
        for (int i = 0; i < n - 1; i++) {
            swapped = false;
            for (int j = 0; j < n - i - 1; j++) {
                if (key(arr.At(j)).CompareTo(key(arr.At(j+1))) > 0) {
                    T1 temp = arr.At(j);
                    arr.Set(j,arr.At(j + 1));
                    arr.Set(j + 1, temp);
                    swapped = true;
                }
            }
            if (!swapped)
                break;
        }
    }
}


// public class MergeSort
// {
//     public static void Sort<T, K>(DynamicArray<T> arr, Func<T, K> key) where K : IComparable<K>
//     {
//         if (arr.Count > 1)
//         {
//             Sort(arr, 0, arr.Count - 1, key);
//         }
//     }

//     private static void Sort<T, K>(DynamicArray<T> arr, int left, int right, Func<T, K> key) where K : IComparable<K>
//     {
//         if (left >= right) return;

//         int mid = (left + right) / 2;
//         Sort(arr, left, mid, key);
//         Sort(arr, mid + 1, right, key);
//         Merge(arr, left, mid, right, key);
//     }

//     private static void Merge<T, K>(DynamicArray<T> arr, int left, int mid, int right, Func<T, K> key) where K : IComparable<K>
//     {
//         int leftSize = mid - left + 1;
//         int rightSize = right - mid;

//         var leftArr = new DynamicArray<T>(leftSize);
//         var rightArr = new DynamicArray<T>(rightSize);

//         for (int i = 0; i < leftSize; i++) leftArr.Set(i, arr.At(left + i));
//         for (int j = 0; j < rightSize; j++) rightArr.Set(j, arr.At(mid + 1 + j));

//         int iLeft = 0, iRight = 0, k = left;

//         while (iLeft < leftSize && iRight < rightSize)
//         {
//             if (key(leftArr.At(iLeft)).CompareTo(key(rightArr.At(iRight))) <= 0)
//             {
//                 arr.Set(k++, leftArr.At(iLeft++));
//             }
//             else
//             {
//                 arr.Set(k++, rightArr.At(iRight++));
//             }
//         }

//         while (iLeft < leftSize) arr.Set(k++, leftArr.At(iLeft++));
//         while (iRight < rightSize) arr.Set(k++, rightArr.At(iRight++));
//     }
// }
