namespace BehaviourTree.Node;

public interface INode
{
    NodeState Evaluate(double delta);
    object? GetData(string key);
    void SetData(string key, object value);
    bool RemoveData(string key);

    INode Root { get; set; }
    INode? Parent { get; set; }
}
