using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;
    [SerializeField] private PolygonCollider2D polyCollider;
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;
    Animator anim;
    private Health playerHealth;
    private float direction = 1f;
    [SerializeField] private float speed;
    private Rigidbody2D goblin;
    private float placeholder;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        goblin = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {

        //Attacks
        //hit.transform.GetComponent<Animator>().IsPlaying(MartialHeroAttack);
        cooldownTimer += Time.deltaTime;
        if (PlayerInSight1())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("Attacking1");
            }
        }
        else if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("Attacking");
            }
        }
        //animation
        anim.SetFloat("Speed", Mathf.Abs(goblin.velocity.x));
    }
    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(polyCollider.bounds.center + transform.right * 0.3f * transform.localScale.x, polyCollider.bounds.size, 0, Vector2.left, 0, playerLayer);
        return hit.collider != null;
    }
    private bool PlayerInSight1()
    {
        RaycastHit2D hit = Physics2D.BoxCast(polyCollider.bounds.center + transform.right * 0.5f * transform.localScale.x, polyCollider.bounds.size, 0, Vector2.left, 0, playerLayer);
        return hit.collider != null && hit.transform.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("MartialHeroAttack");
    }
    private bool PlayerInSight2()
    {
        RaycastHit2D hit = Physics2D.BoxCast(polyCollider.bounds.center + transform.right * 0.5f * transform.localScale.x, polyCollider.bounds.size, 0, Vector2.left, 0, playerLayer);
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
            RaycastHit2D hit = Physics2D.BoxCast(polyCollider.bounds.center + transform.right * 0.3f * transform.localScale.x, polyCollider.bounds.size, 0, Vector2.left, 0, playerLayer);
            playerHealth = hit.transform.GetComponent<Health>();
            playerHealth.TakeDamage(damage);
        }
    }
    private void DamagePlayer1()
    {
        if (PlayerInSight2())
        {
            RaycastHit2D hit = Physics2D.BoxCast(polyCollider.bounds.center + transform.right * 0.5f * transform.localScale.x, polyCollider.bounds.size, 0, Vector2.left, 0, playerLayer);
            playerHealth = hit.transform.GetComponent<Health>();
            playerHealth.TakeDamage(damage);
        }
    }
}
