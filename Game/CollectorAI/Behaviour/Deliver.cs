namespace Game.CollectorAI.Behaviour;

using System;
using BehaviourTree.Node;
using Constants;
using Enum;
using Features.EventBus;

public partial class Deliver(ResourceType resourceType) : Node
{
    private readonly ResourceType resourceType = resourceType;

    public override NodeState Evaluate(double delta)
    {
        int resourceAmount = (int)(
            this.Root.GetData(Constants.CurrentResourceAmount)
            ?? throw new NullReferenceException($"{Constants.CurrentResourceAmount} is null")
        );

        EventBus.Instance.EmitSignal(
            EventBus.SignalName.ResourceCollected,
            resourceAmount,
            (int)this.resourceType
        );

        this.Root.SetData(Constants.CurrentResourceAmount, 0);

        this.Root.RemoveData(Constants.Target);
        this.Root.RemoveData(Constants.TargetCell);
        this.Root.RemoveData(Constants.TargetIsResource);

        this.State = NodeState.Success;

        return this.State;
    }
}
