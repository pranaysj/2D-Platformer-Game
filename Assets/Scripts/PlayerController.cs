using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;

    void Update()
    {
        float speed = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(speed));

        //Jump
        float jump = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetBool("Jump", true);
        }
        else
        {
            animator.SetBool("Jump", false);
        }

        //Flip the player
        Vector3 scale = transform.localScale;
        if (speed < 0)
        {
            scale.x = -1.0f * Mathf.Abs(scale.x);
        }
        else if (speed > 0)
        {
            scale.x = Mathf.Abs(scale.x); 
        }
        transform.localScale = scale;

        //Crouch
        if (Input.GetKey(KeyCode.RightControl))
        {
            animator.SetBool("Crouch", true);
        }
        else
        {
            animator.SetBool("Crouch", false);
        }
    }
}
