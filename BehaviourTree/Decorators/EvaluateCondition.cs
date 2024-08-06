namespace BehaviourTree.Decorators;

using System;
using Node;

public class EvaluateCondition(Func<bool> func) : Node
{
    private readonly Func<bool> func = func;

    public override NodeState Evaluate(double delta)
    {
        this.State = this.func() ? NodeState.Success : NodeState.Failure;
        return this.State;
    }
}
