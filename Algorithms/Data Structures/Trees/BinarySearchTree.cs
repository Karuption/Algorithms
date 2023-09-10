namespace Algorithms.Data_Structures.Trees;

public class BinarySearchTree {
    private Node? _root;
    
    // Equals go to the right subtree
    public void Add(int data) {
        var node = new Node(data);
        Add(node);
    }

    private void Add(Node? node) {
        if(node is null)
            return;
        
        if (_root is null) {
            _root = node;
            return;
        }

        var current = _root;
        while (current is not null) {
            if (node.Data < current.Data) {
                if (current.Left is null) {
                    current.Left = node;
                    return;
                }

                current = current.Left;
            } else {
                if (current.Right is null) {
                    current.Right = node;
                    return;
                }

                current = current.Right;
            }
        }
    }

    public List<int> InPlaceTraversal() {
        if (_root is null)
            return new List<int>();

        List<int> traversal = new();
        Stack<Node> stack = new();
        stack.Push(_root);
        var current = _root;
        
        while (stack.Count > 0) {
            // push all left children
            while (current?.Left is not null) {
                stack.Push(current.Left);
                current = current?.Left;
            }
        
            //try and pop to visit the node
            if (stack.TryPop(out current)) {
                traversal.Add(current.Data);
                
                //try and go right
                if (current.Right is not null) {
                    current = current.Right;
                    stack.Push(current);
                }
            }
        }

        return traversal;
    }

    // arbitrary replace delete with left first, then right
    public bool Remove(int data) {
        var parent = ParentOf(data);
        if (parent is null)
            return false;

        if (parent.Left?.Data == data) {
            var node = parent.Left;
            parent.Left = null;
            Add(node.Left);
            Add(node.Right);
        } else {
            var node = parent.Right!;
            parent.Right = null;
            Add(node.Left);
            Add(node.Right);
        }

        return true;
    }

    private Node? ParentOf(int data) {
        if (_root is null)
            return null;

        var current = _root;
        while (current is not null) {
            if (data < current.Data) {
                if (current.Left?.Data == data)
                    return current;
                current = current.Left;
            } else {
                if (current.Right?.Data == data)
                    return current;
                current = current.Right;
            }
        }

        return null;
    }
}

class Node {
    public Node(int data) {
        Data = data;
    }
    public int Data { get; set; }
    
    public Node? Left { get; set; }
    public Node? Right { get; set; }
}