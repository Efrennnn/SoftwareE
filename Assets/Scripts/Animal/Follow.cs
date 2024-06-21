using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform playerTransform; // Assign in Inspector
    public Rigidbody2D rb2d;
    public float followSpeed = 0.5f;
    public bool isFollowing = false;
    public float stopDistance = 1.0f; // Minimum distance from the player
    public float maxFollowDistance = 10.0f; // Maximum distance to follow the player
    bool FacingRight = false;

    private void Start() {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isFollowing)
        {
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
        if (playerTransform == null)
        {
            Debug.LogWarning("Player Transform not assigned in the Inspector");
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        
        if (distanceToPlayer > maxFollowDistance)
        {
            isFollowing = false; // Stop following if the player is too far away
            return;
        }

        if (distanceToPlayer > stopDistance)
        {
            Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;

            if (directionToPlayer.x < 0 && FacingRight){
                transform.Rotate(0, 180, 0);
                FacingRight = false;
            } else if (directionToPlayer.x > 0 && !FacingRight){
                transform.Rotate(0, 180, 0);
                FacingRight = true;
            }

            // transform.position += directionToPlayer * followSpeed * Time.deltaTime;
            rb2d.velocity = new Vector2(directionToPlayer.x * followSpeed, directionToPlayer.y * followSpeed);
            
        }
    }
}
