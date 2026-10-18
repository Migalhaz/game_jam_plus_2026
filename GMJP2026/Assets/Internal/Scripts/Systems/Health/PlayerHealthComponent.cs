using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthComponent : LoadDataHealthComponent
{
    private void OnEnable()
    {
        HealthComponentObserver.m_OnDie += OnToothDies;
        HealthComponentObserver.m_OnDie += OnMouthDies;
        PlayerHealthComponentObserver.m_OnRequestPlayerDamage += TryHitPlayer;
    }

    private void OnDisable()
    {
        HealthComponentObserver.m_OnDie -= OnToothDies;
        HealthComponentObserver.m_OnDie -= OnMouthDies;
        PlayerHealthComponentObserver.m_OnRequestPlayerDamage -= TryHitPlayer;
    }

    bool TryHitPlayer(int damage)
    {
        if (!IsAlive) return false;
        Hit(damage);
        return true;
    }

    private void OnToothDies(HealthComponent hc)
    {
        if (!(hc is ToothHealthComponent)) return;

        Heal(1);
    }

    void OnMouthDies(HealthComponent hc)
    {
        if (!(hc is MouthHealthComponent)) return;
        if (m_healthData is PlayerHealthData pd)
        {
            pd.UpdateStartHealth(m_currentHealth);
        }
    }

    protected override void Die()
    {
        base.Die();
        SceneController.Instance.LoadScene(1);
    }
}

public static class PlayerHealthComponentObserver
{
    public static System.Func<int, bool> m_OnRequestPlayerDamage = null;

    public static bool RequestPlayerDamage(int damage) => m_OnRequestPlayerDamage?.Invoke(damage) ?? false;
}