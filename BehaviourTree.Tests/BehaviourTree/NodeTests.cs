namespace BehaviourTree.Tests.BehaviourTree;

using FluentAssertions;
using Node;

public class NodeTests
{
    private class TestNode : Node
    {
        public TestNode()
        {

        }

        public TestNode(IEnumerable<Node> children) : base(children)
        {

        }

        public override NodeState Evaluate(double delta)
        {
            throw new NotImplementedException();
        }
    }

    // Reset the LastId after each test
    public NodeTests()
    {
        Node.LastId = 0;
    }

    [Fact]
    public void CanCreateNode()
    {
        TestNode node = new();
        node.Should().NotBeNull();
    }

    [Fact]
    public void CanCreateNodeWithChildren()
    {
        TestNode child = new();
        TestNode child2 = new();
        TestNode node = new([child, child2]);

        node.HasChildren.Should().BeTrue();
    }

    [Fact]
    public void NodeId_IsExpected()
    {
        TestNode node = new();
        node.Id.Should().Be(0);

        TestNode node2 = new();
        node2.Id.Should().Be(1);
    }

    [Fact]
    public void CanSetChildren()
    {
        TestNode node = new();
        TestNode child = new();
        node.Attach(child);
        node.HasChildren.Should().BeTrue();
    }

    [Fact]
    public void CanDetachChild()
    {
        TestNode node = new();
        TestNode child = new();
        node.Attach(child);
        node.Detach(child);
        node.HasChildren.Should().BeFalse();
    }

    [Fact]
    public void CanSetRoot()
    {
        TestNode node = new();
        TestNode child = new();
        node.Attach(child);
        child.Root.Should().Be(node);
    }

    [Fact]
    public void CanSetChildrenWithRoot()
    {
        TestNode node = new();
        TestNode child = new();
        node.SetChildren([child], true);
        child.Root.Should().Be(node);
    }

    [Fact]
    public void CanSetParent()
    {
        TestNode node = new();
        TestNode child = new();
        node.Attach(child);
        child.Parent.Should().Be(node);
    }

    [Fact]
    public void CanGetData()
    {
        TestNode node = new();
        node.SetData("key", "value");
        node.GetData("key").Should().Be("value");
    }

    [Fact]
    public void CanGetDataFromParent()
    {
        TestNode node = new();
        TestNode child = new();
        node.Attach(child);
        node.SetData("key", "value");
        child.GetData("key").Should().Be("value");
    }

    [Fact]
    public void CanRemoveData()
    {
        TestNode node = new();
        node.SetData("key", "value");
        node.RemoveData("key").Should().BeTrue();
        node.GetData("key").Should().BeNull();
    }

    [Fact]
    public void CanRemoveDataFromParent()
    {
        TestNode node = new();
        TestNode child = new();
        node.Attach(child);
        node.SetData("key", "value");
        child.RemoveData("key").Should().BeTrue();
        child.GetData("key").Should().BeNull();
    }

    [Fact]
    public void TryGetData_IsFalse()
    {
        TestNode node = new();
        node.GetData("key").Should().BeNull();
    }

    [Fact]
    public void NodeState_IsDefaultState()
    {
        TestNode node = new();
        node.State.Should().Be(NodeState.Running);
    }

    [Fact]
    public void Evaluate_ThrowsNotImplementedException()
    {
        TestNode node = new();
        Action act = () => node.Evaluate(0f);
        act.Should().Throw<NotImplementedException>();
    }
}
