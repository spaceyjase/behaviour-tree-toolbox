namespace BehaviourTree.Tests.BehaviourTree;

using Chickensoft.GoDotTest;
using FluentAssertions;
using Decorators;
using FlowControl.Selector;
using Node;

public class TimerTests(Godot.Node testScene) : TestClass(testScene)
{
    [Test]
    public void CanCreateTimer()
    {
        Timer timer = new(100f);
        timer.Should().NotBeNull();
    }

    [Test]
    public void CanCreateTimerWithChildren()
    {
        Selector child = new();
        Timer timer = new(100f, new Node[] { child });

        timer.HasChildren.Should().BeTrue();
    }

    [Test]
    public void Timer_CallTimerElapsed()
    {
        bool timerElapsed = false;

        Selector child = new();
        Timer timer = new(100f, new Node[] { child }, () => timerElapsed = true);
        timer.Evaluate(100f);
        timer.Evaluate(100f);

        timerElapsed.Should().BeTrue();
    }

    [Test]
    public void Timer_Evaluate()
    {
        Selector child = new();
        Timer timer = new(100f, new Node[] { child });
        timer.Evaluate(50f);

        timer.State.Should().Be(NodeState.Running);
    }

    [Test]
    public void Timer_Evaluate_Success()
    {
        Selector child = new();
        Timer timer = new(100f, new Node[] { child });
        timer.Evaluate(100f).Should().Be(NodeState.Running);
        timer.Evaluate(100f).Should().Be(NodeState.Success);
    }

    [Test]
    public void Timer_Evaluate_Failure()
    {
        Timer timer = new(50f);
        timer.Evaluate(100f).Should().Be(NodeState.Failure);
    }
}
