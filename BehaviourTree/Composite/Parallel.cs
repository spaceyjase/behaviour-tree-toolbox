namespace BehaviourTree.Composite;

using System.Collections.Generic;
using System.Linq;
using Node;

/// <summary>
/// The parallel node can run its child nodes "at the same time". Not implemented here as it doesn't have an interruption condition.
/// The policy is:
///  - if all children fail, the parallel node fails.
///  - if any child is running, the parallel node is running.
///  - else the parallel node succeeds.
/// </summary>
public class Parallel : Node
{
    public Parallel() { }

    public Parallel(IEnumerable<INode> children)
        : base(children) { }

    public override NodeState Evaluate(double delta)
    {
        bool anyChildRunning = false;
        int failedChildren = 0;
        foreach (INode child in this.Children)
        {
            switch (child.Evaluate(delta))
            {
                case NodeState.Failure:
                    ++failedChildren;
                    continue;
                case NodeState.Success:
                    continue;
                case NodeState.Running:
                    anyChildRunning = true;
                    continue;
                default:
                    this.State = NodeState.Success;
                    return this.State;
            }
        }

        this.State =
            failedChildren == this.Children.Count()
                ? NodeState.Failure
                : anyChildRunning
                    ? NodeState.Running
                    : NodeState.Success;

        return this.State;
    }
}
