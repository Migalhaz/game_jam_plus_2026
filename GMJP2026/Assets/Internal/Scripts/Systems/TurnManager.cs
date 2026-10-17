using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private int m_currentTurn;
    private void Start()
    {
        m_currentTurn = 0;
        StartTurn();
    }

    void StartTurn()
    {
        m_currentTurn++;
        TurnManagerObserver.StartTurn(m_currentTurn);
    }

    public void FinishTurn()
    {
        TurnManagerObserver.FinishTurn();
        StartTurn();
    }
}

public static class TurnManagerObserver
{
    public static System.Action<int> m_OnStartTurn = null;
    public static System.Action m_OnFinishTurn = null;
 

    public static void StartTurn(int ct) => m_OnStartTurn?.Invoke(ct);
    public static void FinishTurn() => m_OnFinishTurn?.Invoke();
}