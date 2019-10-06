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

    private int[,] world;

    void Start()
    {
        world = WorldGenerator.GenerateWorld(worldWidth, worldHeight);
        CreateTiles();
        CastleGenerator.GenerateCastle(20, 20, 10);
    }
    
    private void CreateTiles()
    {
        for (int y = 0; y < worldHeight; y++)
        {
            for (int x = 0; x < worldWidth; x++)
            {
                Vector3 newTilePosition = new Vector3(x, y);
                if (world[x,y] == 0)
                {
                    Instantiate(grassTile, newTilePosition, Quaternion.identity);
                }
                else if (world[x, y] == 1 )
                {
                    Instantiate(treeTile, newTilePosition, Quaternion.identity);
                }
            }
        }
    }
}
