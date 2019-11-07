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
    public void Move(Vector2Int direction)
    {
        Sprite newSprite = downSprite;      //default value
        if (CanMove(direction))
        {
            if (direction == Vector2Int.zero) newSprite = downSprite;
            else if (direction == Vector2Int.up) newSprite = upSprite;
            else if (direction == Vector2Int.down) newSprite = downSprite;
            else if (direction == Vector2Int.left) newSprite = leftSprite;
            else if (direction == Vector2Int.right) newSprite = rightSprite;
            else Debug.LogError("Unexpected value"+direction.x+";"+direction.y);
            spriteRenderer.sprite = newSprite;
            transform.Translate(new Vector3(direction.x, direction.y));
        }
        
    }
    private bool CanMove(Vector2Int direction)
    {
        RaycastHit2D[] hits;
        hits = Physics2D.RaycastAll(transform.position + new Vector3(0.5f, 0.5f), new Vector2(direction.x, direction.y), 1f);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform != this && !hit.collider.isTrigger)
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
