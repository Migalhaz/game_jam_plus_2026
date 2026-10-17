using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardEffectData : ScriptableObject
{
    [SerializeField] private Sprite m_cardBackground;
    public Sprite CardBackground => m_cardBackground;

    public abstract void ApplyEffect(ToothHealthComponent thc);

    public abstract bool CanApplyEffect(ToothHealthComponent thc);
}
