namespace Algorithms.Test.Edit_Distance; 

public class JaroTests {
    [Theory]
    [InlineData("FAREMVIEL", "FARMVILLE", .8842)]
    [InlineData("ole", "aelo", .55555)]
    [InlineData("FARMVILLE", "FARMVILLE", 1)]
    [InlineData("", "", 1)]
    [InlineData("aaa", "", 0)]
    [InlineData("", "aaa", 0)]
    public void BaseTest(string s1, string s2, double expected) {
        var actual = Algorithms.Edit_Distance.Jaro.Similarity(s1, s2);
        
        Assert.Equal(expected, actual, .0001);
    }
}