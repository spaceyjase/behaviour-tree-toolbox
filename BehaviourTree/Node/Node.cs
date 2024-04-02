namespace BehaviourTree.Node;

using System.Collections.Generic;

public abstract class Node
{
    public static uint LastId;

    private readonly NodeData nodeData = new();

    private readonly List<Node> children;
    private Node root;

    protected Node()
    {
        this.Id = LastId++;
        this.Parent = null;
        this.children = new List<Node>();
        this.root = this;
    }

    protected Node(IEnumerable<Node> children)
        : this()
    {
        this.SetChildren(children);
    }

    public Node Root
    {
        get => this.root;
        private set
        {
            this.root = value;
            foreach (Node child in this.children)
            {
                child.Root = this.root;
            }
        }
    }

    public NodeState State { get; protected set; }
    public uint Id { get; }

    public Node? Parent { get; private set; }

    protected IEnumerable<Node> Children => this.children;
    public bool HasChildren => this.children.Count > 0;

    public abstract NodeState Evaluate(double delta);

    public void SetChildren(IEnumerable<Node> children, bool setRoot = false)
    {
        foreach (Node child in children)
        {
            this.Attach(child);
        }

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
        child.Root = child;
    }

    public object? GetData(string key)
    {
        if (this.nodeData.TryGetValue(key, out object? value))
        {
            return value;
        }
        if (this.Parent is not null)
        {
            value = this.Parent.GetData(key);
        }

        return value;
    }

    public void SetData(string key, object value) => this.nodeData.SetValue(key, value);

    public bool RemoveData(string key)
    {
        if (this.nodeData.RemoveValue(key))
        {
            return true;
        }
        return this.Parent is not null && this.Parent.RemoveData(key);
    }
}
