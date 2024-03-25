namespace BehaviourTree.Node;

using System.Collections.Generic;

public abstract class Node
{
    private static uint LastId;

    private readonly List<Node> children;
    private readonly uint id;
    protected NodeState state;
    private Node root;

    protected Node()
    {
        this.id = LastId++;
        this.Parent = null;
        this.children = new List<Node>();
        this.Root = this;
    }

    protected Node(IEnumerable<Node> children)
        : this()
    {
        this.SetChildren(children);
    }

    public Node Root
    {
        get => this.root;
        set
        {
            this.root = value;
            foreach (Node child in this.children)
            {
                child.Root = value;
            }
        }
    }

    public NodeState State => this.state;
    public uint Id => this.id;
    public Node Parent { get; set; }

    public IEnumerable<Node> Children => this.children;
    public bool HasChildren => this.children.Count > 0;

    public abstract NodeState Evaluate();

    public void SetChildren(IEnumerable<Node> children, bool setRoot = false)
    {
        this.children.AddRange(children);
        if (setRoot)
        {
            this.Root = this;
        }
    }

    public void Attach(Node child)
    {
        this.children.Add(child);

        child.Root = this.Root;
        child.Parent = this;
    }

    public void Detach(Node child)
    {
        this.children.Remove(child);

        child.Parent = null;
        child.Root = null;
    }
}
