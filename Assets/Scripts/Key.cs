using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private int ID;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestoreFromMemento(KeyMemento keyMemento)
    {
        int ID = keyMemento.ID;

        SpriteRenderer spriteRenderer;
        spriteRenderer = GetComponent<SpriteRenderer>();
        System.Random random = new System.Random(ID);
        float red = (float)random.NextDouble() % 1;
        float green = (float)random.NextDouble() % 1;
        float blue = (float)random.NextDouble() % 1;
        spriteRenderer.color = new Color(red, green, blue);
    }
}
