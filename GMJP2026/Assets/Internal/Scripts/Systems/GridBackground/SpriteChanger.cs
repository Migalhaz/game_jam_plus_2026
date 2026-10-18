using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    [SerializeField] MouthHealthComponent mhc;
    [SerializeField] private SpriteRenderer mySpriteRenderer;
    [SerializeField] private Sprite newSprite; 
    
    private int m_rowNumber;

    void Update()
    {
        if (mhc.m_CurrentColumn >= 7)
        {
            mySpriteRenderer.sprite = newSprite;
        }
    }
}