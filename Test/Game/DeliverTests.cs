namespace BehaviourTree.Tests.Game;

using Chickensoft.GoDotTest;
using Features.EventBus;
using FluentAssertions;
using global::Game.CollectorAI.Behaviour;
using global::Game.Constants;
using global::Game.Enum;
using Godot;

public class DeliverTests(Node testScene) : TestClass(testScene)
{
    [Test]
    public void CanCreateDeliverNode()
    {
        Deliver deliver = new(ResourceType.Wood);
        deliver.Should().NotBeNull();
    }

    [Test]
    public void Deliver_CallsEventBusWithCorrectData()
    {
        bool eventBusCalled = false;
        EventBus.Instance.ResourceCollected += (resourceAmount, resourceType) =>
        {
            resourceAmount.Should().Be(10);
            resourceType.Should().Be((int)ResourceType.Wood);
            eventBusCalled = true;
        };
        Deliver deliver = new(ResourceType.Wood);
        deliver.Root.SetData(Constants.CurrentResourceAmount, 10);
        deliver.Evaluate(0);

        eventBusCalled.Should().BeTrue();
    }
}
