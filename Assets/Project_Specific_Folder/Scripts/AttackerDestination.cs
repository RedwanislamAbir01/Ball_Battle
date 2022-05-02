using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AttackerDestination : MonoBehaviour
{
    NavMeshAgent myNavMeshAgent;
    public Transform Gate;
    void Start()
    {
        myNavMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        SetDestination();
    }

    void  SetDestination()
    {
        myNavMeshAgent.SetDestination(Gate.position);
    }
}
