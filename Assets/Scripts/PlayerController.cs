using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D myRigidBody;
    private Vector3 change;
    private Animator animator;

    private Vector3 previousDirection = Vector3.zero;
    private int layerMask;
    Transform interactable = null;

    void Start()
    {
        layerMask = LayerMask.GetMask("Interactable");
        myRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // Manage input for movement
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        // Update animation and movement
        if (change != Vector3.zero)
        {
            myRigidBody.MovePosition(transform.position + change.normalized * speed * Time.deltaTime);
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
        
        // Check whether the player is facing interactable object
        CheckInteraction();
    }

    private void Update()
    {
        // Interact with the interactable object
        if (interactable != null && Input.GetKeyDown(KeyCode.F))
            interactable.GetComponent<Interactable>().Interact();
    }

    private void CheckInteraction()
    {
        // Determine raycast direction
        Vector3 direction = new Vector2(change.x, change.y);
        if (direction == Vector3.zero)
            direction = previousDirection;
        else
            previousDirection = direction;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f, layerMask);

        if(hit)
        {
            interactable = hit.transform;
            Debug.Log("You found a collectable item!");
        }
        else
        {
            interactable = null;
        }
    }
}
