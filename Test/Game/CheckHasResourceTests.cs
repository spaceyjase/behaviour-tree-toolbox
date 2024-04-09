namespace BehaviourTree.Tests.Game;

using System;
using Chickensoft.GoDotTest;
using FluentAssertions;
using global::Game.Constants;
using Node;
using Node = Godot.Node;

public class CheckHasResourceTests(Node testScene) : TestClass(testScene)
{
    [Test]
    public void CheckHasResource_NullResource_Throws()
    {
        CheckHasResource checkHasResource = new();
        checkHasResource.Invoking(c => c.Evaluate(0)).Should().Throw<NullReferenceException>();
    }

    [Test]
    public void CheckHasResource_NoResource_Failure()
    {
        CheckHasResource checkHasResource = new();
        checkHasResource.Root.SetData(Constants.CurrentResourceAmount, 0);
        checkHasResource.Evaluate(0).Should().Be(NodeState.Failure);
    }

    [Test]
    public void CheckHasResource_NoResource_Success()
    {
        CheckHasResource checkHasResource = new();
        checkHasResource.Root.SetData(Constants.CurrentResourceAmount, 1);
        checkHasResource.Evaluate(0).Should().Be(NodeState.Success);
    }
}
