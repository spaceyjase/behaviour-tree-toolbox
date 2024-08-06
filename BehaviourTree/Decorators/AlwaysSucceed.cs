namespace BehaviourTree.Decorators;

using System.Linq;
using Node;

public class AlwaysSucceed : Node
{
    private readonly INode child;

    public AlwaysSucceed(INode child)
        : base([child])
    {
        this.child = this.Children.First();
    }

    public override NodeState Evaluate(double delta)
    {
        this.State =
            this.child.Evaluate(delta) == NodeState.Running ? NodeState.Running : NodeState.Success;
        return this.State;
    }
}
