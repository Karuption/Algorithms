namespace Algorithms.Data_Structures.Trees;

public class AVLTree {
    private Node? _root;

    // Equals go to the right subtree
    public void Add(int data) {
        var node = new Node(data);
        Add(node);
    }

    private void Add(Node current, Node node) {
        if (node.Data < current.Data) {
            if (current.Left is not null) 
                Add(current.Left, node);
            else 
                current.Left = node;
        } else {
            if (current.Right is not null) 
                Add(current.Right, node);
            else 
                current.Right = node;
        }
        
        current.Left = Balance(current.Left);
        current.Right = Balance(current.Right);
        //need to check root as a special case.
    }

    private void Add(Node? node) {
        if (node is null)
            return;

        if (_root is null) {
            _root = node;
            return;
        }
        
        Add(_root, node);
        _root.CalculateBalance();
        Balance(_root);
    }
    
    private Node? Balance(Node? current) {
        if (current is null)
            return null;
        
        var isRoot = current == _root;
        
        current.CalculateBalance();

        // decide on rotations for children
        // if right heavy
        if (current.Balance < -1 ) {
            if ((current.Left?.Balance ?? 0) <= 0) {
                // right rotation
                current = RightRotation(current);
            } else {
                // left right double rotation
                current = LeftRightRotation(current);
            }
        } else if (current.Balance > 1) {
            if ((current.Right?.Balance ?? 0)>= 0) {
                // left rotation
                current = LeftRotation(current);
            } else {
                // right left double rotation
                current = RightLeftRotation(current);
            }
        }

        if (isRoot)
            return _root = current;
        return current;
    }

    private Node RightRotation(Node demoteNode) {
        var liftNode = demoteNode.Left;
        demoteNode.Left = liftNode!.Right;
        
        liftNode.Right = demoteNode;
        
        demoteNode.CalculateBalance();
        liftNode.CalculateBalance();
        return liftNode;
    }

    private Node LeftRotation(Node demoteNode) {
        var liftNode = demoteNode.Right;
        demoteNode.Right = liftNode!.Left;

        liftNode.Left = demoteNode;

        demoteNode.CalculateBalance();
        liftNode.CalculateBalance();

        return liftNode;
    }

    private Node LeftRightRotation(Node current) {
        //left and re-balance
        current.Left = LeftRotation(current.Left);
        //right
        return RightRotation(current);
    }

    private Node RightLeftRotation(Node current) {
        //right and re balance
        current.Right = RightRotation(current.Right);
        //left
        return LeftRotation(current);

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
                } else {
                    current = null;
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

    private class Node {
        public Node(int data) {
            Data = data;
        }

        public int Data { get; set; }

        public Node? Left { get; set; }
        public Node? Right { get; set; }
        
        private int RightCount { get; set; }

        private int LeftCount { get; set; }
        public int Balance = 0;
        
        public void CalculateBalance() {
            if (Left is not null)
                LeftCount = Left.LeftCount + Left.RightCount + 1;
            else
                LeftCount = 0;

            if (Right is not null)
                RightCount = Right.LeftCount + Right.RightCount + 1;
            else
                RightCount = 0;
            
            Balance = RightCount - LeftCount;
        }

    }
}