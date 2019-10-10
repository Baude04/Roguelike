using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleTileGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject roomPrefab;
    [SerializeField]
    private GameObject keyPrefab;
    [SerializeField]
    private GameObject holePrefab;
    [SerializeField]
    private GameObject lockedDoorPrefab;
    [SerializeField]
    private GameObject leftDoor;
    [SerializeField]
    private GameObject upDoor;
    [SerializeField]
    private GameObject rightDoor;
    [SerializeField]
    private GameObject downDoor;
    [SerializeField]
    private GameObject debugPrefab;
    [SerializeField]
    private const int CASTLE_WIDTH = 10;
    [SerializeField]
    private const int CASTLE_HEIGHT = 10;
    // Start is called before the first frame update
    void Start()
    {
        Castle castle = new Castle(CASTLE_WIDTH, CASTLE_HEIGHT, 5, Constants.SEED);
        Debug.Log("---CastleTileGeneration---");
        for (int y = 0; y < CASTLE_HEIGHT; y++)
        {
            for (int x = 0; x < CASTLE_WIDTH; x++)
            {
                #region exitCreation
                Vector3 exitPosition;
                GameObject roomGameObject = Instantiate(roomPrefab, new Vector3(x * 6, y * 6), Quaternion.identity);
                string[] directionsNames = new string[4] { "Up", "Right", "Down", "Left" };
                Debug.Log("création de la salle" + "(" + x + ";" + y + ")");
                CastleRoom room = castle.GetRoom(x, y);
                Debug.Log("ouvertures de la salle:" + room.ToString());


                for (int i = 0; i <= 3; i++)
                {
                    if (room.exits[i] != Constants.NO_EXIT)
                    {
                        
                        exitPosition = roomGameObject.transform.Find(directionsNames[i]).transform.position;
                        Destroy(roomGameObject.transform.Find(directionsNames[i]).gameObject);
                        GameObject door = Instantiate(GetPrefab(room.exits[i]), exitPosition, Quaternion.identity, roomGameObject.transform);
                        if (room.exits[i] == Constants.LOCKED_DOOR)
                        {
                            LockedDoorScript doorScript = door.GetComponent<LockedDoorScript>();
                            doorScript.SetId(room.lockedDoorsIDs[i]);
                        }
                    }

                    if (room.isGenerated)//////debuuuuug
                    {
                        exitPosition = roomGameObject.transform.Find("middlePoint").transform.position;
                        Destroy(roomGameObject.transform.Find("middlePoint").gameObject);
                        Instantiate(debugPrefab, exitPosition, Quaternion.identity, roomGameObject.transform);
                    }
                    #endregion
                    #region keysCreation
                    if (room.keys.Count > 0)
                    {
                        Vector3 keyPosition = roomGameObject.transform.Find("keyLocation").transform.position;
                        Destroy(roomGameObject.transform.Find("keyLocation").gameObject);
                        Instantiate(keyPrefab, keyPosition, Quaternion.identity, roomGameObject.transform);
                        Debug.Log("clef posée en " + x + " " + y);
                    }
                    #endregion
                }
            }
        }
    }

    private GameObject GetPrefab(int exitType)
    {
        switch (exitType)
        {
            case Constants.HOLE:
                Debug.Log("hole created");
                return holePrefab;
            case Constants.LEFT_DOOR:
                Debug.Log("création de porte a sens unique");
                return leftDoor;
            case Constants.UP_DOOR:
                Debug.Log("création de porte a sens unique");
                return upDoor;
            case Constants.RIGHT_DOOR:
                Debug.Log("création de porte a sens unique");
                return rightDoor;
            case Constants.DOWN_DOOR:
                Debug.Log("création de porte a sens unique");
                return downDoor;
            case Constants.LOCKED_DOOR:
                Debug.Log("création de porte verouillé");
                return lockedDoorPrefab;
            default:
                Debug.LogError("unknown value");
                return gameObject;
        }
    }
}
