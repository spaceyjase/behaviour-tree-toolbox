namespace BehaviourTree.Node;

using System.Diagnostics.CodeAnalysis;

public interface INodeData
{
    bool TryGetValue(string key, [NotNullWhen(true)] out object? value);
    void SetValue(string key, object? value);
    bool RemoveValue(string key);
}
