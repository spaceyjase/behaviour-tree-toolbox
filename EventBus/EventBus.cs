namespace Features.EventBus;

using Godot;

public partial class EventBus : Node
{
    [Signal]
    public delegate void ResourceCollectedEventHandler(int resourceAmount, int resourceType);

    public static EventBus Instance { get; } = new();
}
