namespace BehaviourTree.Tests.BehaviourTree;

using Chickensoft.GoDotTest;
using Composite;
using Decorators;
using FluentAssertions;
using Node;

public class SelectorTests(Godot.Node testScene) : TestClass(testScene)
{
    private class TestSuccessNode : Node
    {
        public override NodeState Evaluate(double delta)
        {
            return NodeState.Success;
        }
    }

    private class TestFailureNode : Node
    {
        public override NodeState Evaluate(double delta)
        {
            return NodeState.Failure;
        }
    }

    [Test]
    public void CanCreateSelector()
    {
        Selector selector = new();
        selector.Should().NotBeNull();
    }

    [Test]
    public void CanCreateSelectorWithChildren()
    {
        Selector child = new();
        Selector selector = new([child]);

        selector.HasChildren.Should().BeTrue();
    }

    [Test]
    public void DefaultStateEvaluateIsFailure()
    {
        Selector selector = new([new TestDefaultNode()]);
        selector.Evaluate(1f);

        selector.State.Should().Be(NodeState.Failure);
    }

    [Test]
    public void Selector_Evaluate_Failure()
    {
        TestFailureNode child = new();
        Selector selector = new([child]);
        selector.Evaluate(1f);

        selector.State.Should().Be(NodeState.Failure);
    }

    [Test]
    public void Selector_Evaluate_Running()
    {
        TestSuccessNode timerChild = new();
        Timer child = new(1f, timerChild);
        Selector selector = new([child]);
        selector.Evaluate(1f);

        selector.State.Should().Be(NodeState.Running);
    }

    [Test]
    public void Selector_Evaluate_Success()
    {
        TestSuccessNode child = new();
        Selector selector = new([child]);
        selector.Evaluate(1f);

        selector.State.Should().Be(NodeState.Success);
    }

    [Test]
    public void Selector_Evaluate_WithChildren_Success()
    {
        TestSuccessNode child1 = new();
        TestSuccessNode child2 = new();
        Selector selector = new([child1, child2]);
        selector.Evaluate(1f);

        selector.State.Should().Be(NodeState.Success);
    }

    [Test]
    public void Selector_Evaluate_WithOneChildSuccess_Success()
    {
        TestSuccessNode child1 = new();
        TestFailureNode child2 = new();
        Selector selector = new([child1, child2]);
        selector.Evaluate(1f);

        selector.State.Should().Be(NodeState.Success);
    }

    [Test]
    public void Selector_Evaluate_WithChildFailure_Failure()
    {
        TestFailureNode child1 = new();
        TestFailureNode child2 = new();
        Selector selector = new([child1, child2]);
        selector.Evaluate(1f);

        selector.State.Should().Be(NodeState.Failure);
    }
}
