using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public GameObject PlayerGate;
    public GameObject EnemyGate;
    public GameObject PlayerFench;
    public GameObject EnemyFench;

    Vector3 m_PGateStartPos, m_EGateStartPos;
    void Start()
    {
        m_PGateStartPos = PlayerGate.transform.position;
        m_EGateStartPos = EnemyGate.transform.position;

        PlayerFench.GetComponent<MeshRenderer>().material.color = GameManager.Instance.PlayerColor;
        PlayerGate.GetComponent<MeshRenderer>().material.color = GameManager.Instance.PlayerColor;
        EnemyGate.GetComponent<MeshRenderer>().material.color = GameManager.Instance.EnemyColor;
        EnemyFench.GetComponent<MeshRenderer>().material.color = GameManager.Instance.EnemyColor;
    }

    public void SwapGates()
    {
       
        EnemyGate.transform.position = m_PGateStartPos;
        PlayerGate.transform.position = m_EGateStartPos; 
        GameManager.Instance.Gate = EnemyGate;
    }

}
