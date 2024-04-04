namespace Game;

using System;
using BehaviourTree.BTree;
using BehaviourTree.Decorators;
using BehaviourTree.FlowControl.Selector;
using BehaviourTree.FlowControl.Sequence;
using CollectorAI.Behaviour;
using Godot;
using Node = BehaviourTree.Node.Node;
using Timer = BehaviourTree.Decorators.Timer;

public partial class Collector : BTree
{
    public enum ResourceType
    {
        None,
        Minerals,
        Wood,
    }

    [Export]
    private ResourceType resourceType;

    [Export]
    private TileMap? tilemap;

    [Export]
    private NavigationAgent2D? agent;

    [Export]
    private Sprite2D? sprite;

    [Export]
    private ProgressBar? resourceFillBar;

    [Export]
    private float collectRate = 0.5f;

    [Export]
    private float speed = 3f;

    [Export]
    private int maxStorage = 20;

    public ResourceType Resource => this.resourceType;

    protected override Node SetupTree()
    {
        Node root = new Selector();
        // TODO: this could use a fluent API/builder pattern (see Game AI Pro for reference)
        root.SetChildren(
            [
                new Sequence(
                [
                    new CheckHasTarget(),
                    new Selector([
                        new Sequence(
                            [
                                new InTargetRange(this),
                                new TargetIsResource(),
                                new Timer(this.collectRate, [
                                    new Collect(this.maxStorage, this.tilemap),
                                ], this.CollectTimerElapsed)
                        ]),
                        new Walk(this, this.agent, this.speed, this.OnReachTarget),
                    ])
                ]),
                new FindClosestTarget(this, this.tilemap, true)
            ],
            setRoot: true
        );

        root.SetData("current_resource_amount", 0);

        if (this.resourceFillBar is null) return root;

        this.resourceFillBar.MaxValue = this.maxStorage;
        this.resourceFillBar.Value = 0;

        return root;
    }

    private void CollectTimerElapsed()
    {
        int currentAmount = (int)(this.Root?.GetData("current_resource_amount") ?? 0);
        if (this.resourceFillBar is not null)
        {
            this.resourceFillBar.Value = currentAmount;
        }
    }

    private void OnReachTarget(Vector2 velocity)
    {
        if (velocity == Vector2.Zero)
            return;
        if (this.sprite is not null)
            this.sprite.FlipH = velocity.X < 0;
    }
}
