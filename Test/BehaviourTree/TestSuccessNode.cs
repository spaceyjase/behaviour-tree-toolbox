namespace BehaviourTree.Tests.BehaviourTree;

using Node;

public class TestSuccessNode : Node
{
    public override NodeState Evaluate(double delta)
    {
        return NodeState.Success;
    }
}
