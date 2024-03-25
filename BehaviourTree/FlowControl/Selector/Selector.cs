namespace BehaviourTree.FlowControl.Selector;

using System.Collections.Generic;
using BehaviourTree.Node;

public class Selector : Node
{
    public Selector() { }

    public Selector(IEnumerable<Node> children)
        : base(children) { }

    public override NodeState Evaluate()
    {
        foreach (Node node in this.Children)
        {
            switch (node.Evaluate())
            {
                case NodeState.Failure:
                    continue;
                case NodeState.Success:
                    this.state = NodeState.Success;
                    return this.state;
                case NodeState.Running:
                    this.state = NodeState.Running;
                    return this.state;
                default:
                    continue;
            }
        }
        this.state = NodeState.Failure;
        return this.state;
    }
}
