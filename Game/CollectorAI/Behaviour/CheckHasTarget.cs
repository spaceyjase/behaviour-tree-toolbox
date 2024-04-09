namespace Game.CollectorAI.Behaviour;

using BehaviourTree.Node;
using Constants;

public class CheckHasTarget : Node
{
    public override NodeState Evaluate(double delta)
    {
        this.State = this.Root.GetData(Constants.Target) is null
            ? NodeState.Failure
            : NodeState.Success;

        return this.State;
    }
}
