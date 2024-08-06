namespace BehaviourTree.Tests.BehaviourTree;

using Node;

public class TestDefaultNode : Node
{
    public override NodeState Evaluate(double delta)
    {
        return NodeState.Default;
    }
}
