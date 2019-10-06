using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WorldGenerator
{
    public static int[,] GenerateWorld (int worldWidth, int worldHeight, float scale = 0.2f, float treeFrequency = 0.30f)
    {
        //scale représente le "zoom" de la noise map
        int[,] World = new int[worldWidth, worldHeight];

        for (int y = 0; y < worldWidth; y++)
        {
            for (int x = 0; x < worldHeight; x++)
            {
                float sampleX = x * scale;
                float sampleY = y * scale;

                float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);
                if (perlinValue <= treeFrequency)
                {
                    World[x, y] = 1;
                }
                else
                {
                    World[x, y] = 0;
                }
            }

        }
        return World;
    }
}
