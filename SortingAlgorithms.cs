using System;

namespace DSA;


public interface ISortingAlgorithm
{
    string Name { get; }
    void Sort<T>(DynamicArray<T> arr , Func<T, IComparable> key);
}
public class InsertionSort : ISortingAlgorithm
{
    public string Name {get;} = "Insertion Sort";
    public void Sort<T>(DynamicArray<T> arr , Func<T, IComparable> key)
    {
        for(int i=1; i<arr.Count; i++)
        {
            T element = arr.At(i);
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


public class SelectionSort: ISortingAlgorithm
{
    public string Name {get;} = "Selection Sort";
    public void Sort<T>(DynamicArray<T> arr , Func<T, IComparable> key)
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
            T temp = arr.At(i);
            arr.Set(i, arr.At(minIndex));
            arr.Set(minIndex, temp);
        }
    }
}

public class BubbleSort: ISortingAlgorithm {
    public string Name {get;} = "Bubble Sort";
    public void Sort<T>(DynamicArray<T> arr , Func<T, IComparable> key) {
        int n = arr.Count;
        bool swapped;
        for (int i = 0; i < n - 1; i++) {
            swapped = false;
            for (int j = 0; j < n - i - 1; j++) {
                if (key(arr.At(j)).CompareTo(key(arr.At(j+1))) > 0) {
                    T temp = arr.At(j);
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


class MergeSort: ISortingAlgorithm {

    public string Name {get;} = "Merge Sort";

    static void merge<T>(DynamicArray<T> arr, int l, int m, int r, Func<T, IComparable> key)
    {
        
        int n1 = m - l + 1;
        int n2 = r - m;

        var L = new DynamicArray<T>(n1);
        var R = new DynamicArray<T>(n1);
        int i, j;

        for (i = 0; i < n1; ++i)
            L[i] = arr[l + i];
        for (j = 0; j < n2; ++j)
            R[j] = arr[m + 1 + j];

        i = 0;
        j = 0;

        int k = l;
        while (i < n1 && j < n2) {
            if (key(L[i]).CompareTo(key(R[j])) <= 0) {
                arr[k] = L[i];
                i++;
            }
            else {
                arr[k] = R[j];
                j++;
            }
            k++;
        }

        while (i < n1) {
            arr[k] = L[i];
            i++;
            k++;
        }

        while (j < n2) {
            arr[k] = R[j];
            j++;
            k++;
        }
    }

    static void mergeSort<T>(DynamicArray<T> arr, int l, int r, Func<T, IComparable> key)
    {
        if (l < r) {

            int m = l + (r - l) / 2;


            mergeSort(arr, l, m, key);
            mergeSort(arr, m + 1, r, key);

            merge(arr, l, m, r, key);
        }
    }

    public void Sort<T>(DynamicArray<T> arr , Func<T, IComparable> key) {
        mergeSort(arr, 0, arr.Count - 1, key);
    }
}

class QuickSort: ISortingAlgorithm {

    public string Name {get;} = "Quick Sort";
    static int Partition<T>(DynamicArray<T> arr, int low, int high, Func<T, IComparable> key) {
        
        var pivot = arr[high];
        
        int i = low - 1;

        for (int j = low; j <= high - 1; j++) {
            if (key(arr[j]).CompareTo(key(pivot)) < 0) {
                i++;
                Swap(arr, i, j);
            }
        }
        
        Swap(arr, i + 1, high);  
        return i + 1;
    }

    static void Swap<T>(DynamicArray<T> arr, int i, int j) {
        var temp = arr[i];
        arr[i] = arr[j];
        arr[j] = temp;
    }

    static void quicksort<T>(DynamicArray<T> arr, int low, int high, Func<T, IComparable> key) {
        if (low < high) {
            
            int pi = Partition(arr, low, high, key);

            quicksort(arr, low, pi - 1, key);
            quicksort(arr, pi + 1, high, key);
        }
    }

    public void Sort<T>(DynamicArray<T> arr , Func<T, IComparable> key) { 
        quicksort(arr, 0, arr.Count - 1, key);
    }
}

