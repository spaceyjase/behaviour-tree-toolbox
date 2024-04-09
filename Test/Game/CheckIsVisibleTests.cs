namespace BehaviourTree.Tests.Game;

using FluentAssertions;
using Chickensoft.GoDotTest;
using global::Game;
using global::Game.CollectorAI.Behaviour;
using Node;

public class CheckIsVisibleTests(Godot.Node testScene) : TestClass(testScene)
{
    [Test]
    public void Evaluate_Visible_ReturnsSuccess()
    {
        Collector collector = new();
        collector.Visible = true;

        CheckIsVisible checkIsVisible = new(collector);
        NodeState result = checkIsVisible.Evaluate(0);

        result.Should().Be(NodeState.Success);
    }

    [Test]
    public void Evaluate_NotVisible_ReturnsFailure()
    {
        Collector collector = new();
        collector.Visible = false;

        CheckIsVisible checkIsVisible = new(collector);
        NodeState result = checkIsVisible.Evaluate(0);

        result.Should().Be(NodeState.Failure);
    }
}
