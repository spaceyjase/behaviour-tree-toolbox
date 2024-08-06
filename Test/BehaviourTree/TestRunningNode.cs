namespace BehaviourTree.Tests.BehaviourTree;

using Node;

public class TestRunningNode : Node
{
    public override NodeState Evaluate(double delta)
    {
        return NodeState.Running;
    }
}
