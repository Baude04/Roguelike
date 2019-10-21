using UnityEngine;

public class LockedDoorMemento : ITileComponentMemento
{
    public readonly int ID;
    public LockedDoorMemento(int ID)
    {
        this.ID = ID;
    }
    void ITileComponentMemento.RestoreTileComponent(GameObject tile)
    {
        tile.AddComponent<LockedDoor>();
        LockedDoor lockedDoor = tile.GetComponent<LockedDoor>();
        lockedDoor.RestoreFromMemento(this);
    }
}