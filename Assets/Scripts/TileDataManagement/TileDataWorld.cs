using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDataWorld
{
    private TileDataStack[,] tileStacks;
    
    /// <param name="worldNoiseMap">must have the same width and height as treeNoiseMap</param>
    public TileDataWorld(NoiseMap worldNoiseMap, NoiseMap treeNoiseMap)
    {
        tileStacks = new TileDataStack[worldNoiseMap.worldWidth, worldNoiseMap.worldHeight];
        for (int x = 0; x < worldNoiseMap.worldWidth; x++)
        {
            for (int y = 0; y < worldNoiseMap.worldWidth; y++)
            {
                float pointValue = worldNoiseMap.GetPoint(x, y);
                if (pointValue < 0.05f)     //water
                {
                    TileData water = new TileData("TilesTextures/water", false);
                    tileStacks[x, y] = new TileDataStack(water);
                }
                else if (pointValue < 0.15f)//sand
                {
                    TileData sand = new TileData("TilesTextures/sand", true);
                    tileStacks[x, y] = new TileDataStack(sand);
                }
                else                        //grass and tree
                {
                    TileData grass = new TileData("TilesTextures/grass", true);
                    if (treeNoiseMap.GetPoint(x, y) < 0.3f)
                    {
                        TileData tree = new TileData(GetRandomTreeTexturePath(), false, 1);
                        tileStacks[x, y] = new TileDataStack(grass, tree);
                    }
                    else
                    {
                        tileStacks[x, y] = new TileDataStack(grass);
                    }

                }
            }
        }
    }
    private string GetRandomTreeTexturePath()
    {
        return "TilesTextures/tree" + Random.Range(1, 3);//the scnd parameter of this method is not included
    }
    public TileDataStack GetStack(int x,int y)
    {
        return tileStacks[x, y];
    }
}
