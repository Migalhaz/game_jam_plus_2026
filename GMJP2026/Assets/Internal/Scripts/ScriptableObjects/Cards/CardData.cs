using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DA_NewCard", menuName = "Data/Card/Block")]
public class CardData : ScriptableObject
{
    [SerializeField] Sprite m_blockSprite;
    [SerializeField] List<Vector2> m_positions;
    public List<Vector2> Positions => m_positions;
    public Sprite BlockSprite => m_blockSprite;
}
