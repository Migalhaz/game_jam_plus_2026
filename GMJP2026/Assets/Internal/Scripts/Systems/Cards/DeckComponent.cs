using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckComponent : MigalhaSystem.Singleton.Singleton<DeckComponent>
{
    [SerializeField, Min(1)] private int m_startCards = 1;
    [SerializeField, Min(1)] private int m_maxCards = 1;

    private int m_currentCards;

    [SerializeField] private CardComponent m_cardPrefab;
    [SerializeField] List<CardData> m_allDatas;
    [SerializeField] UnityEngine.UI.HorizontalLayoutGroup m_hlg;

    private int nextCardIndex;
    private List<CardData> m_cardPool;


    CardComponent m_selectedCard;
    public CardComponent SelectedCard => m_selectedCard;

    private void OnEnable()
    {
        TurnManagerObserver.m_OnFinishTurn += GenerateCard;
    }

    private void OnDisable()
    {
        TurnManagerObserver.m_OnFinishTurn -= GenerateCard;
    }

    private void Start()
    {
        m_currentCards = 0;
        SetupPool();
        SetupDeck();
    }

    void SetupPool()
    {
        if (m_allDatas.Count <= 0) return;
        nextCardIndex = 0;
        const int BLOCK_REPEATS = 3;
        m_cardPool = new List<CardData>(m_allDatas.Count * BLOCK_REPEATS);
        foreach (CardData c in m_allDatas)
        {
            for (int i = 0; i < BLOCK_REPEATS; ++i)
            {
                m_cardPool.Add(c);
            }
        }

        for (int i = 0; i < m_cardPool.Count - 2; ++i)
        {
            int randomIndex = Random.Range(i+1, m_cardPool.Count);
            CardData other = m_cardPool[randomIndex];
            m_cardPool[randomIndex] = m_cardPool[i];
            m_cardPool[i] = other;
        }
    }

    void SetupDeck()
    {
        for (int i = 0; i < m_startCards; ++i)
        {
            GenerateCard();
        }
    }

    public void SelectCard(CardComponent newCard)
    {
        m_selectedCard = newCard;
    }

    public void UseCard()
    {
        --m_currentCards;
        Destroy(m_selectedCard.gameObject);
        SelectCard(null);
    }

    public void GenerateCard()
    {
        if (m_cardPool.Count <= 0) return;
        if (m_currentCards >= m_maxCards) return;

        CardComponent component = Instantiate<CardComponent>(m_cardPrefab, m_hlg.transform);

        component.SetCardData(m_cardPool[nextCardIndex]);

        nextCardIndex = (nextCardIndex + 1) % m_cardPool.Count;
        ++m_currentCards;
    }
}
