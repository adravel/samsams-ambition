using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMovement : MonoBehaviour
{
    public float speed;
    public bool isWalking;
    public float walkTime;
    public float waitTime;
    private float walkCounter;
    private float waitCounter;
    private Rigidbody2D myRigidBody;
    private Animator animator;
    private int walkDirection;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        waitCounter = waitTime;
        walkCounter = walkTime;

        ChooseDirection();
    }

    // Update is called once per frame
    void Update()
    {
        if(isWalking)
        {
            walkCounter -= Time.deltaTime;
            animator.SetFloat("moveX", myRigidBody.velocity.x);
            animator.SetFloat("moveY", myRigidBody.velocity.y);
            animator.SetBool("isWalking", true);

            if (walkCounter < 0)
            {
                isWalking = false;
                waitCounter = waitTime;
            }

            switch (walkDirection)
            {
                case 0:
                    myRigidBody.velocity = new Vector2(0, speed);
                    break;
                case 1:
                    myRigidBody.velocity = new Vector2(-speed, 0);
                    break;
                case 2:
                    myRigidBody.velocity = new Vector2(speed, 0);
                    break;
                case 3:
                    myRigidBody.velocity = new Vector2(0, -speed);
                    break;
            }
        }
        else
        {
            waitCounter -= Time.deltaTime;
            animator.SetBool("isWalking", false);

            myRigidBody.velocity = Vector2.zero;

            if(waitCounter < 0)
            {
                ChooseDirection();
            }
            
        }
    }

    public void ChooseDirection()
    {
        walkDirection = Random.Range(0, 4);
        isWalking = true;
        walkCounter = walkTime;
    }
}
