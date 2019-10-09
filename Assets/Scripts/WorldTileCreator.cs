using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTileCreator : MonoBehaviour
{
    [SerializeField]
    private GameObject grassTile;
    [SerializeField]
    private GameObject treeTile;
    [SerializeField]
    private int worldWidth = 20;
    [SerializeField]
    private int worldHeight = 20;

    private int[,] noiseMap;

    void Start()
    {
        noiseMap = WorldGenerator.GenerateNoiseMap(worldWidth, worldHeight);
        CreateTiles();
    }
    
    private void CreateTiles()
    {
        for (int y = 0; y < worldHeight; y++)
        {
            for (int x = 0; x < worldWidth; x++)
            {
                Vector3 newTilePosition = new Vector3(x, y);
                if (noiseMap[x,y] == 0)
                {
                    Instantiate(grassTile, newTilePosition, Quaternion.identity);
                }
                else if (noiseMap[x, y] == 1 )
                {
                    Instantiate(treeTile, newTilePosition, Quaternion.identity);
                }
            }
        }
    }
}
