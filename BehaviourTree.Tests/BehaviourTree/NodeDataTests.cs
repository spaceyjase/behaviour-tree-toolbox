namespace BehaviourTree.Tests.BehaviourTree;

using FluentAssertions;
using Node;

public class NodeDataTests
{
    [Fact]
    public void CanCreateNodeData()
    {
        NodeData nodeData = new();
        nodeData.Should().NotBeNull();
    }

    [Fact]
    public void CanSetValue()
    {
        NodeData nodeData = new();
        nodeData.SetValue("key", "value");
        nodeData.TryGetValue("key", out object? value).Should().BeTrue();
        value.Should().Be("value");
    }

    [Fact]
    public void TryGetData_IsFalse()
    {
        NodeData nodeData = new();
        nodeData.TryGetValue("key", out object? _).Should().BeFalse();
    }

    [Fact]
    public void CanRemoveData()
    {
        NodeData nodeData = new();
        nodeData.SetValue("key", "value");
        nodeData.RemoveValue("key").Should().BeTrue();
    }
}
