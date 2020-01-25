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
    public GameState prevGameState = GameState.NEW_GAME;
    public GameState gameState = GameState.NEW_GAME;
    internal Vector3 cameraRigPosition = Vector3.zero;
    internal Quaternion cameraRigRotation = Quaternion.identity;
    internal Vector3 zoom = new Vector3(0, 20, -20);

    internal static void Reset()
    {
        _current = new SaveData();
    }

    internal static void Update(SaveData saveData)
    {
        _current = saveData;
    }
}
