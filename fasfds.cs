using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class ChasingEnemy : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float chaseDistanceRun = 5f;
    [SerializeField] private float chaseDistanceWalk = 5f;
    [SerializeField] private float chaseDuration = 10f;
    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private float runSpeed = 6f;
    private NavMeshAgent navMeshAgent;
    public Transform[] waypoints;
    int waypointsIndex;
    Vector3 target;
    private RunningChecker runningChecker;
    private FieldOfView fieldOfView;
    private bool isChasing = false;
    private bool isGoingToBreakPosition = false;
    private Vector3 breakPosition;
    private float breakTimer = 0f;
    private const float breakDuration = 20f;
    public Animator animator;
    
    [SerializeField] private AudioClip ChasingAudioClip;
    [SerializeField] private AudioClip OldAudioClip;
    [SerializeField] private AudioClip firstAudioClip;
    [SerializeField] private AudioSource audioSource;

    private bool MusicArranger = false;

    public float deathRange = .75f;

    private void Awake()
    {
        animator.SetBool("IsWalking", true);
        navMeshAgent = GetComponent<NavMeshAgent>();
        runningChecker = FindObjectOfType<RunningChecker>(); // RunningChecker referansını al
        fieldOfView = FindObjectOfType<FieldOfView>();
        Breakable.OnStoneBreak += GoToBreakPosition;
        UpdateDestination();
    }

    private void OnDestroy()
    {
        Breakable.OnStoneBreak -= GoToBreakPosition;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        PlayerDeath(distanceToPlayer);

        if (isChasing)
        {
            if (audioSource.clip != ChasingAudioClip && audioSource.clip != firstAudioClip)
            {
                audioSource.Stop();
                audioSource.clip = ChasingAudioClip;
                audioSource.volume = 1f;
                audioSource.Play();
                Debug.Log("1");
            }

            animator.SetBool("IsRunning", true);
            navMeshAgent.speed = runSpeed;
            navMeshAgent.destination = playerTransform.position;
            return;
        }

        if (fieldOfView != null && fieldOfView.canSeePlayer)
        {
            if (audioSource.clip != ChasingAudioClip && audioSource.clip != firstAudioClip)
            {
                audioSource.Stop();
                audioSource.clip = ChasingAudioClip;
                audioSource.volume = 1f;
                audioSource.Play();
                Debug.Log("2");
            }

            animator.SetBool("IsRunning", true);
            animator.SetBool("IsWalking", false);
            navMeshAgent.speed = runSpeed;
            StartCoroutine(ChasePlayer(chaseDuration));
        }
        else if (distanceToPlayer <= chaseDistanceRun && runningChecker != null && runningChecker.isSpeedIncreased)
        {
            if (audioSource.clip != ChasingAudioClip && audioSource.clip != firstAudioClip)
            {
                audioSource.Stop();
                audioSource.clip = ChasingAudioClip;
                audioSource.volume = 1f;
                audioSource.Play();
                Debug.Log("3");
            }

            animator.SetBool("IsRunning", true);
            animator.SetBool("IsWalking", false);
            navMeshAgent.speed = runSpeed;
            StartCoroutine(ChasePlayer(chaseDuration));
        }
        else if (distanceToPlayer <= chaseDistanceWalk && runningChecker != null && !runningChecker.isSpeedIncreased)
        {
            if (audioSource.clip != ChasingAudioClip && audioSource.clip != firstAudioClip)
            {
                audioSource.Stop();
                audioSource.clip = ChasingAudioClip;
                audioSource.volume = 1f;
                audioSource.Play();
                Debug.Log("4");
            }

            animator.SetBool("IsRunning", true);
            animator.SetBool("IsWalking", false);
            navMeshAgent.speed = runSpeed;
            StartCoroutine(ChasePlayer(chaseDuration));
        }
        else if (Vector3.Distance(transform.position, target) < 3)
        {
            if (audioSource.clip != OldAudioClip && audioSource.clip != firstAudioClip)
            {
                audioSource.Stop();
                audioSource.clip = OldAudioClip;
                audioSource.volume = 0.5f;
                audioSource.Play();
                Debug.Log("4");
                MusicArranger = true;
            }

            animator.SetBool("IsWalking", true);
            animator.SetBool("IsRunning", false);
            navMeshAgent.speed = walkSpeed;
            IterateWayPointIndex();
            UpdateDestination();
        }
        else
        {
            if (audioSource.clip != OldAudioClip && audioSource.clip != firstAudioClip)
            {
                audioSource.Stop();
                audioSource.clip = OldAudioClip;
                audioSource.volume = 0.5f;
                audioSource.Play();
                Debug.Log("5");
                MusicArranger = true;
            }

            animator.SetBool("IsWalking", true);
            animator.SetBool("IsRunning", false);
            navMeshAgent.speed = walkSpeed;
            UpdateDestination();
        }

        if (isGoingToBreakPosition)
        {
            float distanceToStone = Vector3.Distance(transform.position, breakPosition);
            if (distanceToStone < deathRange)
            {
                animator.SetBool("IsRunning", false);
                animator.SetBool("IsWalking", false);
            }
            else
            {
                animator.SetBool("IsRunning", true);
                animator.SetBool("IsWalking", false);
            }

            navMeshAgent.speed = runSpeed;
            navMeshAgent.SetDestination(breakPosition);

            breakTimer += Time.deltaTime;
            if (breakTimer >= breakDuration)
            {
                isGoingToBreakPosition = false;
                breakTimer = 0f;
                UpdateDestination();
            }
            return;
        }
    }

    IEnumerator ChasePlayer(float duration)
    {
        isChasing = true;
        float timer = 0f;

        while (timer < duration)
        {
            navMeshAgent.destination = playerTransform.position;
            timer += Time.deltaTime;
            yield return null;
        }

        isChasing = false;
        UpdateDestination();
    }

    void UpdateDestination()
    {
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

    void GoToBreakPosition(Vector3 breakPosition)
    {
        this.breakPosition = breakPosition;
        isGoingToBreakPosition = true;
    }

    private void PlayerDeath(float distance)
    {
        if (distance < deathRange)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
