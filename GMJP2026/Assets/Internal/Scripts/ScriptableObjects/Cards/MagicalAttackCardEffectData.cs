using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DA_AttackEffectData", menuName = "Data/Card/Effect/Magic Attack")]
public class MagicalAttackCardEffectData : CardEffectData
{
    public override void ApplyEffect(ToothHealthComponent thc)
    {
        if (!CanApplyEffect(thc)) return;
        thc.Hit(1);
    }

    public override bool CanApplyEffect(ToothHealthComponent thc) => thc.m_DamageFrom == ToothDamageFrom.MagixEffect;
}
