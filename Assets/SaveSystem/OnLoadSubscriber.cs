using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OnLoadSubscriber
{
    private static OnLoadSubscriber _current;
    public static OnLoadSubscriber current
    {
        get
        {

            if (_current == null)
            {
                _current = new OnLoadSubscriber();
            }

            return _current;
        }
    }


    private List<OnLoadSupport> onLoadEvents = new List<OnLoadSupport>();

    public void AddOnLoadSubscriber(OnLoadSupport newOnLoad)
    {
        onLoadEvents.Add(newOnLoad);
    }

    public void RemoveOnLoadSubscriber(OnLoadSupport newOnLoad)
    {
        onLoadEvents.Remove(newOnLoad);
    }

    public void TriggerLoad()
    {
        foreach(OnLoadSupport onLoad in onLoadEvents)
        {
            onLoad.OnLoad();
        }
    }
}
