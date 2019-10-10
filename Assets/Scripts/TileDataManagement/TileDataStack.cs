using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDataStack
{
    public TileData topTile;
    public TileData bottomTile;
    public TileDataStack(TileData bottomTile)
    {
        this.bottomTile = bottomTile;
    }
    public TileDataStack(TileData bottomTile, TileData topTile)
    {
        this.bottomTile = bottomTile;
        this.topTile = topTile;
    }
}
