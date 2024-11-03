using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public static class GameEvents
{
    public static event Action<string,string> GameEvent;

    public static void CallGameEvent(string key,string value)
    {
        GameEvent?.Invoke(key,value);
    }
}