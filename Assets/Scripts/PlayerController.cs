using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jump;
    public int playerHealth = 3;

    [SerializeField] private Animator playerAnimator;
    [SerializeField] private BoxCollider2D boxCol;
    [SerializeField] private Rigidbody2D rigidbody2D;

    [SerializeField] private ScoreController scoreController;

    public TextMeshProUGUI playerHealthText;

    public GameObject restartPanel;

    //Collider Variables
    private Vector2 boxColInitSize;
    private Vector2 boxColInitOffset;

    private bool isGrounded = false;
    private bool isMoveSoundPlaying = false;

    private void Start()
    {
        //Fetching initial collider properties
        boxColInitSize = boxCol.size;
        boxColInitOffset = boxCol.offset;

    }
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        bool vertical = Input.GetKeyDown(KeyCode.UpArrow);
        MoveCharacter(horizontal, vertical);
        PlayMovementAnimation(horizontal, vertical);


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

    private void MoveCharacter(float horizontal, bool vertical)
    {
        //Movement
        Vector3 positon = transform.position;
        positon.x += horizontal * speed * Time.deltaTime;
        transform.position = positon;

        //Jump
        if (vertical && isGrounded)
        {
            rigidbody2D.AddForce(new Vector2(0.0f, jump), ForceMode2D.Impulse);
        }

    }

    private void PlayMovementAnimation(float horizontal, bool vertical)
    {
        playerAnimator.SetFloat("Speed", Mathf.Abs(horizontal));

        //Flip the player
        Vector3 scale = transform.localScale;
        if (horizontal < 0)
        {
            MoveSound(true);
            scale.x = -1.0f * Mathf.Abs(scale.x);
        }
        else if (horizontal > 0)
        {
            MoveSound(true);
            scale.x = Mathf.Abs(scale.x);
        }
        else
        {
            MoveSound(false);
        }
        transform.localScale = scale;

        //Jump
        if (vertical && isGrounded)
        {
            playerAnimator.SetBool("Jump", true);
        }
        else
        {
            playerAnimator.SetBool("Jump", false);
        }
    }

    private void MoveSound(bool msound)
    {
        if (msound)
        {
            if (msound && !isMoveSoundPlaying)
            {
                SoundManager.Instance.PlayMovementSound(Sounds.PlayerMove);
                isMoveSoundPlaying = true;
                SoundManager.Instance.soundPlayer.loop = true;
            }

        }
        else if (!msound && isMoveSoundPlaying)
        {
            SoundManager.Instance.soundPlayer.loop = false;
            isMoveSoundPlaying = false; 
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

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.transform.CompareTag("Platform"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.CompareTag("Platform"))
        {
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DeathZone"))
        {
            SoundManager.Instance.Play(Sounds.PlayerDeath);
            restartPanel.SetActive(true);
        }
    }

    public void PickUpKey()
    {
        Debug.Log("Key is picked.");
        scoreController.IncreaseScore(10);
    }

    public void KillPlayer()
    {
        playerHealth--;
        playerHealthText.text = "Health : " + playerHealth + " / 3";
        if (playerHealth == 0)
        {
            SoundManager.Instance.Play(Sounds.PlayerDeath);
            restartPanel.SetActive(true);
            this.enabled = false;

        }
    }
}
