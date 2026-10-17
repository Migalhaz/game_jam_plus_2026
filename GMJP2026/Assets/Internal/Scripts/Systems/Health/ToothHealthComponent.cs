using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothHealthComponent : LoadDataHealthComponent
{
    private bool m_attackMode;
    public bool AttackMode => m_attackMode;

    private void OnEnable()
    {
        TurnManagerObserver.m_OnFinishTurn += OnFinishTurn;
    }

    private void OnDisable()
    {
        TurnManagerObserver.m_OnFinishTurn -= OnFinishTurn;
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


    protected override void Awake()
    {
        base.Awake();
        ChangeAttackMode(false);
    }
}