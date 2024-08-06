namespace BehaviourTree.Tests.BehaviourTree;

using Chickensoft.GoDotTest;
using Decorators;
using FluentAssertions;
using Node;

public class EvaluateConditionTests(Godot.Node testScene) : TestClass(testScene)
{
    [Test]
    public void CanCreateEvaluateCondition()
    {
        EvaluateCondition evaluateCondition = new(() => true);
        evaluateCondition.Should().NotBeNull();
    }

    [Test]
    public void EvaluateCondition_Evaluate_True()
    {
        EvaluateCondition evaluateCondition = new(() => true);
        evaluateCondition.Evaluate(1f);

        evaluateCondition.State.Should().Be(NodeState.Success);
    }

    [Test]
    public void EvaluateCondition_Evaluate_False()
    {
        EvaluateCondition evaluateCondition = new(() => false);
        evaluateCondition.Evaluate(1f);

        evaluateCondition.State.Should().Be(NodeState.Failure);
    }
}
