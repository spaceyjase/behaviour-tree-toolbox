namespace Game.CollectorAI.Behaviour;

using System;
using BehaviourTree.Node;
using Godot;
using Node = BehaviourTree.Node.Node;

public class Walk : Node
{
    private readonly Node2D collector;
    private readonly NavigationAgent2D navigationAgent;
    private readonly float speed;
    private readonly Action<Vector2>? onReachTarget;

    public Walk(
        Node2D collector,
        NavigationAgent2D? navigationAgent,
        float speed,
        Action<Vector2>? onReachTarget = null
    )
    {
        ArgumentNullException.ThrowIfNull(navigationAgent);

        this.collector = collector;
        this.navigationAgent = navigationAgent;
        this.speed = speed;
        this.onReachTarget = onReachTarget;

        this.navigationAgent.VelocityComputed += this.OnVelocityComputed;
        this.navigationAgent.NavigationFinished += this.OnNavigationFinished;
    }

    private void OnNavigationFinished()
    {
        this.State = NodeState.Success;
        this.RemoveData("target");
    }

    private void OnVelocityComputed(Vector2 velocity)
    {
        this.collector.GlobalPosition += velocity;
        this.onReachTarget?.Invoke(velocity);
    }

    public override NodeState Evaluate(double delta)
    {
        Vector2? targetPosition = (Vector2?)this.GetData("target");
        if (targetPosition is null)
        {
            this.State = NodeState.Failure;
            return this.State;
        }

        if (this.navigationAgent.TargetPosition != targetPosition.Value)
        {
            this.navigationAgent.TargetPosition = targetPosition.Value;
        }

        Vector2 nextPosition = this.navigationAgent.GetNextPathPosition();
        Vector2 velocity = (nextPosition - this.collector.GlobalPosition).Normalized();
        this.navigationAgent.Velocity = velocity * this.speed;

        this.State = NodeState.Running;

        return this.State;
    }
}
