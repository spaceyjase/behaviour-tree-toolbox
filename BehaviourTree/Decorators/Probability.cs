namespace BehaviourTree.Decorators;

using System;
using Node;
using System.Linq;

public class Probability : Node
{
    private readonly double runChance;
    private readonly Random rng = new();
    private readonly Node? child;

    public Probability(double chance = 0.5)
    {
        this.runChance = chance;
        if (this.Children.Count() > 1)
            throw new ArgumentException("Probability node can only have one child.");

        this.child = this.Children.First();
    }

    public override NodeState Evaluate(double delta)
    {
        if (!this.HasChildren)
            return NodeState.Failure;

        this.State =
            this.rng.NextDouble() < this.runChance
                ? this.child!.Evaluate(delta)
                : NodeState.Failure;

        return this.State;
    }
}
