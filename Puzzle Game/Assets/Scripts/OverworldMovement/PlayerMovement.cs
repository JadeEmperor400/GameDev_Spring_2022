using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D theRB;
    public float moveSpeed;
    private Vector2 moveInput;

    private void Start()
    {
        theRB.freezeRotation = true;
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        // Y movement in 3d is vertical, so we are using Y movement in Z, that is depth.
        theRB.velocity = new Vector3(moveInput.x * moveSpeed, theRB.velocity.y, moveInput.y * moveSpeed);

    }

    void FixedUpdate()
    {
        theRB.MovePosition(theRB.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }
}
