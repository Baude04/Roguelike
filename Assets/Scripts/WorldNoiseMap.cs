using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseMap
{
    private float[,] noiseMap;
    public readonly int worldWidth;
    public readonly int worldHeight;
    private readonly int seed;

    public float[,] FloatValues { get => noiseMap; }

    /// <param name="scale">représente le "zoom" de la noise map</param>
    public NoiseMap(int worldWidth, int worldHeight, bool rounded, System.Random random, float scale=0.2f)
    {
        this.worldWidth = worldWidth;
        this.worldHeight = worldHeight;
        seed = random.Next(1000);
        noiseMap = CreateNoiseMap(scale);
        if (rounded)
        {
            noiseMap = RoundNoiseMap(noiseMap);
        }
    }

    public float GetPoint(int x, int y)
    {
        return noiseMap[x, y];
    }

    private float[,] CreateNoiseMap(float scale)
    {
        float[,] noiseMap = new float[worldWidth, worldHeight];
        for (int y = 0; y < worldWidth; y++)
        {
            for (int x = 0; x < worldHeight; x++)
            {
                float sampleX = (x + seed) * scale;
                float sampleY = (y + seed) * scale;

                noiseMap[x, y] = Mathf.PerlinNoise(sampleX, sampleY);
            }
        }
        return noiseMap;
    }

    private float[,] RoundNoiseMap(float[,] noiseMap)
    {
        for (int y = 0; y < worldWidth; y++)
        {
            for (int x = 0; x < worldHeight; x++)
            {
                float distanceFromCenterX = Mathf.Abs(x - ((float)worldWidth / 2));
                float distanceFromCenterY = Mathf.Abs(y - ((float)worldHeight / 2));
                float distance = Mathf.Sqrt(Mathf.Pow(distanceFromCenterX, 2) + Mathf.Pow(distanceFromCenterY, 2));
                float maxWidth = ((float)(worldHeight + worldWidth) / 2) / 2 - 10f;
                float delta = distance / maxWidth;
                float gradient = Mathf.Pow(delta, 2);
                float circlePointValue = Mathf.Max(0.0f, 1.0f - gradient);

                noiseMap[x, y] = circlePointValue * noiseMap[x, y];
            }
        }
        return noiseMap;
    }
}
