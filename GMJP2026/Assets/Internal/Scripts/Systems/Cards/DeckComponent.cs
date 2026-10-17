using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckComponent : MigalhaSystem.Singleton.Singleton<DeckComponent>
{
    [SerializeField, Min(1)] private int m_startCards = 1;
    [SerializeField, Min(1)] private int m_maxCards = 1;
    [SerializeField] private CardComponent m_cardPrefab;
    [SerializeField] List<CardData> m_allDatas;
   
    private List<CardData> m_cardPool;


    CardComponent m_selectedCard;
    public CardComponent SelectedCard => m_selectedCard;

    private void Start()
    {
        SetupPool();
    }

    void SetupPool()
    {
        const int BLOCK_REPEATS = 6;
        m_cardPool = new List<CardData>(m_allDatas.Count * BLOCK_REPEATS);
        foreach (CardData c in m_allDatas)
        {
            for (int i = 0; i < BLOCK_REPEATS; ++i)
            {
                m_cardPool.Add(c);
            }
        }
    }

    public void SelectCard(CardComponent newCard)
    {
        m_selectedCard = newCard;
    }

    public void UseCard()
    {
        Destroy(m_selectedCard.gameObject);
        SelectCard(null);
    }

    public void GenerateCard()
    {

    }
}
