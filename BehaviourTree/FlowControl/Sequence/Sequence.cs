namespace BehaviourTree.FlowControl.Sequence;

using System.Collections.Generic;
using BehaviourTree.Node;

public class Sequence : Node
{
    public Sequence() { }

    public Sequence(IEnumerable<Node> children)
        : base(children) { }

    public override NodeState Evaluate()
    {
        bool anyChildRunning = false;
        foreach (Node child in this.Children)
        {
            switch (child.Evaluate())
            {
                case NodeState.Failure:
                    this.state = NodeState.Failure;
                    return this.state;
                case NodeState.Success:
                    continue;
                case NodeState.Running:
                    anyChildRunning = true;
                    break;
                default:
                    this.state = NodeState.Success;
                    return this.state;
            }
        }
        this.state = anyChildRunning ? NodeState.Running : NodeState.Success;
        return this.state;
    }
}
