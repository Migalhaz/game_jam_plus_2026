using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardComponent : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Image m_backgroundRenderer;
    [SerializeField] UnityEngine.UI.Image m_blockRenderer;
    [SerializeField] UnityEngine.UI.Image m_selectOverlayRenderer;

    [SerializeField] CardData m_cardData;
    [SerializeField] CardEffectData m_effectData;

    public CardData CardData => m_cardData;
    public CardEffectData EffectData => m_effectData;

    private void Update()
    {
        if (DeckComponent.Instance == null) return;
        if (m_selectOverlayRenderer == null) return;

        m_selectOverlayRenderer.gameObject.SetActive(Active());
    }

    bool Active()
    {
        if (DeckComponent.Instance == null) return false;
        if (DeckComponent.Instance.SelectedCard == null) return false;
        
        return DeckComponent.Instance.SelectedCard == this;
    }

    public void Select()
    {
        DeckComponent.Instance.SelectCard(this);
    }

    public void SetupCardComponent(CardData cardData, CardEffectData effectData)
    {
        SetCardData(cardData);
        SetEffectData(effectData);
        SetupVisual();
    }


    void SetCardData(CardData cardData)
    {
        m_cardData = cardData;
    }

    void SetEffectData(CardEffectData effectData)
    {
        m_effectData = effectData;
    }

    void SetupVisual()
    {
        m_backgroundRenderer.sprite = m_effectData.CardBackground;
        m_blockRenderer.sprite = m_cardData.BlockSprite;
    }
}
