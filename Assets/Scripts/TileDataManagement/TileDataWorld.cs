using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
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
                    tileStacks[x, y] = new TileDataStack(TileData.water);
                }
                else if (pointValue < 0.15f)//sand
                {
                    tileStacks[x, y] = new TileDataStack(TileData.sand);
                }
                else                        //grass and tree
                {
                    TileData grass = TileData.grass;
                    if (treeNoiseMap.GetPoint(x, y) < 0.3f)
                    {
                        tileStacks[x, y] = new TileDataStack(grass, TileData.newTree());
                    }
                    else
                    {
                        tileStacks[x, y] = new TileDataStack(grass);
                    }

                }
            }
        }
    }
    public TileDataWorld(Castle castle, Vector2Int roomSize)
    {
        tileStacks = new TileDataStack[castle.GetDimension().x * (roomSize.x-1) + 1, castle.GetDimension().y* (roomSize.y-1) + 1];//les "-1" ou "+1" permettent de faire en sorte que les murs ne soient pas double-épaisseur
        for (int x = 0; x < castle.GetDimension().x; x++)
        {
            for (int y = 0; y < castle.GetDimension().y; y++)
            {
                TileDataStack[,] roomTileArray = RoomToTileArray(castle.GetRoom(x, y), roomSize.x, roomSize.y);
                tileStacks = PutRoomInWorld(tileStacks, roomTileArray,  x * (roomSize.x - 1), y * (roomSize.y - 1));//le "-1" permet de faire en sorte que les murs ne soient pas double-épaisseur
            }
        }
    }

    private TileDataStack[,] PutRoomInWorld(TileDataStack[,] listToFill, TileDataStack[,] listSample, int samplePositionX, int samplePositionY)
    {
        TileDataStack[,] result = listToFill;
        for (int x = 0; x < listSample.GetLength(0); x++)
        {
            for (int y = 0; y < listSample.GetLength(1); y++)
            {
                if (result[x + samplePositionX, y + samplePositionY] == null)
                {
                    result[x + samplePositionX, y + samplePositionY] = listSample[x, y];
                }
                else
                {
                    //Debug.Log("tu l'a échappé belle en position" + x +";" + y);
                }
            }
        }
        return result;
    }

    private TileDataStack[,] RoomToTileArray(CastleRoom room, int width, int height)
    {
        TileDataStack[,] result = new TileDataStack[width, height];

        

        Vector2Int keyIndex = new Vector2Int((width - 1) / 2, (width - 1) / 2);

        result = FillBounds(result);

        //place les sorties
        result = PutExits(room, result);

        //place la clef
        for (int i = 0; i < room.keys.Count; i++)
        {
            result[keyIndex.x, keyIndex.y] = new TileDataStack(TileData.castleFloor, TileData.newKey(room.keys[i]));
        }

        return result;
    }

    private TileDataStack[,] PutExits(CastleRoom room, TileDataStack[,] tileRoom)
    {
        int width = tileRoom.GetLength(0);
        int height = tileRoom.GetLength(1);
        Vector2Int[] exitsIndex = new Vector2Int[4];
        exitsIndex[0] = new Vector2Int((width - 1) / 2, height - 1);
        exitsIndex[1] = new Vector2Int(width - 1, (height - 1) / 2);
        exitsIndex[2] = new Vector2Int((width - 1) / 2, 0);
        exitsIndex[3] = new Vector2Int(0, (height - 1) / 2);
        for (int i = 0; i < room.exits.Length; i++)
        {
            
            int exitConstant = room.exits[i];
            TileData tileDataBottom = TileData.castleFloor;
            if (exitConstant == Constants.HOLE)
            {
                tileRoom[exitsIndex[i].x, exitsIndex[i].y] = new TileDataStack(TileData.castleFloor);
            }
            else if (exitConstant == Constants.NO_EXIT)
            {
                tileRoom[exitsIndex[i].x, exitsIndex[i].y] = new TileDataStack(TileData.castleFloor, TileData.castleWall);
            }
            else if (exitConstant == Constants.LOCKED_DOOR)
            {
                tileRoom[exitsIndex[i].x, exitsIndex[i].y] = new TileDataStack(TileData.castleFloor, TileData.newLockedDoor(room.lockedDoorsIDs[0]));
            }
        }
        return tileRoom;
    }

    /// <summary>
    /// fill the bounds of a room with walls
    /// </summary>
    /// <param name="width">width of the room(in tile)</param>
    /// <param name="height">height of the room(in tile)</param>
    private TileDataStack[,] FillBounds(TileDataStack[,] array)
    {
        int width = array.GetLength(0);
        int height = array.GetLength(1);
        TileDataStack[,] result = new TileDataStack[width, height];
        TileData tileDataBottom = TileData.castleFloor;
        TileData tileDataTop = TileData.castleWall;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x==0 || y==0 || x==width-1 || y==height-1)
                {
                    result[x, y] = new TileDataStack(tileDataBottom, tileDataTop);
                }
                else
                {
                    result[x, y] = new TileDataStack(tileDataBottom);
                }
            }
        }
        return result;
    }
    
    
    public TileDataStack GetStack(int x,int y)
    {
        return tileStacks[x, y];
    }
    public Vector2Int GetDimension()
    {
        return new Vector2Int(tileStacks.GetLength(0), tileStacks.GetLength(1));
    }
}
