namespace BehaviourTree.Tests.Game;

using System;
using Chickensoft.GoDotTest;
using FluentAssertions;
using global::Game.CollectorAI.Behaviour;
using global::Game.Constants;
using Node;

public partial class CheckReachedMaxStorageTests(Godot.Node testScene) : TestClass(testScene)
{
    [Test]
    public void CanCreateNode()
    {
        CheckReachedMaxStorage node = new(1);
        node.Should().NotBeNull();
    }

    [Test]
    public void CheckReachedMaxStorage_Evaluate_NoData_ThrowsException()
    {
        CheckReachedMaxStorage node = new(1);
        Action act = () => node.Evaluate(0f);
        act.Should().Throw<NullReferenceException>();
    }

    [Test]
    public void CheckReachedMaxStorage_Evaluate_StorageNotFull_Failure()
    {
        CheckReachedMaxStorage node = new(1);
        node.SetData(Constants.CurrentResourceAmount, 0);
        node.Evaluate(0).Should().Be(NodeState.Failure);
    }

    [Test]
    public void CheckReachedMaxStorage_Evaluate_StorageFull_Success()
    {
        CheckReachedMaxStorage node = new(1);
        node.SetData(Constants.CurrentResourceAmount, 1);
        node.Evaluate(0).Should().Be(NodeState.Success);
    }
}
