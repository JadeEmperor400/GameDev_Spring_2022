using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint;
    public LayerMask whatStopsMovement;
    Vector2 movement;
    public bool freezePlayer = false;
    public Animator animator;
    private KeyCode lastHitKey;


    void Start()
    {
        movePoint.parent = null;
    }

    void Update()
    {
        //Animator
        if (!freezePlayer)
        {
            movement.x = UltimateJoystick.GetHorizontalAxisRaw("Movement") + Input.GetAxisRaw("Horizontal");
            if (movement.x > 1f)
            {
                movement.x = 1f;
            }
            if (movement.x < -1f)
            {
                movement.x = -1f;
            }

            movement.y = UltimateJoystick.GetVerticalAxisRaw("Movement") + Input.GetAxisRaw("Vertical");
            if (movement.y > 1f)
            {
                movement.y = 1f;
            }
            if (movement.y < -1f)
            {
                movement.y = -1f;
            }

            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Speed", movement.sqrMagnitude);

            switch ((movement.x == -1f) ? 0 : (movement.x == 1f) ? 1 : (movement.y == -1f) ? 2 : (movement.y == 1f) ? 3 : 4)
            {
                case 0:
                    animator.SetFloat("LastHorizontal", -1f);
                    animator.SetFloat("LastVertical", 0f);
                    break;
                case 1:
                    animator.SetFloat("LastHorizontal", 1f);
                    animator.SetFloat("LastVertical", 0f);
                    break;
                case 2:
                    animator.SetFloat("LastVertical", -1f);
                    animator.SetFloat("LastHorizontal", 0f);
                    break;
                case 3:
                    animator.SetFloat("LastVertical", 1f);
                    animator.SetFloat("LastHorizontal", 0f);
                    break;
            }




            //Character Movement
            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
            {
                if (Mathf.Abs(movement.y) == 1f)
                {
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, movement.y, 0f), .2f, whatStopsMovement))
                    {
                        movePoint.position += new Vector3(0f, movement.y, 0f);
                        return;
                    }
                }
                if (Mathf.Abs(movement.x) == 1f)
                {
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(movement.x, 0f, 0f), .2f, whatStopsMovement))
                    {
                        movePoint.position += new Vector3(movement.x, 0f, 0f);
                        return;
                    }

                }
                



            }


        }
        

        
    }

   
    public bool IsPlayerFrozen()
    {
        return freezePlayer;
    }

    public void FreezePlayer()
    {
        freezePlayer = true;
    }

    public void UnFreezePlayer()
    {
        freezePlayer = false;
    }
}

