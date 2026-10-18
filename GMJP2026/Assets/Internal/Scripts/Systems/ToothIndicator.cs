using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothIndicator : MonoBehaviour
{
    [SerializeField] PlayerHealthData data;
    [SerializeField] TMPro.TextMeshProUGUI t;

    private void Start()
    {
        t.text = $"{data.m_StartHealth}";
    }
}
