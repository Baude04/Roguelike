using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementScript : MonoBehaviour
{
    [SerializeField]
    private Sprite upSprite;
    [SerializeField]
    private Sprite downSprite;
    [SerializeField]
    private Sprite leftSprite;
    [SerializeField]
    private Sprite rightSprite;

    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    /// <summary>
    /// Move the character in the direction(he can only move one unit)
    /// </summary>
    /// <param name="direction">direction of the movement, values accepted are Vector2Int.zero,Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right</param>
    /// <returns>true if he has moved, else false</returns>
    public bool Move(Vector2Int direction)
    {
        Sprite newSprite = downSprite;      //default value
        if (CanMove(direction))
        {
            //change sprite
            if (direction == Vector2Int.zero) newSprite = downSprite;
            else if (direction == Vector2Int.up) newSprite = upSprite;
            else if (direction == Vector2Int.down) newSprite = downSprite;
            else if (direction == Vector2Int.left) newSprite = leftSprite;
            else if (direction == Vector2Int.right) newSprite = rightSprite;
            else Debug.LogError("Unexpected value"+direction.x+";"+direction.y);
            spriteRenderer.sprite = newSprite;
            //move
            transform.Translate(new Vector3(direction.x, direction.y));
            return true;
        }
        return false;
    }
    private bool CanMove(Vector2Int direction)
    {
        RaycastHit2D[] hits;
        hits = Physics2D.RaycastAll(transform.position + new Vector3(0.5f, 0.5f), new Vector2(direction.x, direction.y), 1f);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform != this.transform && !hit.collider.isTrigger)
            {
                return false;
            }
        }
        return true;
    }
    /*private void OnMouseDown()
    //test
    {
        Move(Vector2Int.right);
    }*/
}
