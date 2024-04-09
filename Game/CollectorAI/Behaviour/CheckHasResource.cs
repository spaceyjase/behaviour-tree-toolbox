namespace BehaviourTree.Tests.Game;

using System;
using global::Game.Constants;
using Node;

public class CheckHasResource : Node
{
    public override NodeState Evaluate(double delta)
    {
        int resourceAmount = (int)(
            this.Root.GetData(Constants.CurrentResourceAmount)
            ?? throw new NullReferenceException($"{Constants.CurrentResourceAmount} is null")
        );

        this.State = resourceAmount > 0 ? NodeState.Success : NodeState.Failure;

        return this.State;
    }
}
