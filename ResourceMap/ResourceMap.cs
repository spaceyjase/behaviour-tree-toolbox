namespace Game.ResourceMap;

using Godot;
using System.Collections.Generic;

public partial class ResourceMap : Node
{
    [Export]
    private TileMap? tilemap;

    private Dictionary<Vector2I, int> resourceMap = new();

    public override void _Ready()
    {
        base._Ready();

        this.GenerateResourceMap();
    }

    private void GenerateResourceMap()
    {
        if (this.tilemap is null)
        {
            GD.PrintErr("Tilemap is null");
            return;
        }

        foreach (Vector2I cell in this.tilemap.GetUsedCells(2))
        {
            TileData? tileData = this.tilemap.GetCellTileData(2, cell);
            if (tileData is null)
                continue;

            this.resourceMap[cell] = 10; // TODO: amount based on the atlas cell type
        }
    }

    public bool TryGetAmount(Vector2I cellPosition, out int amount) =>
        this.resourceMap.TryGetValue(cellPosition, out amount);

    public void SetAmount(Vector2I cellPosition, int amount)
    {
        if (this.resourceMap.ContainsKey(cellPosition))
        {
            this.resourceMap[cellPosition] = amount;
        }
    }

    public void Remove(Vector2I cellPosition) => this.resourceMap.Remove(cellPosition);
}
