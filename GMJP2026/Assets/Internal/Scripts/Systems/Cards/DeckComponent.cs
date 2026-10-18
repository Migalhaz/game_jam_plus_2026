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
    [SerializeField] List<CardEffectData> m_allEffectDatas;

    [SerializeField] UnityEngine.UI.HorizontalLayoutGroup m_hlg;
    [SerializeField] UnityEngine.UI.Image m_nextCardBackground;
    [SerializeField] UnityEngine.UI.Image m_nextCardBlock;

    private int nextCardIndex;

    private List<CardData> m_cardPool;
    private List<CardEffectData> m_effectPool;


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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
        {
            SelectCard(null);
        }
    }

    void SetupPool()
    {
        nextCardIndex = 0;
        SetupDataPool();
        SetupEffectPool();
    }

    void SetupDataPool()
    {
        if (m_allDatas.Count <= 0) return;
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
            int randomIndex = Random.Range(i + 1, m_cardPool.Count);
            CardData other = m_cardPool[randomIndex];
            m_cardPool[randomIndex] = m_cardPool[i];
            m_cardPool[i] = other;
        }
    }

    void SetupEffectPool()
    {
        if (m_allEffectDatas.Count <= 0) return;
        const int EFFECT_REPEATS = 2;

        m_effectPool = new List<CardEffectData>(m_allEffectDatas.Count * EFFECT_REPEATS);

        foreach (CardEffectData c in m_allEffectDatas)
        {
            for (int i = 0; i < EFFECT_REPEATS; ++i)
            {
                m_effectPool.Add(c);
            }
        }

        for (int i = 0; i < m_effectPool.Count - 2; ++i)
        {
            int randomIndex = Random.Range(i + 1, m_effectPool.Count);
            CardEffectData other = m_effectPool[randomIndex];
            m_effectPool[randomIndex] = m_effectPool[i];
            m_effectPool[i] = other;
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

        CardData data = m_cardPool[nextCardIndex % m_cardPool.Count];
        CardEffectData effectData = m_effectPool[nextCardIndex % m_effectPool.Count];
        component.SetupCardComponent(data, effectData);

        ++nextCardIndex;
        ++m_currentCards;

        data = m_cardPool[nextCardIndex % m_cardPool.Count];
        effectData = m_effectPool[nextCardIndex % m_effectPool.Count];
        m_nextCardBackground.sprite = effectData.CardBackground;
        m_nextCardBlock.sprite = data.BlockSprite;
    }
}
