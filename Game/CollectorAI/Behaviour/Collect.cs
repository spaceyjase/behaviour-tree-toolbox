namespace Game.CollectorAI.Behaviour;

using System;
using BehaviourTree.Node;
using Godot;
using Constants;
using Node = BehaviourTree.Node.Node;

public class Collect(int maxStorage, TileMap? tilemap) : Node
{
    private const int CollectAmount = 1;

    private readonly TileMap tilemap = tilemap ?? throw new ArgumentNullException(nameof(tilemap));

    public override NodeState Evaluate(double delta)
    {
        int currentAmount = (int)(
            this.Root.GetData(Constants.CurrentResourceAmount)
            ?? throw new NullReferenceException($"{Constants.CurrentResourceAmount} is null")
        );
        int newAmount = currentAmount + CollectAmount;
        if (newAmount > maxStorage)
        {
            newAmount = maxStorage;
        }

        this.Root.SetData(Constants.CurrentResourceAmount, newAmount);

        Vector2I cellPosition = (Vector2I)(
            this.Root.GetData(Constants.TargetCell)
            ?? throw new NullReferenceException($"{Constants.TargetCell} is null")
        );

        TileData? tileData = this.tilemap.GetCellTileData(2, cellPosition);
        if (tileData is not null)
        {
            int amount = tileData.GetCustomData(Constants.Amount).AsInt32();
            amount -= CollectAmount;
            if (amount < 0)
            {
                amount = 0;
                this.tilemap.SetCell(
                    2 /* resources */
                    ,
                    cellPosition
                );
                this.ClearTarget();
            }

            tileData.SetCustomData(Constants.Amount, amount);
        }
        else
        { // tile may have been consumed by another collector
            this.ClearTarget();
        }

        this.State = NodeState.Running;
        return this.State;
    }

    private void ClearTarget()
    {
        this.Root.RemoveData(Constants.Target);
        this.Root.RemoveData(Constants.TargetCell);
        this.Root.RemoveData(Constants.TargetIsResource);
    }
}
