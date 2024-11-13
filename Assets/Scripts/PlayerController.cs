using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private BoxCollider2D boxCol;

    //Collider Variables
    private Vector2 boxColInitSize;
    private Vector2 boxColInitOffset;

    private void Start()
    {
        //Fetching initial collider properties
        boxColInitSize = boxCol.size;
        boxColInitOffset = boxCol.offset;

    }
    void Update()
    {
        float speed = Input.GetAxisRaw("Horizontal");
        playerAnimator.SetFloat("Speed", Mathf.Abs(speed));

        //Jump
        float jump = Input.GetAxis("Vertical");
        PlayJumpAnimation(jump);

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
        if (Input.GetKey(KeyCode.LeftControl))
        {
            Crouch(true);
        }
        else
        {
            Crouch(false);
        }
    }

    public void Crouch(bool crouch)
    {
        if (crouch == true)
        {
            float offX = -0.0978f;     //Offset X
            float offY = 0.5947f;      //Offset Y

            float sizeX = 0.6988f;     //Size X
            float sizeY = 1.3398f;     //Size Y

            boxCol.size = new Vector2(sizeX, sizeY);   //Setting the size of collider
            boxCol.offset = new Vector2(offX, offY);   //Setting the offset of collider
        }

        else
        {
            //Reset collider to initial values
            boxCol.size = boxColInitSize;
            boxCol.offset = boxColInitOffset;
        }

        playerAnimator.SetBool("Crouch", crouch);
    }


    public void PlayJumpAnimation(float vertical)
    {
        if (vertical > 0)
        {
            playerAnimator.SetTrigger("Jump");
        }
    }
}
