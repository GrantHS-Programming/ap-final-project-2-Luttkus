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
    private Health myHealth;
    private float direction = 1f;
    [SerializeField] private float speed;
    private Rigidbody2D goblin;
    private float placeholder;
    private Vector2[] firstPath = { 
        new Vector2(0.1342627f, -0.05592072f), 
        new Vector2(0.12f, -0.02f), 
        new Vector2(0.1127807f, 0.08939966f), 
        new Vector2(0.06440747f, 0.0869829f), 
        new Vector2(0.03006224f, 0.108017f), 
        new Vector2(0.01395575f, 0.1015587f), 
        new Vector2(0.004965395f, 0.0939185f),
        new Vector2(-0.01284889f, 0.09043007f), 
        new Vector2(-0.02447999f, 0.08762902f), 
        new Vector2(-0.06890867f, 0.0806005f), 
        new Vector2(-0.06557699f, 0.05797824f), 
        new Vector2(-0.04611064f, 0.0318889f), 
        new Vector2(-0.04363068f, 0.005689383f), 
        new Vector2(-0.1148972f, -0.02338666f), 
        new Vector2(-0.1266398f, -0.03680535f), 
        new Vector2(-0.1462559f, -0.04816365f), 
        new Vector2(-0.1629792f, -0.06049822f), 
        new Vector2(-0.1617854f, -0.09639814f), 
        new Vector2(-0.1446151f, -0.1237493f), 
        new Vector2(-0.1400144f, -0.1351975f), 
        new Vector2(-0.1181871f, -0.1893672f), 
        new Vector2(-0.1336641f, -0.2153189f), 
        new Vector2(-0.15f, -0.2600119f), 
        new Vector2(-0.08424659f, -0.2619922f), 
        new Vector2(0.13f, -0.2613444f), 
        new Vector2(0.13444f, -0.1726438f), 
        new Vector2(0.165507f, -0.1292666f), 
        new Vector2(0.1620536f, -0.08305579f), 
        new Vector2(0.1512065f, -0.0707723f)
    };
    private Vector2[] backpath = {
     new Vector2(-0.1150824f, -0.05331741f),
     new Vector2(-0.245329f, -0.026698f),
     new Vector2(-0.2020133f, -0.1149379f),
     new Vector2(-0.2913797f, -0.1180332f),
     new Vector2(-0.3720769f, -0.158626f),
     new Vector2(-0.2905805f, -0.159144f),
     new Vector2(-0.3245947f, -0.2558459f),
     new Vector2(-0.1201998f, -0.1731753f),
     new Vector2(-0.1178306f, -0.1838292f),
     new Vector2(-0.1181871f, -0.1893672f),
     new Vector2(-0.1336641f, -0.2153189f),
     new Vector2(-0.3316319f, -0.2600119f),
     new Vector2(-0.08424659f, -0.2619922f),
     new Vector2(-0.04998179f, -0.2563908f),
     new Vector2(-0.09507617f, -0.1726438f),
     new Vector2(-0.1019883f, -0.1177081f),
     new Vector2(-0.07797024f, -0.1122241f),
     new Vector2(-0.06779173f, -0.03950243f),
    };
    [SerializeField] private PolygonCollider2D target;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        goblin = GetComponent<Rigidbody2D>();
        myHealth = GetComponent<Health>();
        polyCollider.enabled = false;
        polyCollider.pathCount = 1;
        polyCollider.SetPath(0, firstPath);
        polyCollider.enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
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
            goblin.velocity = new Vector2(direction * speed, goblin.velocity.y);
            transform.localScale = new Vector2(3f, 3f);
        }
        if (direction < 0f && !myHealth.IsDead())
        {
            goblin.velocity = new Vector2(direction * speed, goblin.velocity.y);
            transform.localScale = new Vector2(-3f, 3f);
        }
        if (diff.y > 0 && !myHealth.IsDead())
        {
            goblin.velocity = new Vector2(direction * speed, 7f);
        }
        //Attacks
        //hit.transform.GetComponent<Animator>().IsPlaying(MartialHeroAttack);
            cooldownTimer += Time.deltaTime;
        if (PlayerInSight1())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("Attacking1");
                anim.SetTrigger("Attacking");
                polyCollider.enabled = false;
                polyCollider.pathCount = 1;
                polyCollider.SetPath(0, backpath);
                polyCollider.enabled = true;
            }
        }
        else if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
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
        RaycastHit2D hit = Physics2D.BoxCast(polyCollider.bounds.center + transform.right * 0.6f * transform.localScale.x, polyCollider.bounds.size, 0, Vector2.left, 0, playerLayer);
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
            RaycastHit2D hit = Physics2D.BoxCast(polyCollider.bounds.center + transform.right * 0.6f * transform.localScale.x, polyCollider.bounds.size, 0, Vector2.left, 0, playerLayer);
            playerHealth = hit.transform.GetComponent<Health>();
            playerHealth.TakeDamage(damage);
        }
    }
    private void ReturntToFrontpath()
    {
        polyCollider.enabled = false;
        polyCollider.pathCount = 1;
        polyCollider.SetPath(0, firstPath);
        polyCollider.enabled = true;
    }
}
