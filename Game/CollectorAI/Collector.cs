namespace Game;

using System.Collections.Generic;
using BehaviourTree.BTree;
using BehaviourTree.FlowControl.Selector;
using BehaviourTree.FlowControl.Sequence;
using CollectorAI.Behaviour;
using Godot;
using Node = BehaviourTree.Node.Node;

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
    private Sprite2D sprite;

    [Export]
    private float speed = 3f;

    public ResourceType Resource => this.resourceType;

    protected override Node SetupTree()
    {
        Node root = new Selector();
        // TODO: this could use a fluent API/builder pattern (see Game AI Pro for reference)
        root.SetChildren(
            new List<Node>
            {
                new Sequence(
                    new List<Node>
                    {
                        new CheckHasTarget(),
                        new Walk(this, this.agent, this.speed, this.OnReachTarget),
                    }
                ),
                new FindClosestTarget(this, this.tilemap, true)
            },
            setRoot: true
        );
        return root;
    }

    private void OnReachTarget(Vector2 velocity)
    {
        if (velocity == Vector2.Zero)
            return;
        this.sprite.FlipH = velocity.X < 0;
    }
}
