using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint;
    public LayerMask whatStopsMovement;
    float horizontalMovement;
    float verticalMovement;
    public bool freezePlayer = false;


    void Start()
    {
        movePoint.parent = null;
    }

    void Update()
    {
        if (!freezePlayer)
        {
            horizontalMovement = UltimateJoystick.GetHorizontalAxisRaw("Movement") + Input.GetAxisRaw("Horizontal");
            if (horizontalMovement > 1f)
            {
                horizontalMovement = 1f;
            }
            if (horizontalMovement < -1f)
            {
                horizontalMovement = -1f;
            }

            verticalMovement = UltimateJoystick.GetVerticalAxisRaw("Movement") + Input.GetAxisRaw("Vertical");
            if (verticalMovement > 1f)
            {
                verticalMovement = 1f;
            }
            if (verticalMovement < -1f)
            {
                verticalMovement = -1f;
            }


            //Character
            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
            {
                if (Mathf.Abs(horizontalMovement) == 1f)
                {
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(horizontalMovement, 0f, 0f), .2f, whatStopsMovement))
                    {
                        //Ball
                        movePoint.position += new Vector3(horizontalMovement, 0f, 0f);
                    }

                }
                if (Mathf.Abs(verticalMovement) == 1f)
                {
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, verticalMovement, 0f), .2f, whatStopsMovement))
                    {
                        //Ball
                        movePoint.position += new Vector3(0f, verticalMovement, 0f);
                    }
                }


            }


        }
        

        
    }

   
}

