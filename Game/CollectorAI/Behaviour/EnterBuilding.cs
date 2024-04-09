namespace Game.CollectorAI.Behaviour;

using BehaviourTree.Node;

public class EnterBuilding(Collector collector) : Node
{
    private readonly Collector collector = collector;

    public override NodeState Evaluate(double delta)
    {
        this.collector.Visible = false;

        this.State = NodeState.Success;

        return this.State;
    }
}
