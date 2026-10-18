using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button m_playButton;
    
    
    void Start()
    {
        if (m_playButton != null) {
            m_playButton.onClick.AddListener(OnPlayButtonPress);
        }
    }

    public void OnPlayButtonPress() {
        SceneController.Instance.LoadScene(3);
    }
}
