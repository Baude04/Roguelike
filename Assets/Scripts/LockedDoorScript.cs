using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoorScript : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;/*
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }*/
    public void SetId(int ID)
    {
        Debug.LogWarning(ID);
        spriteRenderer.color = new Color(0, HomeMadeFunctions.Map(0f, 1f, -1, 5, ID), 0);
    }
}
