namespace BehaviourTree.Node;

using System.Collections.Generic;

public class NodeData
{
    private readonly Dictionary<string, object?> data = new();

    public bool TryGetValue(string key, out object? value) => this.data.TryGetValue(key, out value);

    public void SetValue(string key, object? value) => this.data[key] = value;

    public bool RemoveValue(string key) => this.data.Remove(key);
}
