namespace Game.CollectorAI.Behaviour;

using BehaviourTree.Node;
using Godot;
using Constants;
using Node = BehaviourTree.Node.Node;

public class InTargetRange(Node2D collector) : Node
{
    private const float ReachThreshold = 5f;

    public override NodeState Evaluate(double delta)
    {
        Vector2? t = (Vector2?)this.Root.GetData(Constants.Target);
        if (t is null)
        {
            this.State = NodeState.Failure;
            return this.State;
        }
        float distance = collector.GlobalPosition.DistanceTo(t.Value);
        this.State =
            distance < ReachThreshold
                ? this.State = NodeState.Success
                : this.State = NodeState.Failure;

        return this.State;
    }
}
