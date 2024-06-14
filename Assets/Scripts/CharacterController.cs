using UnityEditor.Animations;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;
using AnimatorController = UnityEditor.Animations.AnimatorController;

public abstract class CharacterController : MonoBehaviour
{

    protected Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D boxCollider;

    private AnimatorOverrideController animatorOverrideController;
    
    [Header("Animation Clips")]
    [SerializeField] private AnimationClip idleClip;
    [SerializeField] private AnimationClip movingClip;
    [SerializeField] private AnimationClip attackingClip;
    [SerializeField] private AnimationClip dyingClip;
    [SerializeField] private AnimationClip deadClip;

    [Header("Stats")]
    [SerializeField] protected float maxLife;
    protected float life;

    [SerializeField] protected float maxShield;
    protected float shield;

    [SerializeField] protected float maxMovementSpeed;
    [SerializeField] protected float movementSpeed;

    [SerializeField] protected float maxAttackSpeed;
    [SerializeField] protected float attackSpeed;

    [Header("Bullet")]
    [SerializeField] protected BulletController bullet;
    [SerializeField] protected Transform bulletSpawner;

    [Header("Sounds")]
    [SerializeField] protected AudioClip movingSound;
    [SerializeField] protected AudioClip deathSound;
    [SerializeField] protected AudioClip meleeSound;
    [SerializeField] protected AudioClip shootSound;

    // State
    private float attackCooldown;
    private bool canAttack;

    // Mirror sprite
    private float leftDir;
    private float rightDir;

    // pause
    private bool canMove = true;

    public bool IsAlive { get => life > 0; }
    public bool IsMoving { get => rb.velocity.magnitude > 0.1; }
    public bool HasShield { get => shield > 0; }

    public enum Stats
    {
        LifePoints,
        Shield,
        characterSpeed,
        attackSpeed
    }

    public bool CanMove
    {
        set
        {
            canMove = value;
        }
    }

    void UpdateStates()
    {
        anim.SetBool("moving", IsAlive && IsMoving);
        anim.SetBool("attacking", IsAlive && IsAttacking());
        anim.SetBool("dying", !IsAlive);
    }
    protected abstract void ExecuteMove();

    private void Move()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Time.deltaTime * Vector2.down;
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            life = 0;
        }

        ExecuteMove();

        float direccion = rb.velocity.x > 0 ? rightDir : leftDir;
        anim.transform.localScale = new Vector3(direccion, anim.transform.localScale.y, anim.transform.localScale.z);
    }
    protected virtual void Die()
    {
        if(deathSound != null)
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
        this.boxCollider.enabled = false;
    }
    protected abstract bool IsAttacking();
    protected abstract void ExecuteAttack();

    private void CheckAttackCooldown()
    {
        if (canAttack)
            return;

        attackCooldown += Time.deltaTime;

        if (attackCooldown > (5 / attackSpeed))
        {
            canAttack = true;
            attackCooldown = 0;
        }
    }

    private void Attack()
    {
        CheckAttackCooldown();

        if (IsAlive && canAttack && IsAttacking())
        {
            ExecuteAttack();

            canAttack = false;
        }
    }

    public virtual void ApplyDamage(float damage)
    {
        float leftover = shield - damage;

        if (leftover >= 0) {
            shield -= damage;
        } else
        {
            shield = 0;
            life += leftover;
        }

        UpdateStates();

        if (!IsAlive)
            Die();
    }

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        // Con esto puedo setear las animaciones por script
        animatorOverrideController = new AnimatorOverrideController(anim.runtimeAnimatorController);
        anim.runtimeAnimatorController = animatorOverrideController;

        animatorOverrideController["IDLE_DEFAULT"] = idleClip;
        animatorOverrideController["MOVING_DEFAULT"] = movingClip;
        animatorOverrideController["ATTACKING_DEFAULT"] = attackingClip;
        animatorOverrideController["DYING_DEFAULT"] = dyingClip;
        animatorOverrideController["DEAD_DEFAULT"] = deadClip;

        life = maxLife;
        shield = 0;

        canAttack = true;
        attackCooldown = 0;

        leftDir = anim.transform.localScale.x;
        rightDir = -anim.transform.localScale.x;
    }

    protected virtual void ExecuteUpdate() { }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!IsAlive || !canMove)
            return;
        
        UpdateStates();
        Move();
        Attack();
        ExecuteUpdate();
    }
}
