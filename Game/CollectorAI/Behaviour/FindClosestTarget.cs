namespace Game.CollectorAI.Behaviour;

using BehaviourTree.Node;
using Godot;
using Constants;

public class FindClosestTarget : BehaviourTree.Node.Node
{
    private readonly Collector collector;
    private readonly TileMap tilemap;
    private readonly bool searchingForResource;

    private const int radius = 50;

    public FindClosestTarget(Collector collector, TileMap? tilemap, bool searchingForResource)
    {
        System.ArgumentNullException.ThrowIfNull(tilemap);

        this.collector = collector;
        this.tilemap = tilemap;
        this.searchingForResource = searchingForResource;
    }

    public override NodeState Evaluate(double delta)
    {
        float bestDistance = float.MaxValue;
        Vector2I bestCell = Vector2I.MaxValue;
        foreach (Vector2I cell in this.tilemap.GetUsedCells(2))
        {
            TileData? tileData = this.tilemap.GetCellTileData(2, cell);
            if (tileData is null)
                continue;
            int customData = tileData.GetCustomData(Constants.Resource).AsInt32();
            if (customData != (int)this.collector.Resource)
                continue;

            Vector2 gridPosition = this.tilemap.MapToLocal(cell);
            float distance = this.collector.GlobalPosition.DistanceTo(gridPosition);
            if (!(distance < bestDistance))
                continue;
            bestDistance = distance;
            bestCell = cell;
        }

        if (bestCell != Vector2I.MaxValue)
        {
            this.Root.SetData(Constants.Target, this.tilemap.MapToLocal(bestCell));
            this.Root.SetData(Constants.TargetCell, bestCell);
            this.Root.SetData(Constants.TargetIsResource, this.searchingForResource);
            this.State = NodeState.Success;

            return this.State;
        }

        this.State = NodeState.Failure;

        return this.State;
    }
}
