using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothHealthComponent : LoadDataHealthComponent
{
    [SerializeField] private bool m_hit;

    [SerializeField] private int m_damagePerHit = 1;
    public int m_DamagePerHit => m_damagePerHit;

    private void Update()
    {
        ToothHit();
    }
    public void ToothHit() {
    if (m_hit)
        {
            Hit(m_damagePerHit);
            m_hit = false;
        }
    }
}
