namespace BehaviourTree.Decorators;

using System.Linq;
using Node;

/// <summary>
/// The inverter node is used to switch the result of its sub-branch and return the reversed state (i.e. "NOT").
/// </summary>
public class Inverter : Node
{
    private readonly INode child;

    public Inverter(INode child)
        : base([child])
    {
        this.child = this.Children.First();
    }

    public override NodeState Evaluate(double delta)
    {
        if (!this.HasChildren)
            return NodeState.Failure;
        switch (this.child.Evaluate(delta))
        {
            case NodeState.Failure:
                this.State = NodeState.Success;
                return this.State;
            case NodeState.Success:
                this.State = NodeState.Failure;
                return this.State;
            case NodeState.Running:
                this.State = NodeState.Running;
                return this.State;
            case NodeState.Default:
            default:
                this.State = NodeState.Failure;
                return this.State;
        }
    }
}
