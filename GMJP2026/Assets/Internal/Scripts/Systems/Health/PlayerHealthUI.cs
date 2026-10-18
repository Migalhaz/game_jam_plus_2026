using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI txt;
    [SerializeField] PlayerHealthComponent phc;
    [SerializeField] TMPro.TextMeshProUGUI cardIndicatorTxt;

    // Update is called once per frame
    void Update()
    {
        txt.text = $"{phc.CurrentHealth}";

        if (DeckComponent.Instance == null) return;
        cardIndicatorTxt.text = $"{DeckComponent.Instance.CurrentCard}/{DeckComponent.Instance.MaxCards}";
    }
}
