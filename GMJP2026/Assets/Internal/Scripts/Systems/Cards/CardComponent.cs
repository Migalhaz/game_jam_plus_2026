using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardComponent : MonoBehaviour
{
    [SerializeField] CardData m_cardData;
    [SerializeField] CardEffectData m_effectData;

    public CardData CardData => m_cardData;
    public CardEffectData EffectData => m_effectData;


    public void Select()
    {
        DeckComponent.Instance.SelectCard(this);
    }

    public void SetupCardComponent(CardData cardData, CardEffectData effectData)
    {
        SetCardData(cardData);
        SetEffectData(effectData);
    }


    void SetCardData(CardData cardData)
    {
        m_cardData = cardData;
    }

    void SetEffectData(CardEffectData effectData)
    {
        m_effectData = effectData;
    }
}
