using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardComponent : MonoBehaviour
{
    [SerializeField] CardData m_cardData;
    public CardData CardData => m_cardData;

    public void Select()
    {
        DeckComponent.Instance.SelectCard(this);
    }

    public void SetCardData(CardData cardData)
    {
        m_cardData = cardData;
    }
}
