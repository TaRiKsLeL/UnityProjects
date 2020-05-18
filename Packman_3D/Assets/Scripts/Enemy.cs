using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy: MonoBehaviour
{
    private NavMeshAgent meshAgent;

    void Start()
    {
        meshAgent = GetComponent<NavMeshAgent>();
    }

    void FixedUpdate()
    {
        meshAgent.SetDestination(FindObjectOfType<Pacman>().transform.position);
    }
}
