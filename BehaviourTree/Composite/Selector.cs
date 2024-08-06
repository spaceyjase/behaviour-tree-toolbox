namespace BehaviourTree.Composite;

using System.Collections.Generic;
using Node;

/// <summary>
/// The selector node will return success as soon as one of its children returns success (i.e. "OR").
/// </summary>
public class Selector : Node
{
    public Selector() { }

    public Selector(IEnumerable<INode> children)
        : base(children) { }

    public override NodeState Evaluate(double delta)
    {
        foreach (INode node in this.Children)
        {
            switch (node.Evaluate(delta))
            {
                case NodeState.Failure:
                    continue;
                case NodeState.Success:
                    this.State = NodeState.Success;
                    return this.State;
                case NodeState.Running:
                    this.State = NodeState.Running;
                    return this.State;
                case NodeState.Default:
                default:
                    continue;
            }
        }
        this.State = NodeState.Failure;
        return this.State;
    }
}
