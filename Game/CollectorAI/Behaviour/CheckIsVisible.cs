using BehaviourTree.Node;

namespace Game.CollectorAI.Behaviour;

public class CheckIsVisible(Collector collector) : Node
{
    private readonly Collector collector = collector;

    public override NodeState Evaluate(double delta)
    {
        this.State = this.collector.Visible ? NodeState.Success : NodeState.Failure;

        return this.State;
    }
}
