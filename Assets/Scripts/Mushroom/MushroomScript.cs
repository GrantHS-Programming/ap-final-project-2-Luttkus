using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomScript : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;
    [SerializeField] private PolygonCollider2D polyCollider;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private PolygonCollider2D target;
    private float cooldownTimer = Mathf.Infinity;
    Animator anim;
    private Health myHealth;
    private Health playerHealth;
    [SerializeField] private float speed;
    private Rigidbody2D mushroom;
    private float direction = 0f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        mushroom = GetComponent<Rigidbody2D>();
        myHealth = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        Vector3 diff = (target.transform.position - transform.position);
        if (diff.x > 0)
        {
            direction = 1f;
        }
        else
        {
            direction = -1f;
        }
        if (direction > 0f && !myHealth.IsDead())
        {
            mushroom.velocity = new Vector2(direction * speed, mushroom.velocity.y);
            transform.localScale = new Vector2(3f, 3f);
        }
        if (direction < 0f && !myHealth.IsDead())
        {
            mushroom.velocity = new Vector2(direction * speed, mushroom.velocity.y);
            transform.localScale = new Vector2(-3f, 3f);
        }
        //Attacks
        cooldownTimer += Time.deltaTime;
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("Attacking");
            }
        }
        //animations
        anim.SetFloat("Speed", Mathf.Abs(mushroom.velocity.x));
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
