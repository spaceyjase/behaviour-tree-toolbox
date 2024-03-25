namespace BehaviourTree.BTree;

public abstract partial class BTree : Godot.Node
{
    private Node.Node root;

    public override void _Ready()
    {
        Node.Node.LastId = 0;
        this.root = this.SetupTree();
    }

    public override void _Process(double delta)
    {
        this.root?.Evaluate();
    }

    public Node.Node Root => this.root;
    protected abstract Node.Node SetupTree();
}
