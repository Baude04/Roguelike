using System.Collections.Generic;
using UnityEngine;

public class CastleRoom
{

    public int[] exits = new int[4]{0,0,0,0};
    public int[] lockedDoorsIDs = new int[4] { -1, -1, -1, -1 };
    public List<int> keys = new List<int>();
    public bool isGenerated = false;
    public bool chest;

    private readonly Vector2Int[] directions = new Vector2Int[] { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left};//variable qui permet de factoriser le code a certains endroits

public void Generate(List<int> keysToInstall, System.Random random)
    {
        if (keysToInstall.Count != 0 && random.NextDouble() > 1f / (keysToInstall.Count) / 1.10f)
        {
            int keyIDPosition = random.Next(keysToInstall.Count);
            int keyID = keysToInstall[keyIDPosition];
            if (!IsInArray(lockedDoorsIDs, keyID))
            {
                keysToInstall.RemoveAt(keyIDPosition);
                keys.Add(keyID);
            }
        }
        isGenerated = true;
    }
    private bool IsInArray(int[] array, int value)
    {
        foreach (int item in array)
        {
            if (item == value)
            {
                return true;
            }
        }
        return false;
    }
    public void CreateHoleExit(Vector2Int newExitLocation)
    {
        for (int i = 0; i < exits.Length; i++)
        {
            if (directions[i] == newExitLocation && exits[i] == Constants.NO_EXIT)
            {
                exits[i] = Constants.HOLE;
            }
        }
    }


    public void CreateOneWayDoor(bool isGoingFromThisRoom, Vector2Int newOneWayDoorLocation)
    {
        if (newOneWayDoorLocation == Vector2Int.up)
        {
            //rappelons que DOOR_UP est l'opposé de DOOR_DOWN et idem pour right/down
            //et que une right door s'appelle ainsi car elle s'ouvre par la droite
            exits[0] = Constants.UP_DOOR * (isGoingFromThisRoom ? -1 : 1);
        }
        else if (newOneWayDoorLocation == Vector2Int.right)
        {
            exits[1] = Constants.RIGHT_DOOR * (isGoingFromThisRoom ? -1 : 1);
        }
        else if (newOneWayDoorLocation == Vector2Int.down)
        {
            exits[2] = Constants.DOWN_DOOR * (isGoingFromThisRoom ? -1 : 1);
        }
        else if (newOneWayDoorLocation == Vector2Int.left)
        {
            exits[3] = Constants.LEFT_DOOR * (isGoingFromThisRoom ? -1 : 1);
        }
    }

    public void CreateLockedDoor(int keyID, Vector2Int newLockedDoorLocation)
    {
        Debug.Log("locked door");
        for (int i = 0; i < exits.Length; i++)
        {
            if (directions[i] == newLockedDoorLocation)
            {
                exits[i] = Constants.LOCKED_DOOR;
                lockedDoorsIDs[i] = keyID;
            }
        }
    }

    public override string ToString()
    {
        string returnString = "";
        if (exits[0] != Constants.NO_EXIT)
        {
            returnString += "UP ";
        }
        if (exits[1] != Constants.NO_EXIT)
        {
            returnString += "RIGHT ";
        }
        if (exits[2] != Constants.NO_EXIT)
        {
            returnString += "DOWN ";
        }
        if (exits[3] != Constants.NO_EXIT)
        {
            returnString += "LEFT ";
        }
        if (returnString == "")
        {
            returnString = "NOTHING";
        }
        return returnString;
    }
}
//truc a faire: fonction pour bloquer le chemin principale
//              fonction pour installer des clefs de partout