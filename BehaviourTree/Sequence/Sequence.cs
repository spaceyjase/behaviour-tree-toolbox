using BehaviourTree.Node;

namespace BehaviourTree.Sequence;

using System.Collections.Generic;

public class Sequence : Node.Node
{
    public Sequence() { }

    public Sequence(IEnumerable<Node.Node> children)
        : base(children) { }

    public override NodeState Evaluate()
    {
        bool anyChildRunning = false;
        foreach (Node.Node child in this.Children)
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
