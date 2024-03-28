namespace Game.CollectorAI.Behaviour;

using BehaviourTree.Node;
using Godot;

public class FindClosestTarget : BehaviourTree.Node.Node
{
    private readonly Collector collector;
    private readonly TileMap tilemap;
    private readonly int maxGridSize;
    private readonly bool searchingForResource;

    public FindClosestTarget(Collector collector, TileMap? tilemap, bool searchingForResource)
    {
        System.ArgumentNullException.ThrowIfNull(tilemap);

        this.collector = collector;
        this.tilemap = tilemap;
        Rect2I size = tilemap.GetUsedRect();
        this.maxGridSize = Mathf.Max(
            size.Size.X * tilemap.TileSet.TileSize.X,
            size.Size.Y * tilemap.TileSet.TileSize.Y
        );
        this.searchingForResource = searchingForResource;
    }

    public override NodeState Evaluate(double delta)
    {
        Vector2I position = (Vector2I)this.collector.GlobalPosition.Floor();
        for (int radius = 0; radius < this.maxGridSize; ++radius)
        {
            for (int x = position.X - radius; x <= position.X + radius; ++x)
            {
                for (int y = position.Y - radius; y <= position.Y + radius; ++y)
                {
                    Vector2 gridPosition = new(x, y);
                    Vector2I cellPosition = this.tilemap.LocalToMap(
                        this.tilemap.ToLocal(gridPosition)
                    );
                    TileData? tileData = this.tilemap.GetCellTileData(2, cellPosition);
                    if (tileData is null)
                        continue;
                    int customData = tileData.GetCustomData("Resource").AsInt32();
                    if (customData != (int)this.collector.Resource)
                        continue;

                    this.Root?.SetData("target", gridPosition);
                    this.Root?.SetData("target_cell", cellPosition);
                    this.Root?.SetData("target_is_resource", this.searchingForResource);
                    this.State = NodeState.Success;

                    return this.State;
                }
            }
        }

        this.State = NodeState.Failure;

        return this.State;
    }
}
