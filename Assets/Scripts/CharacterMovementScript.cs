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
        if (direction == Vector2Int.up) newSprite = upSprite;
        else if (direction == Vector2Int.down) newSprite = downSprite;
        else if (direction == Vector2Int.left) newSprite = leftSprite;
        else if (direction == Vector2Int.right) newSprite = rightSprite;
        else Debug.LogError("Unexpected value");
        spriteRenderer.sprite = newSprite;

        transform.Translate(new Vector3(direction.x, direction.y));
    }
    /*private void OnMouseDown()
    //test
    {
        Move(Vector2Int.right);
    }*/
}
