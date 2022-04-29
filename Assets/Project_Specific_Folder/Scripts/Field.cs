using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public GameObject PlayerGate;
    public GameObject EnemyGate;
    public GameObject PlayerFench;
    public GameObject EnemyFench;
    void Start()
    {
        PlayerFench.GetComponent<MeshRenderer>().material.color = GameManager.Instance.PlayerColor;
        PlayerGate.GetComponent<MeshRenderer>().material.color = GameManager.Instance.PlayerColor;
        EnemyGate.GetComponent<MeshRenderer>().material.color = GameManager.Instance.EnemyColor;
        EnemyFench.GetComponent<MeshRenderer>().material.color = GameManager.Instance.EnemyColor;
    }

}
