using System.Runtime.InteropServices.JavaScript;
using Algorithms.Edit_Distance;

namespace Algorithms.Test.Edit_Distance;
public class LevenshteinTests {
    [Theory]
    [MemberData(nameof(BaseData))]
    public void IterativeNaiveTest_String_Should_Return_True(string from, string to, decimal expected, decimal substitutionCost = 1, decimal deletionCost = 1, decimal insertionCost = 1) {
        var actual = Levenshtein.DistanceIterativeNaive(from.AsSpan(), to.AsSpan(), substitutionCost, deletionCost, insertionCost);
        Assert.Equal(expected, actual);
    }
    
    [Theory]
    [MemberData(nameof(BaseData))]
    public void IterativeTest_String_Should_Return_True(string from, string to, decimal expected, decimal substitutionCost = 1, decimal deletionCost = 1, decimal insertionCost = 1) {
        var actual = Levenshtein.Distance(from.AsSpan(), to.AsSpan(), substitutionCost, deletionCost, insertionCost);
        Assert.Equal(expected, actual);
    }
    
    [Theory]
    [MemberData(nameof(BaseData))]
    public void RecursiveTest_String_Should_Return_True(string from, string to, decimal expected, decimal substitutionCost = 1, decimal deletionCost = 1, decimal insertionCost = 1) {
        var sut = new Levenshtein();
        var actual = sut.DistanceRecursive(from.AsSpan(), to.AsSpan(), substitutionCost, deletionCost, insertionCost);
        Assert.Equal(expected, actual);
    }

    public static object[][] BaseData => new[] {
        new object[] { "Saturday", "Sunday", 3 },
        new object[] { "Kitten", "Sitting", 3 },
        new object[] { "", "", 0 },
        new object[] { "Kitten", "Kitten", 0 },
        // Testing deletion
        new object[] { "aaaa", "aaa", .5, 1, .5, 1 },
        new object[] { "a", "", .5, 1, .5, 1 },
        // Testing insertion
        new object[] { "", "a", .5, 1, 1, .5 },
        new object[] { "aaa", "aaaa", .5, 1, 1, .5 },
        // Testing substitution
        new object[] { "b", "a", .5, .5, 1, 1 },
        new object[] { "baaa", "aaaa", .5, .5, 1, 1 }
    };
}