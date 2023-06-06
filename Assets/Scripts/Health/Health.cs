using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    Animator anim;
    Rigidbody2D rd;
    PolygonCollider2D poly;
    private bool isDead;
    public FinishScript finishScript;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        rd = GetComponent<Rigidbody2D>();
        poly = GetComponent<PolygonCollider2D>();
    }
    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth -= _damage, 0, startingHealth);
        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
        }
        else
        {
            anim.SetTrigger("die");
            rd.gravityScale = 0;
            poly.enabled = false;
            rd.constraints = RigidbodyConstraints2D.FreezePositionX;
            rd.constraints = RigidbodyConstraints2D.FreezePositionY;
            rd.constraints = RigidbodyConstraints2D.FreezeRotation;
            //rd.constraints = RigidbodyConstraints2D.FreezeScale;
            finishScript.KilledOpponent(gameObject);
            isDead = true;
        }

    }
    public bool IsDead()
    {
        return isDead;
    }
}
