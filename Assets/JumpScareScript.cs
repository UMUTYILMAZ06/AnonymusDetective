using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class JumpScareScript : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 2f; // yürüyü? h?z?
    private NavMeshAgent navMeshAgent;
    public Transform[] waypoints;
    int waypointsIndex;
    Vector3 target;
    private bool _isActive;

    [SerializeField] private AudioClip ChasingAudioClip;
    [SerializeField] private AudioClip OldAudioClip;
    [SerializeField] private AudioClip firstAudioClip;
    [SerializeField] private AudioSource audioSource;

    public float deathRange = .75f; // dü?man?n oyuncuyu öldürece?i mesafe

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        UpdateDestination();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isActive)
            return;
        if (Vector3.Distance(transform.position, target) < 3)
        {
            _isActive = false;
            gameObject.SetActive(false);
        }
        else
        {
            UpdateDestination();
        }
    }

    void UpdateDestination()
    {
        _isActive = true;
        target = waypoints[waypointsIndex].position;
        navMeshAgent.SetDestination(target);
    }

    void IterateWayPointIndex()
    {
        waypointsIndex++;
        if (waypointsIndex == waypoints.Length)
        {
            waypointsIndex = 0;
        }
    }

    public void ActivateEnemy()
    {
        gameObject.SetActive(true);
        // ?sterseniz ba?ka i?lemler de yapabilirsiniz
    }
}
