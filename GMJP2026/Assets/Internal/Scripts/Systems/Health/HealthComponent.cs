using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISetupHealthComponent
{
    public void SetupHealth();
}

[DisallowMultipleComponent]
public class HealthComponent : MonoBehaviour
{
    protected int m_currentHealth;

    public int CurrentHealth => m_currentHealth;
    public bool IsAlive => m_currentHealth > 0;

    protected virtual void Awake()
    {
        if (this is ISetupHealthComponent hc)
        {
            hc.SetupHealth();
        }
    }

    public virtual void Hit(int damage)
    {
        if (!IsAlive) return;
        int healthBefore = m_currentHealth;
        m_currentHealth -= damage;
        m_currentHealth = Mathf.Max(m_currentHealth, 0);
        HealthComponentObserver.OnHit(this, damage);
        HealthComponentObserver.OnHealthChanged(this, healthBefore, m_currentHealth);

        if (m_currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Heal(int heal)
    {
        if (!IsAlive) return;
        int healthBefore = m_currentHealth;
        m_currentHealth += heal;
        HealthComponentObserver.OnHeal(this, heal);
        HealthComponentObserver.OnHealthChanged(this, healthBefore, m_currentHealth);
    }

    protected virtual void Die()
    {
        HealthComponentObserver.OnDie(this);
    }
}

public class StartHealthComponent : HealthComponent, ISetupHealthComponent
{
    [SerializeField, Min(1)] int m_startHealth = 1;

    public void SetupHealth()
    {
        m_currentHealth = m_startHealth;
    }
}

public abstract class LoadDataHealthComponent : HealthComponent, ISetupHealthComponent
{
    [SerializeField] private HealthData m_healthData;
    public void SetupHealth()
    {
        m_currentHealth = m_healthData.m_StartHealth;
    }
}


public static class HealthComponentObserver
{
    public static System.Action<HealthComponent, int> m_OnHit = null;
    public static System.Action<HealthComponent, int> m_OnHeal = null;
    public static System.Action<HealthComponent, int, int> m_OnHealthChanged = null;
    public static System.Action<HealthComponent> m_OnDie = null;

    public static void OnHit(HealthComponent hc, int damage) => m_OnHit?.Invoke(hc, damage);
    public static void OnHeal(HealthComponent hc, int heal) => m_OnHeal?.Invoke(hc, heal);
    public static void OnHealthChanged(HealthComponent hc, int healthBefore, int currentHealth) => m_OnHealthChanged?.Invoke(hc, healthBefore, currentHealth);
    public static void OnDie(HealthComponent hc) => m_OnDie?.Invoke(hc);
}