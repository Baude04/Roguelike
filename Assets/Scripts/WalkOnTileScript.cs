using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkOnTileScript : MonoBehaviour
{
    private void OnMouseDown()
    {
        GameObject player = GameObject.FindWithTag("Player");
        Debug.Log("click");
        Debug.Log(Vector3.Distance(transform.position, player.transform.position));
        if (Vector3.Distance(transform.position, player.transform.position) <= 1f)
        {
            Debug.Log("dist<1");
            Vector3 direction3d = transform.position - player.transform.position;
            Vector2Int direction2d = new Vector2Int((int)direction3d.x, (int)direction3d.y);
            if (player.GetComponent<CharacterMovementScript>().Move(direction2d))
            {
                Clock.tic.Invoke();
            }
        }
    }
}
