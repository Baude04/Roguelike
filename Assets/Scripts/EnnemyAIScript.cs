using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyAIScript : MonoBehaviour
{
    private CharacterMovementScript movementScript;
    private float t = 0;
    void Start()
    {
        movementScript = GetComponent<CharacterMovementScript>();
    }
    void Move()
    {
        List<Vector2Int> directions = new List<Vector2Int>{Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right};
        System.Random random = new System.Random((int)(Random.value*1000f));
        
        
        while (true){
            Vector2Int direction = HomeMadeFunctions.GetRandom(directions, random);
            #region forbid to go out of his chunk
            Vector2Int pos2d = new Vector2Int((int)transform.position.x, (int)transform.position.y);
            Vector2Int destination = pos2d + direction;
            int xDistToBound = MinDistanceToChunkBounds(destination.x, Constants.CHUNK_WIDTH);
            int yDistToBound = MinDistanceToChunkBounds(destination.y, Constants.CHUNK_HEIGHT);
            if (Mathf.Min(xDistToBound, yDistToBound) == 0)
            {
                directions.Remove(direction);
                continue;
            }
            #endregion

            if (!movementScript.Move(direction))//he has to move each time AI.Move() is called
            {
                directions.Remove(direction);
                continue;
            }
            break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (t + 0.3 < Time.time)
        {
            t = Time.time;
            Move();
        }
    }
    private int MinDistanceToChunkBounds(int pos, int chunkSize)
    {
        int nearestPrevBound = pos - (pos % chunkSize);
        int nearestNexteBound = nearestPrevBound + chunkSize;
        int distancePrevBound = Mathf.Abs(pos - nearestPrevBound);
        int distanceNexteBound = Mathf.Abs(pos - nearestNexteBound);
        return Mathf.Min(distanceNexteBound, distancePrevBound);
    }
}
