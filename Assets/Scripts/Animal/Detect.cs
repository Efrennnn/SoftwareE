using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detect : MonoBehaviour
{
    public Transform playerTransform; // Assign in Inspector
    public float detectionRange = 7.0f;
    private Wandering wanderingMovement;
    private Follow followingMovement;
    Animator animator;
    bool isSleep = false;
    float lastSleep;
    [SerializeField] public float sleepTime;
    [SerializeField] int health;
    int dartCounter = 0;
    [SerializeField] public float moveSpeedReduction;

    void Start()
    {
        wanderingMovement = GetComponent<Wandering>();
        followingMovement = GetComponent<Follow>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        DetectPlayer();

        if (isSleep){
            if (Time.time - lastSleep < sleepTime){
                wanderingMovement.enabled = false;
                followingMovement.isFollowing = false;
                return;
            } else {
                isSleep = false;
                animator.Play("Tiger_Idle");
            }

            lastSleep = Time.time;
        }
        
    }

    void DetectPlayer()
    {
        if (playerTransform == null)
        {
            Debug.LogWarning("Player Transform not assigned in the Inspector");
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (!isSleep){
            if (distanceToPlayer < detectionRange && distanceToPlayer <= followingMovement.maxFollowDistance)
            {
                followingMovement.isFollowing = true;
                wanderingMovement.enabled = false;
                animator.Play("Tiger_Running");
            }
            else 
            {
                followingMovement.isFollowing = false;
                wanderingMovement.enabled = true;
                animator.Play("Tiger_Idle");
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "dartBullet"){

            dartCounter += 1;

            if (dartCounter == health){
                isSleep = true;
            }
            
            animator.Play("Tiger_Sleeping");
        }
    }
}
