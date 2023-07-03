using System;
using System.Runtime.Versioning;
using BenchmarkDotNet.Attributes;
using Algorithms.Edit_Distance;

namespace Algorithms.Benchmark.Edit_Distance;
[MarkdownExporter]
[MemoryDiagnoser]
public class Levenshtein {
    [Params(10,100)]//,1000)]
    public int Size { get; set; }

    private byte[] length1;
    private byte[] length2;

    [GlobalSetup]
    public void Setup() {
        length1 = new byte[Size];
        length2 = new byte[Size];

        Random.Shared.NextBytes(length1);
        Random.Shared.NextBytes(length2);
    }
    
    [Benchmark(Baseline = true)]
    public void LevenshteinDistanceIterativeNaive() {
        Algorithms.Edit_Distance.Levenshtein.DistanceIterativeNaive<byte>(length1.AsSpan(), length2.AsSpan());
    }

    [Benchmark]
    public void LevenshteinDistanceIterative() {
        Algorithms.Edit_Distance.Levenshtein.Distance<byte>(length1.AsSpan(), length2.AsSpan());
    }
    
    //[Benchmark]
    public void LevenshteinDistanceRecursive() {
        var sut = new Algorithms.Edit_Distance.Levenshtein();
        sut.DistanceRecursive<byte>(length1.AsSpan(), length2.AsSpan());
    }
}