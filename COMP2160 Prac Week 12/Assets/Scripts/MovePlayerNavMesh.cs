using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class MovePlayerNavMesh : MonoBehaviour
{
    private PlayerActions playerActions;
    private InputAction mousePosition;
    private InputAction mouseClick;

    [SerializeField] private float speed = 5;
    [SerializeField] private LayerMask layerMask;
    private Vector3 destination;
    private NavMeshAgent agent;

    void Awake()
    {
        playerActions = new PlayerActions();
        mousePosition = playerActions.Movement.Position;
        mouseClick = playerActions.Movement.Click;
    }

    void OnEnable()
    {
        mousePosition.Enable();
        mouseClick.Enable();
    }

    void OnDisable()
    {
        mousePosition.Disable();
        mouseClick.Disable();
    }

    void Start()
    {
        mouseClick.performed += GetDestination;
        destination = transform.position;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        agent.SetDestination(destination);
        
        
    }

    private void GetDestination(InputAction.CallbackContext context)
    {
        Vector2 position = mousePosition.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            destination = hit.point;
            
        }
        
        
    }        

    private void MoveToDestination()
    {
        Vector3 offset = destination - transform.position;
        Vector3 move = offset.normalized * speed * Time.deltaTime;
        if (move.magnitude > offset.magnitude)  // avoid overshoot
        {
            move = offset;
        }
        transform.Translate(move);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, destination);
    }
}
