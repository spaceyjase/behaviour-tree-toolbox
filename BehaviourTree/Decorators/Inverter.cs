namespace BehaviourTree.Decorators;

using System.Collections.Generic;
using System.Linq;
using Node;

/// <summary>
/// The inverter node is used to switch the result of its sub-branch and return the reversed state (i.e. "NOT").
/// </summary>
public class Inverter : Node
{
    public Inverter() { }

    public Inverter(IEnumerable<Node> children)
        : base(children) { }

    public override NodeState Evaluate(double delta)
    {
        if (!this.HasChildren)
            return NodeState.Failure;
        switch (this.Children.First().Evaluate(delta))
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
            default:
                this.State = NodeState.Failure;
                return this.State;
        }
    }
}
