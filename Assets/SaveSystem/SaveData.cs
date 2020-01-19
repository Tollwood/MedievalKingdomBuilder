using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    private static SaveData _current;
    public static SaveData current
    {
        get
        {

            if (_current == null)
            {
                _current = new SaveData();
            }

            return _current;
        }
    }

    public string familyName;
    public bool gameStarted = false;

    internal static void Reset()
    {
        _current = new SaveData();
    }

    internal static void Update(SaveData saveData)
    {
        _current = saveData;
    }
}
