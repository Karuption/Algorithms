namespace Algorithms.Sorting.Comparison;

public static class QuickSort {
    public static Span<T> QSort<T>(T[] elements, Comparer<T>? comparer = null) where T : IComparable {
        if (elements.Length < 2)
            return elements;

        comparer ??= Comparer<T>.Default;

        if (elements.Length == 2) {
            if (comparer.Compare(elements[0], elements[1]) <= 0)
                return elements;

            elements.Reverse();
            return elements;
        }



        var pivotIndex = elements.Length / 2;
        (elements[pivotIndex], elements[^1]) = (elements[^1], elements[pivotIndex]);

        int left = 0, right = elements.Length - 2;

        while (left < right) {
            if (comparer.Compare(elements[left], elements[^1]) <= 0)
                left++;
            else if (comparer.Compare(elements[right], elements[^1]) >= 0)
                right--;
            else
                (elements[left], elements[right]) = (elements[right], elements[left]);
        }

        (elements[^1], elements[pivotIndex]) = (elements[pivotIndex], elements[^1]);

        var rightSort = elements[0..right];
        var leftSort = elements[right..];

        QSort<T>(rightSort, comparer);
        QSort<T>(leftSort, comparer);

        return elements;
    }
}