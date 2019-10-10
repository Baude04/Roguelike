using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWorld : MonoBehaviour
{
    [SerializeField]
    private GameObject tilePrefab;
    [SerializeField]
    private int worldWidth = 50;
    [SerializeField]
    private int worldHeight = 50;
    private NoiseMap treeNoiseMap;
    private NoiseMap worldNoiseMap;
    private TileDataWorld world;
    private System.Random random;
    private void Start()
    {
        random = new System.Random(Constants.SEED);
        worldNoiseMap = new NoiseMap(worldWidth, worldHeight, true, random);
        treeNoiseMap = new NoiseMap(worldWidth, worldHeight, false, random);
        world = new TileDataWorld(worldNoiseMap, treeNoiseMap);
        for (int y = 0; y < worldHeight; y++)
        {
            for (int x = 0; x < worldWidth; x++)
            {
                TileDataStack tileDataStack = world.GetStack(x, y);
                Debug.Log(tileDataStack);
                CreateTileFromData(y, x, tileDataStack.bottomTile);

                if (tileDataStack.topTile != null)
                {
                    CreateTileFromData(y, x, tileDataStack.topTile);
                }

            }
        }
    }

    private void CreateTileFromData(int y, int x, TileData tileData)
    {
        GameObject tile = Instantiate(tilePrefab);
        tile.transform.position = new Vector3(x, y);
        tile.AddComponent<TileSaveManager>().Restore(tileData);
    }
}
