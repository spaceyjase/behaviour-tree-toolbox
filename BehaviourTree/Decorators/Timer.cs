namespace BehaviourTree.Decorators;

using System;
using System.Linq;
using Node;

/// <summary>
/// The Timer node waits for a given time before evaluating its part of the tree (repeating endlessly).
/// </summary>
public class Timer : Node
{
    private readonly double delay;
    private double timer;

    private readonly Action? timerElapsed;
    private readonly INode? child;

    public Timer(double delay, Action? timerElapsed = null)
    {
        this.delay = delay;
        this.timer = this.delay;
        this.timerElapsed = timerElapsed;
    }

    public Timer(double delay, INode children, Action? timerElapsed = null)
        : base([children])
    {
        this.delay = delay;
        this.timer = this.delay;
        this.timerElapsed = timerElapsed;
        this.child = this.Children.First();
    }

    public override NodeState Evaluate(double delta)
    {
        if (this.timer <= 0)
        {
            this.timer = this.delay;
            if (this.child is not null)
                this.State = this.child.Evaluate(delta);
            this.timerElapsed?.Invoke();
        }
        else
        {
            this.timer -= delta;
            this.State = NodeState.Running;
        }

        return this.State;
    }
}
