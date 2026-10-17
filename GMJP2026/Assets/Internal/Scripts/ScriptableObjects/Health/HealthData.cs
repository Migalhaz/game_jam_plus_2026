using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DA_NewHealth", menuName = "Data/Health/Basic Health Data")]
public class HealthData : ScriptableObject
{
    [SerializeField, Min(1)] protected int m_startHealth = 1;
    public int m_StartHealth => m_startHealth;
}