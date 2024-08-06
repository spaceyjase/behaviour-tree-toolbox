using Features.EventBus;
using Game.Enum;
using Godot;

public partial class GameManager : Node2D
{
    public override void _Ready()
    {
        base._Ready();

        EventBus.Instance.ResourceCollected += OnResourceCollected;
    }

    private static void OnResourceCollected(int resourceAmount, int resourceType)
    {
        GD.Print($"Resource collected: {resourceAmount} of type {(ResourceType)resourceType}");
    }
}
