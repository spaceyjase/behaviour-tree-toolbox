namespace BehaviourTree.Tests.BehaviourTree;

using Chickensoft.GoDotTest;
using Decorators;
using FluentAssertions;
using Node;

public class AlwaysSucceedTests(Godot.Node testScene) : TestClass(testScene)
{
    [Test]
    public void CanCreateAlwaysSucceed()
    {
        AlwaysSucceed alwaysSucceed = new(new TestSuccessNode());
        alwaysSucceed.Should().NotBeNull();
    }

    [Test]
    public void AlwaysSucceed_Evaluate_Success()
    {
        AlwaysSucceed alwaysSucceed = new(new TestSuccessNode());
        alwaysSucceed.Evaluate(1f);

        alwaysSucceed.State.Should().Be(NodeState.Success);
    }

    [Test]
    public void AlwaysSucceed_Evaluate_Failure_ShouldSucceed()
    {
        AlwaysSucceed alwaysSucceed = new(new TestFailureNode());
        alwaysSucceed.Evaluate(1f);

        alwaysSucceed.State.Should().Be(NodeState.Success);
    }

    [Test]
    public void AlwaysSucceed_Evaluate_Running()
    {
        AlwaysSucceed alwaysSucceed = new(new TestRunningNode());
        alwaysSucceed.Evaluate(1f);

        alwaysSucceed.State.Should().Be(NodeState.Running);
    }
}
