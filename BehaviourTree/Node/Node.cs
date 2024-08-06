namespace BehaviourTree.Node;

using System;
using System.Collections.Generic;

public abstract class Node : INode
{
    private readonly NodeData nodeData = new();

    private readonly List<INode> children;
    private INode root;

    protected Node()
    {
        this.Id = new Guid().ToString();
        this.Parent = null;
        this.children = [];
        this.root = this;
    }

    protected internal string Id { get; }

    protected Node(IEnumerable<INode> children)
        : this()
    {
        this.SetChildren(children);
    }

    public INode Root
    {
        get => this.root;
        set
        {
            this.root = value;
            foreach (INode child in this.children)
            {
                child.Root = this.root;
            }
        }
    }

    protected internal NodeState State { get; set; } = NodeState.Default;

    public INode? Parent { get; set; }

    protected IEnumerable<INode> Children => this.children;
    protected internal bool HasChildren => this.children.Count > 0;

    public abstract NodeState Evaluate(double delta);

    public void SetChildren(IEnumerable<INode> newChildren, bool setRoot = false)
    {
        foreach (INode child in newChildren)
        {
            this.Attach(child);
        }

        if (setRoot)
        {
            this.Root = this;
        }
    }

    internal void Attach(INode child)
    {
        this.children.Add(child);

        child.Root = this.Root;
        child.Parent = this;
    }

    public void Detach(INode child)
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
