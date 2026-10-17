using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothHurtBox : MonoBehaviour
{
    [SerializeField] ToothHealthComponent m_thc;
    public ToothHealthComponent THC => m_thc;

    private void OnMouseDown()
    {
        if (DeckComponent.Instance.SelectedCard == null) return;
        if (DeckComponent.Instance.SelectedCard.CardData == null) return;
        if (!m_thc.IsAlive) return;
        ToothHurtBoxObserver.SelectHurtBox(this);
    }
}

public static class ToothHurtBoxObserver
{
    public static System.Action<ToothHurtBox> m_OnSelectHurtBox = null;
    public static void SelectHurtBox(ToothHurtBox toothHurtBox) => m_OnSelectHurtBox?.Invoke(toothHurtBox);
}