using System.Diagnostics;
using MovieTracker;

namespace DSA;

class SortingEngine<T1, T2> where T2 : IComparable<T2> {

    
    private DynamicArray<SortingAlgorithm<T1, T2>> Algorithms;
    public bool Informative { get; set; } = true;

    private Stopwatch? Watch { get; set; }
    private int ItemsSorted { get; set; }

    public SortingEngine() {
        Algorithms = new ();
        Algorithms.AddLast(new SortingAlgorithm<T1, T2>("Bubble Sort", BubbleSort.Sort));
        Algorithms.AddLast(new SortingAlgorithm<T1, T2>("Insertion Sort", InsertionSort.Sort));
        Algorithms.AddLast(new SortingAlgorithm<T1, T2>("Selection Sort", SelectionSort.Sort));

    }
    private SortingAlgorithm<T1, T2> RandomSortingAlgorithem() {
        int index = Random.Shared.Next(0, Algorithms.Count);
        return Algorithms.At(index);
    }

    public void Sort(DynamicArray<T1> arr, Func<T1, T2> key) {
        var algo = RandomSortingAlgorithem();
        ItemsSorted = arr.Count;
        Watch = new();
        Watch.Start();
        algo.Sort(arr, key);
        Watch.Stop();
    }
}