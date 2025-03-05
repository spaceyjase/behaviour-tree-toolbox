namespace Game;

using BehaviourTree.Composite;
using BehaviourTree.Decorators;
using BehaviourTree.Tests.Game;
using CollectorAI.Behaviour;
using Enum;
using Godot;
using Node = BehaviourTree.Node.Node;
using Timer = BehaviourTree.Decorators.Timer;

public partial class Collector : Node2D
{
    [Export]
    private ResourceType resourceType;

    [Export]
    private ResourceMap.ResourceMap? resourceMap;

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
    private float deliverRate = 0.25f;

    [Export]
    private float speed = 3f;

    [Export]
    private int maxStorage = 20;

    [Export]
    private bool isActive = true;

    private Node? tree;

    public ResourceType Resource => this.resourceType;

    public override void _Ready()
    {
        base._Ready();

        if (!this.isActive)
            this.QueueFree();

        Node root = new Selector();
        root.SetChildren(
            [
                new Sequence(
                    [
                        new CheckReachedMaxStorage(this.maxStorage),
                        new Selector([new Inverter(new CheckHasTarget()), new TargetIsResource(),]),
                        new FindClosestTarget(this, this.tilemap, false),
                    ]
                ),
                new Sequence(
                    [
                        new CheckHasTarget(),
                        new Selector(
                            [
                                new Sequence(
                                    [
                                        new InTargetRange(this),
                                        new Selector(
                                            [
                                                new Sequence(
                                                    [
                                                        new TargetIsResource(),
                                                        new Timer(
                                                            this.collectRate,
                                                            new Collect(
                                                                this.maxStorage,
                                                                this.tilemap,
                                                                this.resourceMap
                                                                    ?? throw new System.ArgumentNullException(
                                                                        nameof(this.resourceMap)
                                                                    )
                                                            ),
                                                            this.UpdateResourceBar
                                                        ),
                                                    ]
                                                ),
                                                new Sequence(
                                                    [
                                                        new CheckHasResource(),
                                                        new Timer(
                                                            this.deliverRate,
                                                            new Deliver(this.resourceType),
                                                            this.UpdateResourceBar
                                                        ),
                                                    ]
                                                ),
                                                new Sequence(
                                                    [
                                                        new CheckIsVisible(this),
                                                        new EnterBuilding(this)
                                                    ]
                                                ),
                                            ]
                                        ),
                                    ]
                                ),
                                new Walk(this, this.agent, this.speed, this.OnReachTarget),
                            ]
                        )
                    ]
                ),
                new FindClosestTarget(this, this.tilemap, true),
                new FindClosestTarget(this, this.tilemap, false)
            ],
            setRoot: true
        );

        root.SetData(Constants.Constants.CurrentResourceAmount, 0);

        if (this.resourceFillBar is null)
            return;

        this.resourceFillBar.MaxValue = this.maxStorage;
        this.resourceFillBar.Value = 0;

        this.tree = root;
    }

    private void UpdateResourceBar()
    {
        int currentAmount = (int)(
            this.tree!.GetData(Constants.Constants.CurrentResourceAmount) ?? 0
        );
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

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        this.tree?.Evaluate(delta);
    }
}
