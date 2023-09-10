using System.Reflection.Metadata.Ecma335;
using Algorithms.Sorting.Comparison;

namespace Algorithms.Test.Sorting.Comparison; 

public class Comparison_Sort_Tests {
    [Theory]
    [MemberData(nameof(sortedDataReversed))]
    [MemberData(nameof(sortedDataShuffled))]
    public void MergeSortTest(int[] list, int[] expected) {
        var actual = MergeSort.Sort<int>(list.AsSpan());

        if(!expected.Any())
            Assert.True(actual.Length == 0);
        
        Assert.Equal(expected, actual.ToArray());
    }

    private static readonly int[] data = Enumerable.Range(0, 1_000_000).ToArray();
    
    [Fact]
    public void QuickSortTestData() {
        var expected = data.ToArray();
        var actual = MergeSort.Sort<int>(data.AsSpan());

        if(!expected.Any())
            Assert.True(actual.Length == 0);
        
        Assert.Equal(expected, actual.ToArray());
    }
    
    [Theory]
    [MemberData(nameof(sortedDataReversed))]
    [MemberData(nameof(sortedDataShuffled))]
    public void QuickSortTest(int[] list, int[] expected) {
        var actual = MergeSort.Sort<int>(list.AsSpan());

        if(!expected.Any())
            Assert.True(actual.Length == 0);
        
        Assert.Equal(expected, actual.ToArray());
    }

    public static List<object[]> sortedDataReversed() {
        var output = new List<object[]>();
        
        for (int i = 0; i < 10; i++) {
            var sorted = Enumerable.Range(0, i*10000).ToArray();
            output.Add(new[] {sorted.Reverse().ToArray(),sorted});
        }

        return output;
    }
    
    public static List<object[]> sortedDataShuffled() {
        var output = new List<object[]>();
        var rand = new Random(456);
        
        for (int i = 0; i < 10; i++) {
            var sorted = Enumerable.Range(0, i*10000).ToArray();
            var shuffle = sorted.ToArray();
            for (int j = shuffle.Length - 1; j >= 0; j--) {
                var r = rand.Next(j);
                (shuffle[r], shuffle[j]) = (shuffle[j],shuffle[r]);
            }
            output.Add(new[] {shuffle,sorted});
        }
        
        output.Add(new[]{new[]{1,3,2,6,4,5}, new[]{1,2,3,4,5,6}});
        output.Add(new[]{new[]{6}, new[]{6}});
        output.Add(new[]{new[]{0,1}, new[]{0,1}});
        output.Add(new[]{new[]{1, 0}, new[]{0,1}});
        output.Add(new[]{new[]{1, 0, 2}, new[]{0,1,2}});
        output.Add(new[]{new[]{1, 0, 3, 2}, new[]{0,1,2,3}});
        output.Add(new[]{Array.Empty<int>(), Array.Empty<int>()});

        return output;
    }
}