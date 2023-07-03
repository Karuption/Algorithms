using System.Buffers;

namespace Algorithms.Sorting.Comparison; 

public class MergeSort {
    public static T[] Sort<T>(ReadOnlySpan<T> arr) where T : IComparable<T> {
        if (arr.Length < 2)
            return arr.ToArray();
        
        //split
        var mid = arr.Length / 2;
        var left = Sort<T>(arr[0..mid]);
        var right = Sort<T>(arr[mid..]);
        
        //sort
        var r = 0;
        var l = 0;
        var output = new T[arr.Length];
        var index = 0;
        while (l < left.Length&&r<right.Length) {
            if (left[l].CompareTo(right[r]) > 0) {//left > right {
                output[index++] = right[r++];
            } else {
                output[index++] = left[l++];
            }
        }

        while (l < left.Length)
            output[index++] = left[l++];

        while (r < right.Length)
            output[index++] = right[r++];

        return output;
    }
}