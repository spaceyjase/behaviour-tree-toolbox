namespace BehaviourTree.FlowControl.Parallel;

using System.Collections.Generic;
using System.Linq;
using Node;

public class Parallel : Node
{
    public Parallel() { }

    public Parallel(IEnumerable<Node> children)
        : base(children) { }

    public override NodeState Evaluate()
    {
        bool anyChildRunning = false;
        int failedChildren = 0;
        foreach (Node child in this.Children)
        {
            switch (child.Evaluate())
            {
                case NodeState.Failure:
                    failedChildren++;
                    continue;
                case NodeState.Success:
                    continue;
                case NodeState.Running:
                    anyChildRunning = true;
                    continue;
                default:
                    this.state = NodeState.Success;
                    return this.state;
            }
        }

        this.state =
            failedChildren == this.Children.Count()
                ? NodeState.Failure
                : anyChildRunning
                    ? NodeState.Running
                    : NodeState.Success;

        return this.state;
    }
}
