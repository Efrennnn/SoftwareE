using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerMovement : MonoBehaviour
{
    Rigidbody2D rb2d;
    [SerializeField] public float movementSpeed; 
    [SerializeField] public float detectionRange;
    [SerializeField] public float multiplier1;
    [SerializeField] public float multiplier2;
    [SerializeField] public float multiplier3;
    int isHit;
    public static bool isSleep = false;
    GameObject player;
    bool FacingRight = false;
    Animator animator;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        animator.Play("Tiger_Running");
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;

    
        if (distanceToPlayer < detectionRange && !isSleep){
            if (directionToPlayer.x < 0 && FacingRight){
                transform.Rotate(0, 180, 0);
                FacingRight = false;
            } else if (directionToPlayer.x > 0 && !FacingRight){
                transform.Rotate(0, 180, 0);
                FacingRight = true;
            }
            if (isHit < 4){
                rb2d.velocity = new Vector2(directionToPlayer.x * movementSpeed * Time.deltaTime * multiplier1, directionToPlayer.y * movementSpeed * Time.deltaTime * multiplier1);

            }
            
            if (isHit >= 4 && isHit < 10){
                rb2d.velocity = new Vector2(directionToPlayer.x * movementSpeed * Time.deltaTime * multiplier2, directionToPlayer.y * movementSpeed * Time.deltaTime * multiplier2);

            }

            if (isHit == 10){
                
                animator.Play("Tiger_Sleeping");
                isSleep = true;

            }
            
        }
    }


    void OnTriggerEnter2D (Collider2D other){
        if (other.gameObject.CompareTag("dartBullet")){
            isHit += 1;
            Debug.Log("HIT");
        }
    } 
}