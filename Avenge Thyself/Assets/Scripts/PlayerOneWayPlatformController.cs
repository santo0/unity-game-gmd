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

    public IEnumerator DisableCollision()
    {
            BoxCollider2D platformCollider = currentOneWayPlatform.GetComponent<BoxCollider2D>();
            Physics2D.IgnoreCollision(playerCollider, platformCollider);
            yield return new WaitForSeconds(1f);
            Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
    }
}
