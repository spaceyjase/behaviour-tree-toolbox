namespace BehaviourTree.BTree;

public abstract partial class BTree : Godot.Node2D
{
    public override void _Ready()
    {
        this.Root = this.SetupTree();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        this.Root?.Evaluate(delta);
    }

    protected Node.Node? Root { get; private set; }

    protected abstract Node.Node SetupTree();
}
