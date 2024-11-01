using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class MoveDroneNavMesh : MonoBehaviour
{
    [SerializeField] Transform followTarget;
    [SerializeField] private float speed = 5;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float minDistance = 2;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        
        agent.SetDestination(followTarget.position);
        
    }       

    void OnDrawGizmos()
    {

    }
}
