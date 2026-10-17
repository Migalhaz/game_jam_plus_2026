using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouthHealthComponent : HealthComponent, ISetupHealthComponent
{
    // indice = linha * total_colunas + coluna
    [SerializeField] private ToothHealthComponent m_toothHealthComponent;
    [SerializeField] private int m_rows = 5;
    [SerializeField] private int m_columns = 5;
    [SerializeField, Min(0)] private float m_spacing;
    [SerializeField] Transform m_toothPivot;
    [SerializeField] Vector2 m_startRange;
    private List<List<ToothHealthComponent>> m_toothHealthComponents;

    public int m_Rows => m_rows;
    public int m_Columns => m_columns;

    private void OnEnable()
    {
        HealthComponentObserver.m_OnDie += OnToothDies;
        ToothHurtBoxObserver.m_OnSelectHurtBox += OnSelectTooth;
        TurnManagerObserver.m_OnStartTurn += OnStartTurn;
    }

    private void OnDisable()
    {
        HealthComponentObserver.m_OnDie -= OnToothDies;
        ToothHurtBoxObserver.m_OnSelectHurtBox -= OnSelectTooth;
        TurnManagerObserver.m_OnStartTurn -= OnStartTurn;
    }

    public void SetupHealth()
    {
        m_currentHealth = m_rows * m_columns;
        m_toothHealthComponents = new List<List<ToothHealthComponent>>(m_rows);


        for (int y = 0; y < m_rows; ++y)
        {
            m_toothHealthComponents.Add(new List<ToothHealthComponent>(m_columns));
            for (int x = 0; x < m_columns; ++x)
            {
                ToothHealthComponent newToothHc = Instantiate<ToothHealthComponent>(m_toothHealthComponent, m_toothPivot);
                newToothHc.transform.position = m_toothPivot.position + new Vector3(x * m_spacing, -(y * m_spacing), 0);
                m_toothHealthComponents[y].Add(newToothHc);
            }
        }
    }

    private void OnToothDies(HealthComponent hc)
    {
        if (!(hc is ToothHealthComponent thc)) return;

        if (!CheckTooth(thc)) return;
        
        Hit(1);
    }

    private void OnStartTurn(int ct)
    {
        int teethCount = Random.Range((int)(m_startRange.x + (ct - 1d)), (int)(m_startRange.y + ct));
        teethCount = Mathf.Min(m_currentHealth, teethCount);

        List<Vector2> validIdx = new List<Vector2>(CurrentHealth);

        for (int y = 0; y < m_rows; ++y)
        {
            for (int x = 0; x < m_columns; ++x)
            {
                if (m_toothHealthComponents[y][x].IsAlive)
                {
                    validIdx.Add(new Vector2(x, y));
                }
            }
        }

        for (int i = 0; i < teethCount; ++i)
        {
            if (validIdx.Count <= 0) break;
            int idx = Random.Range(0, validIdx.Count);
            Vector2 randomIdx = validIdx[idx];
            validIdx.RemoveAt(idx);

            m_toothHealthComponents[(int)randomIdx.y][(int)randomIdx.x].ChangeAttackMode(true);
        }

    }

    protected override void Die()
    {
        base.Die();
        SceneController.Instance.LoadScene(2);
    }

    private bool CheckTooth(ToothHealthComponent thc)
    {
        foreach (List<ToothHealthComponent> list in m_toothHealthComponents)
        {
            if (list.Contains(thc)) return true;
        }
        return false;
    }

    void OnSelectTooth(ToothHurtBox thb)
    {
        CardComponent selectCard = DeckComponent.Instance.SelectedCard;
        CardData card = selectCard.CardData;
        int indexRow = m_toothHealthComponents.FindIndex(x => x.Contains(thb.THC));
        int indexColumn = m_toothHealthComponents[indexRow].IndexOf(thb.THC);

        foreach (Vector2 p in card.Positions)
        {
            int currentX = (int)(indexColumn + p.x);
            int currentY = (int)(indexRow - p.y);

            if (!(currentX >= 0 && currentX < m_columns && currentY >= 0 && currentY < m_rows)) continue;


            m_toothHealthComponents[currentY][currentX].Hit(1);
        }

        DeckComponent.Instance.UseCard();
    }

}
