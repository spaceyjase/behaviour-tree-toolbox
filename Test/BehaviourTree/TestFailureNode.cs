namespace BehaviourTree.Tests.BehaviourTree;

using Node;

public class TestFailureNode : Node
{
    public override NodeState Evaluate(double delta)
    {
        return NodeState.Failure;
    }
}
