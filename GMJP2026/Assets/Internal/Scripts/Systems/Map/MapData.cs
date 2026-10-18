using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/MapData")]
public class MapData : ScriptableObject
{
    public GameObject m_currentMap;

    public Dictionary<int, int> m_nodeLevels;
    public int currentNodeIdx;
    public bool shouldReset;
}
