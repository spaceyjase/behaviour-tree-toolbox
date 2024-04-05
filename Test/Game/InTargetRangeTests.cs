namespace BehaviourTree.Tests.Game;

using Chickensoft.GoDotTest;
using FluentAssertions;
using global::Game.CollectorAI.Behaviour;
using global::Game.Constants;
using Godot;
using Node;
using Node = Godot.Node;

public partial class InTargetRangeTests(Node testScene) : TestClass(testScene)
{
    private partial class TestCollector : Node2D { }

    [Test]
    public void CanCreateNode()
    {
        TestCollector collector = new();
        InTargetRange node = new(collector);
        node.Should().NotBeNull();
    }

    [Test]
    public void InTargetRange_Evaluate_NoTarget_Failure()
    {
        TestCollector collector = new();
        InTargetRange node = new(collector);
        node.Evaluate(0).Should().Be(NodeState.Failure);
    }

    [Test]
    public void InTargetRange_Evaluate_HasTarget_Success()
    {
        TestCollector collector = new();
        InTargetRange node = new(collector);
        node.SetData(Constants.Target, Vector2.One);

        node.Evaluate(0).Should().Be(NodeState.Success);
    }

    [Test]
    public void InTargetRange_Evaluate_HasTarget_OutOfRange_Failure()
    {
        TestCollector collector = new();
        collector.GlobalPosition = new(100f, 100f);
        InTargetRange node = new(collector);
        node.SetData(Constants.Target, Vector2.One);

        node.Evaluate(0).Should().Be(NodeState.Failure);
    }
}
