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
        Debug.Log("ChaseState");
        /*
        if (!chasing)
        {
            Debug.Log("Not chasing");
            Transform target = player.transform;

            targetPosition = target.position;

            MoveToDirection((targetPosition - transform.position).normalized.x);
            Debug.Log("Started chasing");
            chasing = true;
        }*/

        Transform target = player.transform;

        targetPosition = target.position;
        var distance = targetPosition - transform.position;
        MoveToDirection(distance.normalized.x);

        spriteRenderer.flipX = distance.x >= 0;

        animator.SetFloat("velX", Mathf.Abs(body.velocity.x));
        
        if (CheckIfReachedTargetPosition())
        {
            Debug.LogWarning("Target reached");
            body.velocity = body.velocity * Vector2.up;
            animator.SetFloat("velX", Mathf.Abs(body.velocity.x));
            chasing = false;
            int n = Random.Range(0, 2);
            if (n == 0)
            {
                Debug.Log("MeleeAttack");
                return MeleeAttackState;
            }
            else
            {
                Debug.Log("DistanceAttack");
                return DistanceAttackState;
            }
        }
        else
        {
            Debug.Log("Continue chasing");
            return this;
        }
    }
}