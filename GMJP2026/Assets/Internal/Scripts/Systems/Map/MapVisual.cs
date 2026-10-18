using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct VisualMap
{
    public Sprite m_backGround;
    public Sprite m_house;

}

public class MapVisual : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Image m_backGroundImage;
    [SerializeField] List<UnityEngine.UI.Image> m_allHouses;

    public void SetVisual(VisualMap currentMap)
    {
        m_backGroundImage.sprite = currentMap.m_backGround;

        foreach (var h in m_allHouses)
        {
            h.sprite = currentMap.m_house;
        }
    }
}
