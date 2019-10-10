using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileData
{
    private string texturePath;
    private bool crossable;
    private int layer;
    public TileData(string texturePath, bool crossable, int layer=0)
    {
        this.texturePath = texturePath;
        this.crossable = crossable;
        this.layer = layer;
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
