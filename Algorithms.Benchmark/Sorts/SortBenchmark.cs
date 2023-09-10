using System;
using System.Linq;
using BenchmarkDotNet.Attributes;
using Algorithms.Sorting.Comparison;

namespace Algorithms.Benchmark.Sorts; 

[MemoryDiagnoser]
public class ComparisonSortBenchmarks {

    [Params(1_000, 100_000)]
    public int Size;

    private int[] _toSort;
    private int[] _toSortSorted;
    private int[] _backwards;

    [GlobalSetup]
    public void Setup() {
        _toSortSorted = Enumerable.Range(0, Size).ToArray();
        _backwards = _toSortSorted.Reverse().ToArray();
        _toSort = _toSortSorted.ToArray();

        var rng = new Random(456);
        for (int i = _toSort.Length - 1; i > 0; i--) {
            var rand = rng.Next(i);
            (_toSort[rand], _toSort[i]) = (_toSort[i], _toSort[rand]);
        }
    }
    [Benchmark]
    public Span<int> MergeSort() {
        return Sorting.Comparison.MergeSort.Sort<int>(_toSort.AsSpan()).AsSpan();
    }
    [Benchmark]
    public Span<int> QuickSort() {
        return Sorting.Comparison.QuickSort.QSort(_toSort);
    }
    [Benchmark]
    public Span<int> MergeSortSorted() {
        return Sorting.Comparison.MergeSort.Sort<int>(_toSortSorted.AsSpan()).AsSpan();
    }
    
    [Benchmark]
    public Span<int> QuickSortSorted() {
        return Sorting.Comparison.QuickSort.QSort(_toSortSorted);
    }
    
    [Benchmark]
    public Span<int> MergeSortBackwards() {
        return Sorting.Comparison.MergeSort.Sort<int>(_backwards).AsSpan();
    }
    
    [Benchmark]
    public Span<int> QuickSortBackwards() {
        return Sorting.Comparison.QuickSort.QSort(_backwards);
    }
}