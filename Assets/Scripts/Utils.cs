using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class Utils
{
    public static bool HasTimeElapsed(float time, float targetTime)
    {
        return targetTime > time;
    }
}
