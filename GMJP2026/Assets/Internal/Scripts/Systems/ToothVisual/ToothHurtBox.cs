using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothHurtBox : MonoBehaviour
{
    [SerializeField] ToothHealthComponent m_thc;
    public ToothHealthComponent THC => m_thc;

    private void OnMouseEnter()
    {
        if (DeckComponent.Instance.SelectedCard == null) return;
        if (DeckComponent.Instance.SelectedCard.CardData == null) return;
        ToothHurtBoxObserver.SelectHurtBox(this);
    }

    private void OnMouseDown()
    {
        if (DeckComponent.Instance.SelectedCard == null) return;
        if (DeckComponent.Instance.SelectedCard.CardData == null) return;
        ToothHurtBoxObserver.ClickHurtBox(this);
    }
}

public static class ToothHurtBoxObserver
{
    public static System.Action<ToothHurtBox> m_OnSelectHurtBox = null;
    public static System.Action<ToothHurtBox> m_OnClickHurtBox = null;
    public static void SelectHurtBox(ToothHurtBox toothHurtBox) => m_OnSelectHurtBox?.Invoke(toothHurtBox);
    public static void ClickHurtBox(ToothHurtBox toothHurtBox) => m_OnClickHurtBox?.Invoke(toothHurtBox);
}