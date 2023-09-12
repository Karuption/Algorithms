using Algorithms.Data_Structures.Trees;
namespace Algorithms.Test.Data_Structures.Trees; 

public class AVLTreeTests {

    [Theory]
    // Left rotation
    [InlineData(new[]{1,2,3}, new[]{1,2,3})]
    // Left Right Rotation
    [InlineData(new[]{7,6,5,4,3,2,1}, new[]{1,2,3,4,5,6,7})]

    // right rotation
    [InlineData(new[]{3,2,1}, new[]{1,2,3})]
    // RightLeft Rotation
    [InlineData(new[]{1,2,3,4,5,6,7}, new[]{1,2,3,4,5,6,7})]
    
    [MemberData(nameof(ShuffledAndSortedIntArrays))]
    public void SmallInPlaceTraversal(int[] input, int[] expected) {
        var sut = new AVLTree();

        foreach (var i in input) 
            sut.Add(i);
        
        var actual = sut.InPlaceTraversal();
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void LargeIncreasingArray() {
        var sut = new AVLTree();

        foreach (var i in Enumerable.Range(0,10000).ToArray()) 
            sut.Add(i);
        
        var actual = sut.InPlaceTraversal();
        Assert.Equal(Enumerable.Range(0,10000).ToArray(), actual);
    }
    
    [Fact]
    public void DecreasingArray() {
        var sut = new AVLTree();

        foreach (var i in Enumerable.Range(0,6).Reverse().ToArray()) 
            sut.Add(i);
        
        var actual = sut.InPlaceTraversal();
        Assert.Equal(Enumerable.Range(0,6).ToArray(), actual);
    }

    // [Theory]
    // [InlineData(new[]{4,2,1,3,6,5,7}, 6, new[]{1,2,3,4,5,7})]
    // [InlineData(new[]{4,2,1,3,6,5,7}, 2, new[]{1,3,4,5,6,7})]
    public void DeletionTest(int[] seed, int remove, int[] expected) {
        var sut = new AVLTree();

        foreach (var i in seed)
            sut.Add(i);

        Assert.True(sut.Remove(remove));

        var actual = sut.InPlaceTraversal();
        
        Assert.Equal(expected.ToList(), actual);
    }
    
    public static List<object[]> ShuffledAndSortedIntArrays() {
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