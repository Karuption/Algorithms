using Algorithms.Data_Structures.Trees;
namespace Algorithms.Test.Data_Structures.Trees; 

public class BinarySearchTreeTests {

    [Theory]
    [InlineData(new[]{4,2,1,3,6,5,7}, new[]{1,2,3,4,5,6,7})]
    public void SmallInPlaceTraversal(int[] input, int[] expected) {
        var sut = new BinarySearchTree();

        foreach (var i in input) 
            sut.Add(i);
        
        var actual = sut.InPlaceTraversal();
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(new[]{4,2,1,3,6,5,7}, 6, new[]{1,2,3,4,5,7})]
    [InlineData(new[]{4,2,1,3,6,5,7}, 2, new[]{1,3,4,5,6,7})]
    public void DeletionTest(int[] seed, int remove, int[] expected) {
        var sut = new BinarySearchTree();

        foreach (var i in seed)
            sut.Add(i);

        Assert.True(sut.Remove(remove));

        var actual = sut.InPlaceTraversal();
        
        Assert.Equal(expected.ToList(), actual);
    }
}