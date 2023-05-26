using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;
    [SerializeField] private PolygonCollider2D polyCollider;
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;
    Animator anim;
    private Health myHealth;
    private Health playerHealth;
    private float direction = 1f;
    [SerializeField] private float speed;
    private Rigidbody2D skeleton;
    private float placeholder;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        skeleton = GetComponent<Rigidbody2D>();
        myHealth = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        if (direction != 0 && skeleton.velocity == new Vector2(0,0) && !PlayerInSight())
        {
            direction *= -1f;
        }
        if (direction > 0f && !myHealth.IsDead())
        {
            skeleton.velocity = new Vector2(direction * speed, skeleton.velocity.y);
            transform.localScale = new Vector2(2f, 2f);
        }
        if (direction < 0f && !myHealth.IsDead())
        {
            skeleton.velocity = new Vector2(direction * speed, skeleton.velocity.y);
            transform.localScale = new Vector2(-2f, 2f);
        }
        //attacking
        cooldownTimer += Time.deltaTime;
        //only when sees player
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("Attacking");
            }
        }
        anim.SetFloat("speed", Mathf.Abs(skeleton.velocity.x));

    }
    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(polyCollider.bounds.center + transform.right * range * transform.localScale.x, polyCollider.bounds.size, 0, Vector2.left, 0, playerLayer);
        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(polyCollider.bounds.center + transform.right * range * transform.localScale.x, polyCollider.bounds.size);
    }
    private void DamagePlayer()
    {
        if (PlayerInSight())
        {
            RaycastHit2D hit = Physics2D.BoxCast(polyCollider.bounds.center + transform.right * range * transform.localScale.x, polyCollider.bounds.size, 0, Vector2.left, 0, playerLayer);
            if (hit.transform.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("MartialHeroBlock"))
            {
                cooldownTimer = -1;
            }
            else
            {
                playerHealth = hit.transform.GetComponent<Health>();
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
