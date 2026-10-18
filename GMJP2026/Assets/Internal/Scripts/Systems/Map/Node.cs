using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    UnityEngine.UI.Button button;
    TMPro.TextMeshProUGUI levelIndicator;

    [SerializeField] bool m_startNode;
    public bool m_StartNode => m_startNode;
    public List<Node> m_forwardConnections;

    public int m_NodeLevel;

    public bool IsLastNode => m_forwardConnections.Count <= 0;

    private void OnEnable()
    {
        if (levelIndicator == null)
        {
            levelIndicator = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        }

        if (button == null)
        {
            button = GetComponent<UnityEngine.UI.Button>();
        }
        button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        if (button == null)
        {
            button = GetComponent<UnityEngine.UI.Button>();
        }
        if (button == null) return;
        button.onClick.RemoveListener(OnClick);
    }

    private void Update()
    {
        if (levelIndicator == null) return;
        levelIndicator.text = $"{m_NodeLevel}";
    }

    public int GetNodeIdx()
    {
        for (int i = 0; i < transform.parent.childCount; ++i)
        {
            if (transform.parent.GetChild(i) == transform)
            {
                return i;
            }
        }

        return -1;
    }

    public Vector3 GetWorldPosition()
    {
        return transform.position;
        RectTransform t = transform as RectTransform;

        Vector3 r = Camera.main.ViewportToWorldPoint(t.position);
        r.z = 0;
        return r;
    }

    public void OnClick()
    {
        MapController.Instance.TryTravel(this);
    }
}

public static class NodeObserver
{

}