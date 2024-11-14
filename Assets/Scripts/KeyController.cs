using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public Animator keyAnimator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.PickUpKey();
            Destroy(gameObject, 1.0f); 
            keyAnimator.SetTrigger("Collected");
        }
    }


}
