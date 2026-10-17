using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DA_AttackEffectData", menuName = "Data/Card/Effect/Attack")]
public class AttackCardEffectData : CardEffectData
{
    public override void ApplyEffect(ToothHealthComponent thc)
    {
        thc.Hit(1);
    }

    public override bool CanApplyEffect(ToothHealthComponent thc) => true;
}
