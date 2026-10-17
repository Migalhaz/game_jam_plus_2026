using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouthHealthComponent : HealthComponent, ISetupHealthComponent
{
    [SerializeField] private int m_startHealth;
    [SerializeField] private ToothHealthComponent m_toothHealthComponent;
    private List<ToothHealthComponent> m_toothHealthComponents;

    private void OnEnable()
    {
        HealthComponentObserver.m_OnDie += OnToothDies;
    }

    private void OnDisable()
    {
        HealthComponentObserver.m_OnDie -= OnToothDies;
    }

    public void SetupHealth()
    {
        m_currentHealth = m_startHealth;
        m_toothHealthComponents = new List<ToothHealthComponent>(m_currentHealth);
        for (int i = 0; i < m_currentHealth; ++i)
        {
            ToothHealthComponent newToothHc = Instantiate<ToothHealthComponent>(m_toothHealthComponent, transform);
            m_toothHealthComponents.Add(newToothHc);
        }
    }

    private void OnToothDies(HealthComponent hc)
    {
        if (!(hc is ToothHealthComponent thc)) return;

        if (!m_toothHealthComponents.Contains(thc)) return;
        m_toothHealthComponents.Remove(thc);
        Destroy(thc.gameObject);
        Hit(1);
    }
}
