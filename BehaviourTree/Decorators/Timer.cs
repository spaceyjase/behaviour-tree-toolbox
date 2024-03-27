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
    private System.Timers.Timer timer = new(); // TODO: could use an engine timer (needs to be in the scene tree?)
    private bool timeout;

    public readonly Action? timerElapsed;

    public Timer(float delay, Action? timerElapsed = null)
    {
        this.delay = delay;
        this.timerElapsed = timerElapsed;

        this.ConfigureTimer();
    }

    private void ConfigureTimer()
    {
        this.timer = new System.Timers.Timer(this.delay);
        this.timeout = false;
        this.timer.Elapsed += (_, _) =>
        {
            this.timeout = true;
        };
        this.timer.Enabled = true;
        this.timer.Start();
    }

    public Timer(float delay, IEnumerable<Node> children, Action? timerElapsed = null)
        : base(children)
    {
        this.delay = delay;
        this.timerElapsed = timerElapsed;

        this.ConfigureTimer();
    }

    public override NodeState Evaluate()
    {
        if (!this.HasChildren)
            return NodeState.Failure;

        if (this.timeout)
        {
            this.timer.Start();
            this.timeout = false;
            this.state = this.Children.First().Evaluate();
            this.timerElapsed?.Invoke();
            return this.state;
        }
        else
        {
            this.state = NodeState.Running;
            return this.state;
        }
    }
}
