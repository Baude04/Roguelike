using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSaveManager : MonoBehaviour
{
    public void Restore(TileData data)
    {
        string texturePath = data.GetTexturePath();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>(texturePath);
        spriteRenderer.sortingOrder = data.GetLayer();
        gameObject.name = texturePath.Split('/')[1];
    }
}
