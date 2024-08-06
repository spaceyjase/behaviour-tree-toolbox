namespace BehaviourTree.Tests.BehaviourTree;

using System;
using System.Collections.Generic;
using Chickensoft.GoDotTest;
using FluentAssertions;
using Node;

public class NodeTests(Godot.Node testScene) : TestClass(testScene)
{
    private class TestNode : Node
    {
        public TestNode() { }

        public TestNode(IEnumerable<Node> children)
            : base(children) { }

        public override NodeState Evaluate(double delta)
        {
            throw new NotImplementedException();
        }
    }

    [Test]
    public void CanCreateNode()
    {
        TestNode node = new();
        node.Should().NotBeNull();
    }

    [Test]
    public void CanCreateNodeWithChildren()
    {
        TestNode child = new();
        TestNode child2 = new();
        TestNode node = new([child, child2]);

        node.HasChildren.Should().BeTrue();
    }

    [Test]
    public void NodeId_HasId()
    {
        TestNode node = new();
        node.Id.Should().NotBeEmpty();
    }

    [Test]
    public void CanSetChildren()
    {
        TestNode node = new();
        TestNode child = new();
        node.Attach(child);
        node.HasChildren.Should().BeTrue();
    }

    [Test]
    public void CanSetChildren_Ctor()
    {
        TestNode child = new();
        TestNode node = new([child]);
        node.HasChildren.Should().BeTrue();
        child.Parent.Should().Be(node);
    }

    [Test]
    public void ChildCanGetParentData()
    {
        TestNode node = new();
        TestNode child = new();
        node.SetData("key", "value");
        node.Attach(child);
        child.GetData("key").Should().Be("value");
    }

    [Test]
    public void CanDetachChild()
    {
        TestNode node = new();
        TestNode child = new();
        node.Attach(child);
        node.Detach(child);
        node.HasChildren.Should().BeFalse();
    }

    [Test]
    public void CanSetRoot()
    {
        TestNode node = new();
        TestNode child = new();
        node.Attach(child);
        child.Root.Should().Be(node);
    }

    [Test]
    public void CanSetChildrenWithRoot()
    {
        TestNode node = new();
        TestNode child = new();
        node.SetChildren([child], true);
        child.Root.Should().Be(node);
    }

    [Test]
    public void CanSetParent()
    {
        TestNode node = new();
        TestNode child = new();
        node.Attach(child);
        child.Parent.Should().Be(node);
    }

    [Test]
    public void CanGetData()
    {
        TestNode node = new();
        node.SetData("key", "value");
        node.GetData("key").Should().Be("value");
    }

    [Test]
    public void CanGetDataFromParent()
    {
        TestNode node = new();
        TestNode child = new();
        node.Attach(child);
        node.SetData("key", "value");
        child.GetData("key").Should().Be("value");
    }

    [Test]
    public void CanRemoveData()
    {
        TestNode node = new();
        node.SetData("key", "value");
        node.RemoveData("key").Should().BeTrue();
        node.GetData("key").Should().BeNull();
    }

    [Test]
    public void CanRemoveDataFromParent()
    {
        TestNode node = new();
        TestNode child = new();
        node.Attach(child);
        node.SetData("key", "value");
        child.RemoveData("key").Should().BeTrue();
        child.GetData("key").Should().BeNull();
    }

    [Test]
    public void TryGetData_IsNull()
    {
        TestNode node = new();
        node.GetData("key").Should().BeNull();
    }

    [Test]
    public void NodeState_IsDefaultState()
    {
        TestNode node = new();
        node.State.Should().Be(NodeState.Default);
    }

    [Test]
    public void Evaluate_ThrowsNotImplementedException()
    {
        TestNode node = new();
        Action act = () => node.Evaluate(0f);
        act.Should().Throw<NotImplementedException>();
    }
}
