namespace Game.CollectorAI.Behaviour;

using BehaviourTree.Node;

public class TargetIsResource : Node
{
    public override NodeState Evaluate(double delta)
    {
        this.State = this.Root.GetData("target_is_resource") is true
            ? NodeState.Success
            : NodeState.Failure;
        return this.State;
    }
}
