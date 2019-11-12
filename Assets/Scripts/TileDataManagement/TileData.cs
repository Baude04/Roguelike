using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileData
{
    #region static
    public static TileData newKey(int ID)
    {
        Debug.Log("clef posée");
        TileData result = new TileData("TilesTextures/key", true, 1);
        result.AddMemento(new KeyMemento(ID));
        return result;
    }

    public static TileData newTree()
    {
        string texture = "TilesTextures/tree" + UnityEngine.Random.Range(1, 3);
        return new TileData(texture, false, 1);
    }
    public static TileData newLockedDoor(int ID)
    {
        TileData result = new TileData("TilesTextures/castleLockedDoor", false, 1);
        result.AddMemento(new LockedDoorMemento(ID));
        return result;
    }
    public static readonly TileData grass = new TileData("TilesTextures/grass", true);
    public static readonly TileData sand = new TileData("TilesTextures/sand", true);
    public static readonly TileData water = new TileData("TilesTextures/water", false);
    public static readonly TileData castleFloor = new TileData("TilesTextures/castleFloor", true, 0);
    public static readonly TileData castleWall = new TileData("TilesTextures/castleWall", false, 1);
    #endregion
    private string texturePath;
    private bool crossable;
    private int layer;
    private List<ITileComponentMemento> mementos = new List<ITileComponentMemento>();

    public TileData(string texturePath, bool crossable, int layer=0)
    {
        this.texturePath = texturePath;
        this.crossable = crossable;
        this.layer = layer;
    }
    public void AddMemento(ITileComponentMemento newMemento)
    {
        mementos.Add(newMemento);
    }

    public void Restore(GameObject gameObject)
    {
        string texturePath = GetTexturePath();
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>(texturePath);
        spriteRenderer.sortingOrder = GetLayer();
        gameObject.name = texturePath.Split('/')[1];
        BoxCollider2D coll = gameObject.AddComponent<BoxCollider2D>();

        if (crossable)
        {
            coll.isTrigger = true;
        }
        else coll.isTrigger = false;

        for (int i = 0; i < mementos.Count; i++)
        {
            mementos[i].RestoreTileComponent(gameObject);
        }
    }

    public string GetTexturePath()
    {
        return texturePath;
    }
    public int GetLayer()
    {
        return layer;
    }
}
