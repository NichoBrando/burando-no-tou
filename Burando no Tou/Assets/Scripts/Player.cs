using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 15;

    const string IDLE = "idle_";
    const string WALKING = "walking_";

    private Rigidbody2D body;
    private int movespeed = 3;
    private Animator animator;
    private string lastMovement = "down";

    public GameObject BattleScene;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Move()
    {
        if (BattleScene.activeSelf) {
            body.velocity = new Vector2(0, 0);
            return;
        };

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        body.velocity = new Vector2(x, y) * movespeed;
        string newAnimation = "";
        
        if(x != 0 || y != 0)
        {
            string direction = "";
            if(y > 0)
            {
                direction = "up";
            }
            else if (y < 0)
            {
                direction = "down";
            }
            if(x > 0)
            {
                direction = "right";
            }
            else if (x < 0)
            {
                direction = "left";
            }
            lastMovement = direction;
            newAnimation = $"{WALKING}{direction}";
        }
        else{
            newAnimation = $"{IDLE}{lastMovement}";
        }

        ChangeAnimation(newAnimation);
    }

    void ChangeAnimation(string newAnimation) 
    {
        animator.Play(newAnimation, 0);
    }

    void Update()
    {
        Move();
    }
}
