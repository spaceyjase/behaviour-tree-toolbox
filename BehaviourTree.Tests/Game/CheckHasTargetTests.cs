namespace BehaviourTree.Tests.Game;

using FlowControl.Selector;
using FluentAssertions;
using global::Game.CollectorAI.Behaviour;
using Godot;
using Node;
using global::Game.Constants;
using Node = global::BehaviourTree.Node.Node;

public class CheckHasTargetTests
{
    [Fact]
    public void CanCreateNode()
    {
        CheckHasTarget node = new();
        node.Should().NotBeNull();
    }

    [Fact]
    public void CheckHasTarget_Evaluate_Success()
    {
        Selector root = new();
        root.SetChildren(new List<Node> { new CheckHasTarget() });
        root.SetData(Constants.Target, Vector2.Zero);
        root.Evaluate(0).Should().Be(NodeState.Success);
    }

    [Fact]
    public void CheckHasTarget_Evaluate_NoTarget_Failure()
    {
        Selector root = new();
        root.SetChildren(new List<Node> { new CheckHasTarget() });
        root.Evaluate(0).Should().Be(NodeState.Failure);
    }
}
