using UnityEngine;
public class BossChaseState : MonoBehaviour, State
{
    public BossMeleeAttackState MeleeAttackState;
    public BossDistanceAttackState DistanceAttackState;

    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D body;

    public Vector3 targetPosition; //when null, no target position

    public float positionOffset;

    public float baseVelocity;

    public GameObject player;

    public bool chasing;

    public float distanceProbability;

    public Transform attacksTransform;

    private bool spriteFlipped = false;


    private void Awake()
    {
        chasing = false;
    }



    void MoveToDirection(float dir)
    {
        //        body.velocity = (body.velocity + Vector2.right).normalized * baseVelocity * dir;
        body.velocity = new Vector2(dir * baseVelocity * Time.deltaTime, body.velocity.y);


    }

    Vector2 GetPlayerReachPosition(Transform target)
    {
        return target.position;
    }

    bool CheckIfReachedTargetPosition()
    {
        return targetPosition.x <= transform.position.x + positionOffset &&
               targetPosition.x >= transform.position.x - positionOffset;
    }


    public State RunState()
    {
        Transform target = player.transform;

        targetPosition = target.position;
        Vector2 dist = targetPosition - transform.position;
        MoveToDirection(dist.normalized.x);

        //        spriteRenderer.flipX = dist.x >= 0;

        if (spriteFlipped != (dist.normalized.x < 0))
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
            attacksTransform.localScale = new Vector3(-attacksTransform.localScale.x,
                                                      attacksTransform.localScale.y,
                                                      attacksTransform.localScale.z);
            spriteFlipped = !spriteFlipped;
        }

        animator.SetFloat("velX", Mathf.Abs(body.velocity.x));
        if (CheckIfReachedTargetPosition())
        {
            body.velocity = body.velocity * Vector2.up;
            animator.SetFloat("velX", Mathf.Abs(body.velocity.x));
            chasing = false;
            return MeleeAttackState;
        }
        float n = Random.Range(0f, 1f);
        if (n <= distanceProbability)
        {
            Debug.Log(n);
            body.velocity = body.velocity * Vector2.up;
            animator.SetFloat("velX", Mathf.Abs(body.velocity.x));
            chasing = false;
            return DistanceAttackState;
        }
        else
        {
            return this;
        }
    }
}