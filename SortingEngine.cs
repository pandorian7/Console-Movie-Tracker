using System.Diagnostics;
using MovieTracker;

namespace DSA;

class SortingEngine {

    
    private DynamicArray<ISortingAlgorithm> Algorithms;
    public bool Informative { get; set; } = true;

    private Stopwatch? Watch { get; set; }
    private int ItemsSorted { get; set; }

    public SortingEngine() {
        Algorithms = new ();
        Algorithms.AddLast(new InsertionSort());
        Algorithms.AddLast(new SelectionSort());
        Algorithms.AddLast(new BubbleSort());

    }
    private ISortingAlgorithm RandomSortingAlgorithem() {
        int index = Random.Shared.Next(0, Algorithms.Count);
        return Algorithms.At(index);
    }

    public void Sort<T>(DynamicArray<T> arr, Func<T, IComparable> key) {
        var algo = RandomSortingAlgorithem();
        ItemsSorted = arr.Count;

        if (Informative) {
            Console.WriteLine($"Sorting {ItemsSorted} items using {algo.Name}");
        }

        Watch = new();
        Watch.Start();
        algo.Sort(arr, key);
        Watch.Stop();

        if (Informative) {
            Console.WriteLine($"Sorted {ItemsSorted} items in {Watch.ElapsedMilliseconds}ms");
        }
    }
}