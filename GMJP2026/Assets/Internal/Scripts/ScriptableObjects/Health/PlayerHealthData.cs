using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DA_NewHealth", menuName = "Data/Health/Player Health Data")]
public class PlayerHealthData : HealthData
{
    public void UpdateStartHealth(int newHealth)
    {
        m_startHealth = newHealth;
    }
}
