namespace BehaviourTree.Node;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

public class NodeData : INodeData
{
    private readonly Dictionary<string, object?> data = new();

    public bool TryGetValue(string key, [NotNullWhen(true)] out object? value) =>
        this.data.TryGetValue(key, out value);

    public void SetValue(string key, object? value) => this.data[key] = value;

    public bool RemoveValue(string key) => this.data.Remove(key);
}
