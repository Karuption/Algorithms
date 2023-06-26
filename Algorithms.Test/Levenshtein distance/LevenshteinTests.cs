using Algorithms.Levenshtein_distance;
namespace Algorithms.Test.Levenshtein_distance; 

public class LevenshteinTests {
    [Theory]
    [InlineData("Saturday", "Sunday", 3)]
    [InlineData("Kitten", "Sitting", 3)]
    [InlineData("", "", 0)]
    [InlineData("Kitten", "Kitten", 0)]
    // Testing deletion
    [InlineData("aaaa", "aaa", .5, 1, .5, 1)]
    [InlineData("a", "", .5, 1, .5, 1)]
    // Testing insertion
    [InlineData("", "a", .5, 1, 1, .5)]
    [InlineData("aaa", "aaaa", .5, 1, 1, .5)]
    // Testing substitution
    [InlineData("b", "a", .5, .5, 1, 1)]
    [InlineData("baaa", "aaaa", .5, .5, 1, 1)]
    public void Test_String_Should_Return_True(string from, string to, decimal expected, decimal substitutionCost = 1, decimal deletionCost = 1, decimal insertionCost = 1) {

        var actual = Levenshtein.Distance(from, to, substitutionCost, deletionCost, insertionCost);
        
        Assert.Equal(expected, actual);
    }
}