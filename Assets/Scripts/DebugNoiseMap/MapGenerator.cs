using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int mapWidth;
    public int mapHeight;
    public float noiseScale;

    public bool autoUpdate;

    public void GenerateMap()
    {
        System.Random random = new System.Random(Random.Range(0, 1000));
        float[,] noiseMap = new NoiseMap(mapWidth, mapHeight, true, random, noiseScale).FloatValues;

        MapDisplay display = FindObjectOfType<MapDisplay>();
        display.DrawNoiseMap(noiseMap);
    }
}