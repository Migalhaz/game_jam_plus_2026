using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI txt;
    [SerializeField] PlayerHealthComponent phc;

    // Update is called once per frame
    void Update()
    {
        txt.text = $"{phc.CurrentHealth}";
    }
}
