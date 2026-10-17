using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class HealthComponent : MonoBehaviour
{
    [SerializeField, Min(1)] private int m_health = 10;

    public int CurrentHealth => m_health;

    public virtual void Hit(int damage)
    {
        int healthBefore = m_health;
        m_health -= damage;
        m_health = Mathf.Max(m_health, 0);
        HealthComponentObserver.OnHit(this, damage);
        HealthComponentObserver.OnHealthChanged(this, healthBefore, m_health);

        if (m_health <= 0)
        {
            Die();
        }
    }

    public virtual void Heal(int heal)
    {
        int healthBefore = m_health;
        m_health += heal;
        HealthComponentObserver.OnHeal(this, heal);
        HealthComponentObserver.OnHealthChanged(this, healthBefore, m_health);
    }

    protected virtual void Die()
    {
        HealthComponentObserver.OnDie(this);
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