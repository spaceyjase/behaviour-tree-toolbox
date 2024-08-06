namespace BehaviourTree.Tests.Game;

using global::Game;
using Chickensoft.GoDotTest;
using FluentAssertions;
using global::Game.CollectorAI.Behaviour;
using Node;
using Node = Godot.Node;

public class EnterBuildingTests(Node testScene) : TestClass(testScene)
{
    [Test]
    public void Evaluate_Visible_Success()
    {
        Collector collector = new();
        collector.Visible = true;

        EnterBuilding enterBuilding = new(collector);
        NodeState result = enterBuilding.Evaluate(0);

        result.Should().Be(NodeState.Success);
        collector.Visible.Should().BeFalse();
    }

    [Test]
    public void Evaluate_NotVisible_Success()
    {
        Collector collector = new();
        collector.Visible = false;

        EnterBuilding enterBuilding = new(collector);
        NodeState result = enterBuilding.Evaluate(0);

        result.Should().Be(NodeState.Success);
        collector.Visible.Should().BeFalse();
    }
}
