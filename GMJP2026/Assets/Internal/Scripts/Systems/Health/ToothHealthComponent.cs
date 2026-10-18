using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ToothDamageFrom
{ 
    PhysxEffect = 0,
    MagixEffect = 1,
}

public class ToothHealthComponent : LoadDataHealthComponent
{
    public ToothDamageFrom m_DamageFrom { get; private set; }
    private bool m_selected;
    private bool m_attackMode;
    public bool Selected => m_selected;
    public bool AttackMode => m_attackMode;

    private void OnEnable()
    {
        TurnManagerObserver.m_OnFinishTurn += OnFinishTurn;
        ToothHurtBoxObserver.m_OnClickHurtBox += ClickHurtBox;
    }

    private void OnDisable()
    {
        TurnManagerObserver.m_OnFinishTurn -= OnFinishTurn;
        ToothHurtBoxObserver.m_OnClickHurtBox -= ClickHurtBox;
    }

    private void OnFinishTurn()
    {
        if (m_attackMode && IsAlive)
        {
            PlayerHealthComponentObserver.RequestPlayerDamage(1);
        }
        ChangeAttackMode(false);
    }

    public void ChangeAttackMode(bool attackModeState)
    {
        m_attackMode = attackModeState;
    }

    public void ChangeSelected(bool selectState)
    {
        m_selected = selectState;
    }

    void ClickHurtBox(ToothHurtBox _)
    {
        ChangeSelected(false);
    }

    protected override void Awake()
    {
        base.Awake();
        ChangeAttackMode(false);
        m_DamageFrom = (ToothDamageFrom)Random.Range(0, 2);
    }
}