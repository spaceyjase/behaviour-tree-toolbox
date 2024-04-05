namespace BehaviourTree.Tests.BehaviourTree;

using Chickensoft.GoDotTest;
using FluentAssertions;
using Node;
using Node = Godot.Node;

public class NodeDataTests(Node testScene) : TestClass(testScene)
{
    [Test]
    public void CanCreateNodeData()
    {
        NodeData nodeData = new();
        nodeData.Should().NotBeNull();
    }

    [Test]
    public void CanSetValue()
    {
        NodeData nodeData = new();
        nodeData.SetValue("key", "value");
        nodeData.TryGetValue("key", out object? value).Should().BeTrue();
        value.Should().Be("value");
    }

    [Test]
    public void TryGetData_IsFalse()
    {
        NodeData nodeData = new();
        nodeData.TryGetValue("key", out object? _).Should().BeFalse();
    }

    [Test]
    public void CanRemoveData()
    {
        NodeData nodeData = new();
        nodeData.SetValue("key", "value");
        nodeData.RemoveValue("key").Should().BeTrue();
    }
}
