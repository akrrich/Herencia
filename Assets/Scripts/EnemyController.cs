using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : CharacterController
{
    [Header("Target")]
    [SerializeField] CharacterController target;

    [Header("Sight")]
    [SerializeField] private float seekRadius;
    [SerializeField] private float meleeAttackRadius;
    [SerializeField] private float bulletAttackRadius;

    [Header("Damage")]
    [SerializeField] private float meleeDamage;

    [Header("Attack Type")]
    [SerializeField] private Attack attackType;
    public enum Attack
    {
        Melee,
        Bullet,
        Both
    }

    protected override void Start()
    {
        base.Start();
        target = GameManager.Instance.Victor;
    }
    protected override bool IsAttacking()
    {
        if (target == null || !target.IsAlive) 
            return false;

        float distance = (target.transform.position - this.transform.position).magnitude;

        if ((attackType is Attack.Melee || attackType is Attack.Both) && distance < meleeAttackRadius)
            return true;

        if ((attackType is Attack.Bullet || attackType is Attack.Both) && distance < bulletAttackRadius)
            return true;

        return false;
    }

    private void MeleeAttack()
    {
        target.ApplyDamage(meleeDamage);
        if(meleeSound != null)
            AudioSource.PlayClipAtPoint(meleeSound, transform.position);
    }
    private void BulletAttack()
    {
        Vector2 shootDirection = (target.transform.position - transform.position).normalized;

        BulletController newBullet = Instantiate(bullet, bulletSpawner.position, Quaternion.identity);

        newBullet.Init(this, shootDirection);
    }

    protected override void ExecuteAttack()
    {
        switch (attackType)
        {
            case Attack.Melee:
                MeleeAttack();
                break;

            case Attack.Bullet:
                BulletAttack();
                break;

            case Attack.Both:
                float distance = (target.transform.position - this.transform.position).magnitude;
                if (distance < meleeAttackRadius)
                    MeleeAttack();
                else 
                    BulletAttack();

                break;
        }
    }

    protected override void ExecuteMove()
    {
        rb.velocity = Vector2.zero;

        if (target == null || !target.IsAlive) return;

        if ((target.transform.position - this.transform.position).magnitude < seekRadius)
        {
            Vector2 moveTo = target.transform.position - transform.position;
            rb.velocity = moveTo.normalized * movementSpeed;
        }
    }
}

