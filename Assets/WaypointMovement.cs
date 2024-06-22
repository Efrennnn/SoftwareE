using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMovement : MonoBehaviour
{
    [SerializeField] Transform [] waypoint;
    [SerializeField] private float movementSpeed0;
    [SerializeField] private float movementSpeed1;
    [SerializeField] private float movementSpeed2;
    private float baseMovementSpeed;
    [Range(0f, 10f)][SerializeField] private float detectionRange;
    [Range(1f, 2f)][SerializeField] private float movementSpeedMultiplier;
    int isHit = 0;
    private int waypointIndex;
    private GameObject player;
    bool speedy = false;
    Animator animator;
    bool facingRight;
    public static bool isSleep;

    [Header ("Animation")]
    public String idleAnimation;
    public String runningAnimation;
    public String sleepAnimation;


    void Start()
    {
        transform.position = waypoint[waypointIndex].transform.position;

        player = GameObject.FindGameObjectWithTag("Player");

        animator = GetComponent<Animator>();

        baseMovementSpeed = movementSpeed0;

        waypointIndex = 0;

    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer < detectionRange){

            if (!speedy){
                speedy = true;
                movementSpeed0 = baseMovementSpeed * movementSpeedMultiplier;
            } 

        } else {
            if (speedy){
                speedy = false;
                movementSpeed0 = baseMovementSpeed / movementSpeedMultiplier;
            } 
        }

        if (isHit == 1){
            movementSpeed0 = movementSpeed1;

        } else if (isHit == 2){
            movementSpeed0 = movementSpeed2;

        } else if (isHit > 2) {
            animator.Play(sleepAnimation);
            isSleep = true;
        }

        if (!isSleep){
            if (waypointIndex <= waypoint.Length - 1){
                transform.position = Vector2.MoveTowards(transform.position, waypoint[waypointIndex].transform.position, movementSpeed0 * Time.deltaTime);
                Debug.Log(movementSpeed0);

                animator.Play(runningAnimation);

                if (waypoint[waypointIndex].transform.position.x < transform.position.x){
                    if (facingRight){
                        Flip();
                        facingRight = false;
                    }
                } else {
                    if (!facingRight){
                        Flip();
                        facingRight = true;
                    }
                }

                if (transform.position == waypoint[waypointIndex].transform.position){
                    waypointIndex += 1;
                }
            }

            if (waypointIndex == waypoint.Length){
                waypointIndex = 0;
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("dartBullet")){
            isHit += 1;
        }
    }

    void Flip(){
        transform.Rotate(0,180,0);
    }
}