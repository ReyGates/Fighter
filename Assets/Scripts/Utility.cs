using UnityEngine;
using System.Collections;

public static class Utility
{
    public static bool CheckInputTouchPosition(float currentTouchPos, float firstTouchPos, float resistance)
    {
        return currentTouchPos > (firstTouchPos + resistance) || currentTouchPos < (firstTouchPos - resistance);
    }
}
