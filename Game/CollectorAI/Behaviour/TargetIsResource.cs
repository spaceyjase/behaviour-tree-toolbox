namespace Game.CollectorAI.Behaviour;

using BehaviourTree.Node;

public class TargetIsResource : Node
{
    public override NodeState Evaluate(double delta)
    {
        this.State = this.Root.GetData("targetIsResource") is true
            ? NodeState.Success
            : NodeState.Failure;
        return this.State;
    }
}
