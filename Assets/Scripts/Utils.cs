using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    public const int UP_DOOR = 2;
    public const int RIGHT_DOOR = 1;
    public const int DOWN_DOOR = -2;
    public const int LEFT_DOOR = -1;

    public const int NO_EXIT = 0;
    public const int HOLE = 3;
    public const int LOCKED_DOOR = 4;
}
public static class HomeMadeFunctions
{
    public static T GetRandom<T>(IList<T> list)
    {
        return list[CastleGenerator.random.Next(list.Count)];
    }
    /// <summary>
    /// map value like the arduino function(https://www.arduino.cc/reference/en/language/functions/math/map/)
    /// </summary>
    public static float Map(float from, float to, float from2, float to2, float value)
    {
        if (value <= from2)
        {
            return from;
        }
        else if (value >= to2)
        {
            return to;
        }
        else
        {
            return (to - from) * ((value - from2) / (to2 - from2)) + from;
        }
    }
}