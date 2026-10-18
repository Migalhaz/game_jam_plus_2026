using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DA_AttackEffectData", menuName = "Data/Card/Effect/Physical Attack")]
public class PhysicalAttackCardEffectData : CardEffectData
{
    public override void ApplyEffect(ToothHealthComponent thc)
    {
        if (!CanApplyEffect(thc)) return;
        thc.Hit(1);
    }

    public override bool CanApplyEffect(ToothHealthComponent thc) => thc.m_DamageFrom == ToothDamageFrom.PhysxEffect;
}
