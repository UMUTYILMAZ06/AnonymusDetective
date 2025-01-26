using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class ChasingEnemy : MonoBehaviour
{
    [SerializeField] private Transform playerTransform; // oyuncunun transformu
    [SerializeField] private float chaseDistanceRun = 5f; // ko?arak kovalama mesafesi
    [SerializeField] private float chaseDistanceWalk = 5f; // yürüyerek kovalama mesafesi
    [SerializeField] private float chaseDuration = 10f; // kovalamak için süre
    [SerializeField] private float walkSpeed = 2f; // yürüyü? h?z?
    [SerializeField] private float runSpeed = 6f; // ko?u h?z?
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
    public Animator animator; // reference to the enemy's Animator component

    [SerializeField] private AudioClip  ChasingAudioClip;
    [SerializeField] private AudioClip OldAudioClip;
    [SerializeField] private AudioClip firstAudioClip;
    [SerializeField] private AudioSource audioSource;

    

    public float deathRange = .75f; // the distance at which the enemy kills the player

    private void Awake()
    {

        animator.SetBool("IsWalking", true);
        navMeshAgent = GetComponent<NavMeshAgent>();
        runningChecker = FindObjectOfType<RunningChecker>(); // RunningChecker referans?n? al
        fieldOfView = FindObjectOfType<FieldOfView>(); // FieldOfView referans?n? al
        Breakable.OnStoneBreak += GoToBreakPosition; // Ta? k?r?lma olay?na abone ol
        UpdateDestination();
    }

    private void OnDestroy()
    {
        Breakable.OnStoneBreak -= GoToBreakPosition; // Olaydan ç?k
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        PlayerDeath(distanceToPlayer); // method that reloads the level when enemy catches player

        if (isChasing)
        {
             if (audioSource.clip != ChasingAudioClip && audioSource.clip != firstAudioClip)
            {
                audioSource.Stop();
                audioSource.clip = ChasingAudioClip;
                audioSource.volume = 1f;
                audioSource.Play();
               
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
               

            }
            


            animator.SetBool("IsRunning", true);
            animator.SetBool("IsWalking", false);
            navMeshAgent.speed = runSpeed;
            StartCoroutine(ChasePlayer(chaseDuration));
        }
        else if (distanceToPlayer <= chaseDistanceRun && runningChecker != null && runningChecker.HasPositionChanged && runningChecker.isSpeedIncreased)
        {
            if (audioSource.clip != ChasingAudioClip && audioSource.clip != firstAudioClip)
            {
                audioSource.Stop();
                audioSource.clip = ChasingAudioClip;
                audioSource.volume = 1f;
                audioSource.Play();
               

            }

            animator.SetBool("IsRunning", true);
            animator.SetBool("IsWalking", false);
            navMeshAgent.speed = runSpeed;
            StartCoroutine(ChasePlayer(chaseDuration));
        }
        else if (distanceToPlayer <= chaseDistanceWalk && runningChecker != null && (!runningChecker.isSpeedIncreased || !runningChecker.HasPositionChanged))
        {
            if (audioSource.clip != ChasingAudioClip && audioSource.clip != firstAudioClip)
            {
                audioSource.Stop();
                audioSource.clip = ChasingAudioClip;
                audioSource.volume = 1f;
                audioSource.Play();
                

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
               
               
            }

            animator.SetBool("IsWalking", true);
            animator.SetBool("IsRunning", false);
            navMeshAgent.speed = walkSpeed;
            UpdateDestination();
        }

        if (isGoingToBreakPosition)
        {
            float distanceToStone = Vector3.Distance(transform.position, breakPosition);
            if(distanceToStone < deathRange)
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
                UpdateDestination(); // ?stenirse waypointler aras?nda devam eder
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
        // if the distance is close enough to the player it reloads the scene
        if (distance < deathRange)
        {
            // loads the active scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}


