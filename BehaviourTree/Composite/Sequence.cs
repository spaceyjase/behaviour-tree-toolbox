namespace BehaviourTree.Sequence;

using System.Collections.Generic;
using Node;

/// <summary>
/// The sequence node evaluates children from left-to-right until either one fails, or the end of the list is reached (i.e. "AND").
/// </summary>
public class Sequence : Node
{
    public Sequence() { }

    public Sequence(IEnumerable<Node> children)
        : base(children) { }

    public override NodeState Evaluate(double delta)
    {
        bool anyChildRunning = false;
        foreach (Node child in this.Children)
        {
            switch (child.Evaluate(delta))
            {
                case NodeState.Failure:
                    this.State = NodeState.Failure;
                    return this.State;
                case NodeState.Success:
                    continue;
                case NodeState.Running:
                    anyChildRunning = true;
                    break;
                default:
                    this.State = NodeState.Success;
                    return this.State;
            }
        }
        this.State = anyChildRunning ? NodeState.Running : NodeState.Success;
        return this.State;
    }
}
