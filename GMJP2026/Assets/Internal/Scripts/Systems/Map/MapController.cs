using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class MapController : MigalhaSystem.Singleton.Singleton<MapController>
{
    [SerializeField] bool m_regenMap;
    [SerializeField] MapData data;
    [SerializeField] GameObject m_currentMap;
    [SerializeField] List<GameObject> m_allMaps;

    [SerializeField] Vector2 m_levelRange;

    [SerializeField] GameObject m_playerIndicator;
    [SerializeField] float m_speed;
    bool m_traveling;
    Node currentNodeTarget;
    [SerializeField] float pato;

    void Start()
    {
        foreach(GameObject obj in m_allMaps)
        {
            obj.SetActive(false);
        }
        m_traveling = false;
        if (m_regenMap || data.m_currentMapIdx == -1 || data.shouldReset)
        {
            GenerateMap();
            return;
        }
        LoadMap();
    }

    private void Update()
    {
        if (m_traveling)
        {
            m_playerIndicator.transform.position = Vector3.Lerp(m_playerIndicator.transform.position, currentNodeTarget.GetWorldPosition(), m_speed * Time.deltaTime);

            if (Vector3.Distance(m_playerIndicator.transform.position, currentNodeTarget.GetWorldPosition()) <= pato)
            {
                m_playerIndicator.transform.position = currentNodeTarget.GetWorldPosition();
                data.currentNodeIdx = currentNodeTarget.GetNodeIdx();
                data.shouldReset = currentNodeTarget.IsLastNode;
                currentNodeTarget = null;
                m_traveling = false;

                SceneController.Instance.LoadScene(4);
            }
        }
    }

    void GenerateMap()
    {
        if (data == null) return;

        data.m_currentMapIdx = Random.Range(0, m_allMaps.Count);
        m_currentMap = m_allMaps[data.m_currentMapIdx];
        m_currentMap.SetActive(true);
        if (data.m_nodeLevels == null)
        {
            data.m_nodeLevels = new();
        }
        else
        {
            data.m_nodeLevels.Clear();
        }
        data.shouldReset = false;
        Node[] mapNodes = m_currentMap.GetComponentsInChildren<Node>();

        foreach (Node n in mapNodes)
        {
            int nodeIdx = n.GetNodeIdx();
            if (n.m_StartNode)
            {
                data.currentNodeIdx = nodeIdx;
                m_playerIndicator.transform.position = n.GetWorldPosition();
            }

            int nodeLevel = Random.Range((int)m_levelRange.x, (int)m_levelRange.y + 1);
            data.m_nodeLevels.Add(nodeIdx, nodeLevel);
            n.m_NodeLevel = nodeLevel;
        }
    }

    void LoadMap()
    {
        m_currentMap = m_allMaps[data.m_currentMapIdx];
        m_currentMap.SetActive(true);

        Node[] mapNodes = m_currentMap.GetComponentsInChildren<Node>();

        foreach (Node n in mapNodes)
        {
            int nodeIdx = n.GetNodeIdx();
            n.m_NodeLevel = data.m_nodeLevels[nodeIdx];
            if (nodeIdx == data.currentNodeIdx)
            {
                m_playerIndicator.transform.position = n.GetWorldPosition();
            }
        }
    }

    public void TryTravel(Node n)
    {
        if (m_traveling) return;

        Node[] mapNodes = m_currentMap.GetComponentsInChildren<Node>();
        Node currentNode = mapNodes.FirstOrDefault(x => x.GetNodeIdx() == data.currentNodeIdx);

        if (!currentNode.m_forwardConnections.Contains(n)) return;

        m_traveling = true;
        currentNodeTarget = n;
    }
}


