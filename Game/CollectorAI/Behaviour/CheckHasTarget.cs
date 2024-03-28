namespace Game.CollectorAI.Behaviour;

using BehaviourTree.Node;

public class CheckHasTarget : Node
{
    public override NodeState Evaluate(double delta)
    {
        this.State = this.Root?.GetData("target") is null ? NodeState.Failure : NodeState.Success;

        return this.State;
    }
}
