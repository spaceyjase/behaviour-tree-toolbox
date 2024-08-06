namespace BehaviourTree.Tests.BehaviourTree;

using Chickensoft.GoDotTest;
using Composite;
using Decorators;
using FluentAssertions;
using Node;

public class SequenceTests(Godot.Node testScene) : TestClass(testScene)
{
    [Test]
    public void CanCreateSequence()
    {
        Sequence sequence = new();
        sequence.Should().NotBeNull();
    }

    [Test]
    public void DefaultStateEvaluateIsSuccess()
    {
        Sequence node = new([new TestDefaultNode()]);
        node.Evaluate(1f);

        node.State.Should().Be(NodeState.Success);
    }

    [Test]
    public void CanCreateSequenceWithChildren()
    {
        Sequence child = new();
        Sequence sequence = new([child]);

        sequence.HasChildren.Should().BeTrue();
    }

    [Test]
    public void Sequence_Evaluate_Failure()
    {
        TestFailureNode child = new();
        Sequence sequence = new([child]);
        sequence.Evaluate(1f);

        sequence.State.Should().Be(NodeState.Failure);
    }

    [Test]
    public void Sequence_Evaluate_Running()
    {
        TestSuccessNode timerChild = new();
        Timer child = new(1f, timerChild);
        Sequence sequence = new([child]);
        sequence.Evaluate(0.5f);

        sequence.State.Should().Be(NodeState.Running);
    }

    [Test]
    public void Sequence_Evaluate_Success()
    {
        TestSuccessNode child = new();
        Sequence sequence = new([child]);
        sequence.Evaluate(1f);

        sequence.State.Should().Be(NodeState.Success);
    }

    [Test]
    public void Sequence_Evaluate_WithChildren_Success()
    {
        TestSuccessNode child1 = new();
        TestSuccessNode child2 = new();
        Sequence sequence = new([child1, child2]);
        sequence.Evaluate(1f);

        sequence.State.Should().Be(NodeState.Success);
    }

    [Test]
    public void Sequence_Evaluate_WithOneChildSuccess_Failure()
    {
        TestSuccessNode child1 = new();
        TestFailureNode child2 = new();
        Sequence sequence = new([child1, child2]);
        sequence.Evaluate(1f);

        sequence.State.Should().Be(NodeState.Failure);
    }

    [Test]
    public void Sequence_Evaluate_WithChildFailure_Failure()
    {
        TestFailureNode child1 = new();
        TestFailureNode child2 = new();
        Sequence sequence = new([child1, child2]);
        sequence.Evaluate(1f);

        sequence.State.Should().Be(NodeState.Failure);
    }
}
