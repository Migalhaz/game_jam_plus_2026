using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardEffectData : ScriptableObject
{
    public abstract void ApplyEffect(ToothHealthComponent thc);

    public abstract bool CanApplyEffect(ToothHealthComponent thc);
}
