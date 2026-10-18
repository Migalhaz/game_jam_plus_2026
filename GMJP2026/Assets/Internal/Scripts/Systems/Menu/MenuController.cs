using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button m_playButton;
    [SerializeField] private int m_startPlayerHealth = 10;
    [SerializeField] private PlayerHealthData m_playerHealthData;
    [SerializeField] private MapData m_mapData;

    void Start()
    {
        if (m_playButton != null) {
            m_playButton.onClick.AddListener(OnPlayButtonPress);
        }
    }

    public void OnPlayButtonPress() {
        m_playerHealthData.UpdateStartHealth(m_startPlayerHealth);
        m_mapData.m_currentMapIdx = -1;
        SceneController.Instance.LoadScene(2);
    }
}
