using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MigalhaSystem.Singleton;

public class SceneController : Singleton<SceneController>
{
    [SerializeField] private bool m_loadStartScene;
    [SerializeField, Min(1)] private int m_startScene = 1;

    private void Start()
    {
        if (m_loadStartScene)
        {
            LoadScene(m_startScene);
        }
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
