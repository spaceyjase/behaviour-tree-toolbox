namespace Game.CollectorAI.Behaviour;

using BehaviourTree.Node;
using Constants;

public class TargetIsResource : Node
{
    public override NodeState Evaluate(double delta)
    {
        this.State = this.Root.GetData(Constants.TargetIsResource) is true
            ? NodeState.Success
            : NodeState.Failure;
        return this.State;
    }
}
