using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothVisual : MonoBehaviour
{
    [SerializeField] ToothHealthComponent m_thc;
    [SerializeField] SpriteRenderer m_spriteRenderer;

    private void OnEnable()
    {
        HealthComponentObserver.m_OnDie += OnDie;

    }

    private void OnDisable()
    {
        HealthComponentObserver.m_OnDie -= OnDie;
    }

    private void Update()
    {
        if (m_thc.IsAlive)
        {
            if (m_thc.AttackMode)
            {
                m_spriteRenderer.color = Color.red;
            }
            else
            {
                m_spriteRenderer.color = Color.white;
            }
        }

    }

    void OnDie(HealthComponent hc)
    {
        if (!(hc is ToothHealthComponent thc)) return;
        if (thc != m_thc) return;

        m_spriteRenderer.color = Color.black;
    }
}
