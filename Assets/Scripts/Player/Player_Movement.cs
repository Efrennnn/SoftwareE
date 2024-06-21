using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
public class Character_Controller : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private enum State {
        Normal,
        Dashing
    }

    [Header("Character Movement")]
    public float speed;
    float horizontal;
    float vertical;
    Vector2 moveDirection;
    private State state;
    bool FacingRight;

    [Header("Dash")]
    public float dashSpeed;
    float dashSpeedCounter;
    public float dashCooldown;
    Vector2 dashDirection;
    public float dashSpeedDropMultiplier;
    public float minimumDashSpeed;
    float lastDash;

    [Header("Gun")]
    public Gun_Controller gun;
    Vector2 mousePosition;
    private Transform aim;
    public float shotDelay;
    float lastShot;
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();                 // Used for animating
        spriteRenderer = GetComponent<SpriteRenderer>();     // Used for flipping the sprite
        state = State.Normal;
        dashSpeedCounter = dashSpeed;

        aim = transform.Find("Aim");
    }

    
    void Update()
    {
        switch(state)
        {
            case State.Normal:
                horizontal = Input.GetAxisRaw("Horizontal");
                vertical = Input.GetAxisRaw("Vertical");

                moveDirection = new Vector2(horizontal, vertical).normalized;

                // Aim
                mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                Vector2 aimDirection = (mousePosition - rb2d.position).normalized;
                float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
                aim.eulerAngles = new Vector3(0, 0, aimAngle);

                if (aimAngle < 90 && aimAngle > -90){
                    if (FacingRight){
                        Flip();
                        FacingRight = false;
                        gun.Flip();
                    }
                } else{
                    if (!FacingRight){
                        Flip();
                        FacingRight = true;
                        gun.Flip();
                    }
                }

                // Shoot
                if (Input.GetMouseButtonDown(0))
                {
                    if (Time.time - lastShot < shotDelay){
                        return;
                    }

                    lastShot = Time.time;
                    gun.Fire();
                }

                // Dash
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (Time.time - lastDash < dashCooldown){
                        return;
                    }
                    
                    lastDash = Time.time;
                    dashDirection = moveDirection;
                    state = State.Dashing;
                }

                // Animation
                if (horizontal != 0 || vertical != 0){
                    animator.Play("Player_Running");
                } else {
                    animator.Play("Player_Idle");
                }

                break;

            case State.Dashing:
                dashSpeed -= dashSpeed * dashSpeedDropMultiplier * Time.deltaTime;

                if (dashSpeed <= minimumDashSpeed)
                {
                    dashSpeed = dashSpeedCounter;
                    state = State.Normal;
                }

                // Animation
                animator.Play("Player_Rolling");
                
                break;
        }
    }

    private void FixedUpdate()
    {  
        switch (state)
        {
            case State.Normal:
                rb2d.velocity = new Vector2(horizontal * speed, vertical * speed);
                
                break;

            case State.Dashing:
                rb2d.velocity = dashDirection * dashSpeed;
                state = State.Dashing;
                break;
        }
        
    }

    private void Flip (){
        FacingRight = !FacingRight;
        
        transform.Rotate(0, 180, 0);
    }
}