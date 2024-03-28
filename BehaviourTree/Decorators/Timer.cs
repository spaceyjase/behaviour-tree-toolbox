namespace BehaviourTree.Decorators;

using System;
using System.Collections.Generic;
using System.Linq;
using Node;

/// <summary>
/// The Timer node waits for a given time before evaluating its part of the tree (repeating endlessly).
/// </summary>
public class Timer : Node
{
    private readonly float delay;
    private float timer;

    private readonly Action? timerElapsed;

    public Timer(float delay, Action? timerElapsed = null)
    {
        this.delay = delay;
        this.timer = this.delay;
        this.timerElapsed = timerElapsed;
    }

    public Timer(float delay, IEnumerable<Node> children, Action? timerElapsed = null)
        : base(children)
    {
        this.delay = delay;
        this.timer = this.delay;
        this.timerElapsed = timerElapsed;
    }

    public override NodeState Evaluate(double delta)
    {
        if (!this.HasChildren)
            return NodeState.Failure;

        if (this.timer <= 0)
        {
            this.timer = this.delay;
            this.State = this.Children.First().Evaluate(delta);
            this.timerElapsed?.Invoke();
            this.State = NodeState.Success; // TODO: the child's state?
        }
        else
        {
            this.timer -= (float)delta;
            this.State = NodeState.Running;
        }

        return this.State;
    }
}
