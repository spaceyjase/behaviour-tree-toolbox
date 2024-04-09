namespace Game.CollectorAI.Behaviour;

using System;
using BehaviourTree.Node;
using Constants;

public class CheckReachedMaxStorage(int maxStorage) : Node
{
    public override NodeState Evaluate(double delta)
    {
        int currentAmount = (int)(
            this.Root.GetData(Constants.CurrentResourceAmount)
            ?? throw new NullReferenceException($"{Constants.CurrentResourceAmount} is null")
        );

        this.State = currentAmount == maxStorage ? NodeState.Success : NodeState.Failure;

        return this.State;
    }
}
