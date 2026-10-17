using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DA_DefenseEffectData", menuName = "Data/Card/Effect/Defense")]
public class DefenseCardEffectData : CardEffectData
{
    public override void ApplyEffect(ToothHealthComponent thc)
    {
        thc.ChangeAttackMode(false);
    }

    public override bool CanApplyEffect(ToothHealthComponent thc) => true;
}
