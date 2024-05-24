using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeformeController : MonoBehaviour
{

    [SerializeField] MainCharacter target;
    [SerializeField] AudioClip dieSound;
    [SerializeField] AudioClip meleeSound;

    private Animator anim;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    private float leftDir;
    private float rightDir;

    private bool dead;

    private float seekRadius = 10;
    private float attackRadius = 3;
    private bool canAttack;

    private float attackSpeed = 1f;
    private float attackCounter = 0;
    private float meleeDamage = 3;
    
    public bool IsDead { get => dead; }
    public bool IsAlive { get => !dead; }

    public Rigidbody2D Rb
    {
        get
        {
            return rb;
        }
    }

    public Animator Anim
    {
        get
        {
            return anim;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        boxCollider = GetComponent<BoxCollider2D>();
        target = FindObjectOfType<MainCharacter>();
        dead = false;
        attackSpeed = 1.5f;
        attackCounter = 0;
        canAttack = true;

        leftDir = transform.localScale.x;
        rightDir = -transform.localScale.x;
    }

    void Movement()
    {
        rb.velocity = Vector2.zero;

        if (IsAlive && target != null && (target.transform.position - this.transform.position).magnitude < seekRadius)
        {
            Vector2 moveTo = target.transform.position - transform.position;
            rb.velocity = moveTo.normalized * 3;

            float direccion = rb.velocity.x > 0 ? rightDir : leftDir;
            this.transform.localScale = new Vector3(direccion, transform.localScale.y, transform.localScale.z);
        }
    }

    void CheckCanAttack()
    {
        if (canAttack)
            return;

        attackCounter += Time.deltaTime;

        if (attackCounter > attackSpeed)
        {
            canAttack = true;
            attackCounter = 0;
        }
    }

    void Attack()
    {
        if (IsAlive && target != null && (target.transform.position - this.transform.position).magnitude < attackRadius && canAttack && target.Alive) {
            target.ApplyDamage(meleeDamage);
            AudioSource.PlayClipAtPoint(meleeSound, transform.position);
            canAttack = false;
        }
    }
    void Update()
    {
        Movement();
        CheckCanAttack();
        Attack();
    }
    public void Die()
    {
        AudioSource.PlayClipAtPoint(dieSound, transform.position);
        dead = true;
        this.transform.Rotate(0,0,90);
        this.boxCollider.enabled = false;
        rb.velocity = Vector2.zero;
    }

    public void SetTarget(MainCharacter target)
    {
        this.target = target;
    }
}
