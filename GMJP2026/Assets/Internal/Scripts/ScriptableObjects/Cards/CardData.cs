using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DA_NewCard", menuName = "Data/Card/Block")]
public class CardData : ScriptableObject
{
    [SerializeField] List<Vector2> m_positions;
    public List<Vector2> Positions => m_positions;
}
