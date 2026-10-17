using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DA_NewDamage", menuName = "Data/Health/Basic Damage Data")]
public class DamageData : ScriptableObject
{
    [SerializeField, Min(1)] protected int m_damageQuantity = 1;
    public int m_DamageQuantity => m_damageQuantity;
}