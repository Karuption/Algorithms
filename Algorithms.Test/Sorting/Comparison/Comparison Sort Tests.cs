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

    public static List<object[]> sortedDataReversed() {
        var output = new List<object[]>();
        
        for (int i = 0; i < 10; i++) {
            var sorted = Enumerable.Range(0, i).ToArray();
            output.Add(new[] {sorted.Reverse().ToArray(),sorted});
        }

        return output;
    }
    
    public static List<object[]> sortedDataShuffled() {
        var output = new List<object[]>();
        var rand = new Random(456);
        
        for (int i = 0; i < 10; i++) {
            var sorted = Enumerable.Range(0, i).ToArray();
            var shuffle = sorted.ToArray();
            for (int j = shuffle.Length - 1; j >= 0; j--) {
                var r = rand.Next(j);
                (shuffle[r], shuffle[j]) = (shuffle[j],shuffle[r]);
            }
            output.Add(new[] {shuffle,sorted});
        }

        return output;
    }
}