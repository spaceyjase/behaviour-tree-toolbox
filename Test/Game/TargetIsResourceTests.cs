namespace BehaviourTree.Tests.Game;

using Chickensoft.GoDotTest;
using FluentAssertions;
using global::Game.CollectorAI.Behaviour;
using global::Game.Constants;
using Node;
using Node = Godot.Node;

public class TargetIsResourceTests(Node testScene) : TestClass(testScene)
{
    [Test]
    public void CanCreateNode()
    {
        TargetIsResource node = new();
        node.Should().NotBeNull();
    }

    [Test]
    public void TargetIsResource_Evaluate_NoData_Failure()
    {
        TargetIsResource node = new();
        node.Evaluate(0).Should().Be(NodeState.Failure);
    }

    [Test]
    public void TargetIsResource_Evaluate_TargetIsNotResource_Failure()
    {
        TargetIsResource node = new();
        node.SetData(Constants.TargetIsResource, false);
        node.Evaluate(0).Should().Be(NodeState.Failure);
    }

    [Test]
    public void TargetIsResource_Evaluate_TargetIsResource_Success()
    {
        TargetIsResource node = new();
        node.SetData(Constants.TargetIsResource, true);
        node.Evaluate(0).Should().Be(NodeState.Success);
    }
}
