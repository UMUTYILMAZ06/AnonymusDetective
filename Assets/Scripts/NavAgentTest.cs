using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAgentTest : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform destinationTransform;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent.SetDestination(destinationTransform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
