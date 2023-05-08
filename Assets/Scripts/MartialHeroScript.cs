using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MartialHeroScript : MonoBehaviour
{
    public float speed = 5f;
    private float direction = 0;
    private Rigidbody2D martialhero;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    bool isTouchingGround;
    Animator martialHeroAnimation;
    bool isAttaking1 = false;
    private Health enemyHealth;
    [SerializeField] private PolygonCollider2D polyCollider;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float range;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("This works.");
        martialhero = GetComponent<Rigidbody2D>();
        martialHeroAnimation = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        direction = Input.GetAxis("Horizontal");
        if (direction > 0f)
        {
            martialhero.velocity = new Vector2(direction * speed, martialhero.velocity.y);
            transform.localScale = new Vector2(2f, 2f);
        }
        if (direction < 0f)
        {
            martialhero.velocity = new Vector2(direction * speed, martialhero.velocity.y);
            transform.localScale = new Vector2(-2f, 2f);
        }


        if (Input.GetKeyDown("up") && isTouchingGround)
        {
            martialhero.velocity = new Vector2(martialhero.velocity.x, 5);
        }
        if (Input.GetKeyDown("x")) ;
        {
            isAttaking1 = true;
        }
        if (Input.GetKeyUp("x"))
        {
            isAttaking1 = false;
        }
        martialHeroAnimation.SetBool("IsAttacking1", !isAttaking1);
        martialHeroAnimation.SetFloat("Speed", Mathf.Abs(martialhero.velocity.x));
        martialHeroAnimation.SetBool("OnGround", isTouchingGround);
    }
    private bool EnemyInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(polyCollider.bounds.center + transform.right * range * transform.localScale.x, Vector3.Scale(polyCollider.bounds.size, new Vector3(2, 1, 1)), 0, Vector2.left, 0, enemyLayer);
        enemyHealth = hit.transform.GetComponent<Health>();
        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(polyCollider.bounds.center + transform.right * range * transform.localScale.x, Vector3.Scale(polyCollider.bounds.size, new Vector3(2,1,1)));
    }
    private void DamageEnemy()
    {
        if (EnemyInSight())
        {
            enemyHealth.TakeDamage(1);
        }
    }
}
