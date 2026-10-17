using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthComponent : LoadDataHealthComponent
{
    private void OnEnable()
    {
        HealthComponentObserver.m_OnDie += OnToothDies;
        PlayerHealthComponentObserver.m_OnRequestPlayerDamage += TryHitPlayer;
    }

    private void OnDisable()
    {
        HealthComponentObserver.m_OnDie -= OnToothDies;
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
        if (!(hc is ToothHealthComponent thc)) return;

        Heal(1);
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