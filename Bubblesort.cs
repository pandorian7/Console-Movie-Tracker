namespace DSA;

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
