namespace BehaviourTree.Tests.Game;

using System.Collections.Generic;
using Chickensoft.GoDotTest;
using Composite;
using FluentAssertions;
using global::Game.CollectorAI.Behaviour;
using global::Game.Constants;
using Godot;
using Node;
using Node = Node.Node;

public class CheckHasTargetTests(Godot.Node testScene) : TestClass(testScene)
{
    [Test]
    public void CanCreateNode()
    {
        CheckHasTarget node = new();
        node.Should().NotBeNull();
    }

    [Test]
    public void CheckHasTarget_Evaluate_Success()
    {
        Selector root = new();
        root.SetChildren(new List<Node> { new CheckHasTarget() });
        root.SetData(Constants.Target, Vector2.Zero);
        root.Evaluate(0).Should().Be(NodeState.Success);
    }

    [Test]
    public void CheckHasTarget_Evaluate_NoTarget_Failure()
    {
        Selector root = new();
        root.SetChildren(new List<Node> { new CheckHasTarget() });
        root.Evaluate(0).Should().Be(NodeState.Failure);
    }
}
