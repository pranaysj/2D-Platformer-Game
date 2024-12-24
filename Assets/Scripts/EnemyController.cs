using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float patrolDistance = 5f; 
    public float patrolSpeed = 2f;

    private Vector3 startPosition;
    private bool movingRight = true;
    [SerializeField] private SpriteRenderer sprite;

    void Start()
    {
        startPosition = transform.position;
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (movingRight)
        {
            transform.position += Vector3.right * patrolSpeed * Time.deltaTime;
            if (transform.position.x >= startPosition.x + patrolDistance)
            {
                movingRight = false;
                sprite.flipX = true;
            }
        }
        else
        {
            transform.position += Vector3.left * patrolSpeed * Time.deltaTime;
            if (transform.position.x <= startPosition.x)
            {
                movingRight = true;
                sprite.flipX = false;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(startPosition, startPosition + Vector3.right * patrolDistance);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();

        if (playerController != null)
        {
            playerController.KillPlayer();
        }
    }
}
