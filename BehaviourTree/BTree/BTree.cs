namespace BehaviourTree.BTree;

using Node;

public class BTree(INode tree)
{
    public void Evaluate(double delta) => this.Root?.Evaluate(delta);

    private INode? Root { get; } = tree;
}
