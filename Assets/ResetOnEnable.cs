using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_InputField))]
public class ResetOnEnable : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<TMP_InputField>().text = "";
    }
}
