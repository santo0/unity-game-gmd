using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneWayPlatformController : MonoBehaviour
{
    public GameObject currentOneWayPlatform;
    private Collider2D playerCollider;

    private void Start()
    {
        playerCollider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            currentOneWayPlatform = other.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            currentOneWayPlatform = null;
        }
    }

    public IEnumerator Cor_DisableCollision()
    {
            //Disable collision for the current platform the player is on
            BoxCollider2D platformCollider = currentOneWayPlatform.GetComponent<BoxCollider2D>();
            Physics2D.IgnoreCollision(playerCollider, platformCollider);
            yield return new WaitForSeconds(1f);
            //Return to original state after 1s
            Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
    }
}
