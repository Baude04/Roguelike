using System.Collections.Generic;
using UnityEngine;

public class CastleRoom
{

    public int[] exits = new int[4]{0,0,0,0};
    public int[] lockedDoorsIDs = new int[4] { -1, -1, -1, -1 };
    public List<int> keys = new List<int>();
    public bool isGenerated = false;
    public bool chest;

    public void Generate(List<int> keysToInstall)
    {
        if (keysToInstall.Count != 0 && CastleGenerator.random.NextDouble() > 1f / (keysToInstall.Count) / 1.10f)
        {
            int keyIDPosition = CastleGenerator.random.Next(keysToInstall.Count);
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
        if (newExitLocation == Vector2Int.up)
        {
            exits[0] = Constants.HOLE;
        }
        else if (newExitLocation == Vector2Int.right)
        {
            exits[1] = Constants.HOLE;
        }
        else if (newExitLocation == Vector2Int.down)
        {
            exits[2] = Constants.HOLE;
        }
        else if (newExitLocation == Vector2Int.left)
        {
            exits[3] = Constants.HOLE;
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
        if (newLockedDoorLocation == Vector2Int.up)
        {
            exits[0] = Constants.LOCKED_DOOR;
            lockedDoorsIDs[0] = keyID;
        }
        else if (newLockedDoorLocation == Vector2Int.right)
        {
            exits[1] = Constants.LOCKED_DOOR;
            lockedDoorsIDs[1] = keyID;
        }
        else if (newLockedDoorLocation == Vector2Int.down)
        {
            exits[2] = Constants.LOCKED_DOOR;
            lockedDoorsIDs[2] = keyID;
        }
        else if (newLockedDoorLocation == Vector2Int.left)
        {
            exits[3] = Constants.LOCKED_DOOR;
            lockedDoorsIDs[3] = keyID;
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