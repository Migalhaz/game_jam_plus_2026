using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothHealthComponent : LoadDataHealthComponent
{
    [SerializeField] private bool m_hit;

    private void Update()
    {
        if (m_hit)
        {
            Hit(1);
            m_hit = false;
        }
    }
}
