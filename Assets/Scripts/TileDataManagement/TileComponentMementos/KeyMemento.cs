using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyMemento : ITileComponentMemento
{
    public int ID;
    public KeyMemento(int ID) => this.ID = ID;
    void ITileComponentMemento.RestoreTileComponent(GameObject tile)
    {
        tile.AddComponent<Key>();
        Key key = tile.GetComponent<Key>();
        key.RestoreFromMemento(this);
    }
}
