using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    private int ID;
    public void RestoreFromMemento(LockedDoorMemento memento)
    {
        ID = memento.ID;
    }
    void Update()
    {
        SpriteRenderer spriteRenderer;
        spriteRenderer = GetComponent<SpriteRenderer>();
        System.Random random = new System.Random(ID);
        float red = (float)random.NextDouble() % 1;
        float green = (float)random.NextDouble() % 1;
        float blue = (float)random.NextDouble() % 1;
        spriteRenderer.color = new Color(red, green, blue);
    }
}
