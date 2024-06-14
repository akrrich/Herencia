using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MainCharacter : MonoBehaviour
{
    [SerializeField] private SliderController sliderLife;

    [SerializeField] private SliderController sliderShield;

    [SerializeField] private SliderController sliderCharacterSpeed;

    [SerializeField] private SliderController sliderBulletSpeed;

    [SerializeField] private ItemController itemLife;

    [SerializeField] private ItemController itemShield;

    [SerializeField] private ItemController itemCharacterSpeed;

    [SerializeField] private ItemController itemBulletSpeed;

    [SerializeField] private GameObject bulletPrefab;


    private AudioSource shootSound;

    private Rigidbody2D rb;

    private Animator anim;

    // Stats
    private const float maxLife = 10;
    private float life = 5;

    private const float maxShield = 10;
    private float shield = 0;

    private const float maxCharacterSpeed = 20;
    private float characterSpeed = 10f;

    private const float maxAttackSpeed = 30;
    private float attackSpeed = 2.5f;

    private float counterForShoot = 0;

    private float bulletSpeed = 30;

    private bool alive = true;

    private bool canShoot = true;
    private bool canShootAllTime = true;

    private float leftDir;
    private float rightDir;

    public bool HasLife { get => life > 0; }
    public bool HasShield { get => shield > 0; }

    public bool CanShootAllTime { get => canShootAllTime; set => canShootAllTime = value; }

    public enum Stats
    {
        LifePoints,
        Shield,
        characterSpeed,
        attackSpeed
    }


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

    public bool Alive
    {
        get
        {
            return alive;
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        shootSound = GetComponent<AudioSource>();

        sliderLife.InitializeBarStat(life, maxLife);
        sliderShield.InitializeBarStat(shield, maxShield);
        sliderCharacterSpeed.InitializeBarStat(characterSpeed, maxCharacterSpeed);
        sliderBulletSpeed.InitializeBarStat(attackSpeed, maxAttackSpeed);

        canShoot = true;
        canShootAllTime = true;

        leftDir = anim.transform.localScale.x;
        rightDir = -anim.transform.localScale.x;
        alive = true;

        UpdateStats();
    }


    private void Update()
    {
        CheckCanShoot();
        Movement();
        Shoot();

        CheckVictory();
    }

    private void CheckVictory()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            SceneManager.LoadScene("Victoria");
        }
    }
    private void CheckCanShoot()
    {
        if (canShoot)
            return;

        counterForShoot += Time.deltaTime;

        if (counterForShoot > (2 / attackSpeed))
        {
            canShoot = true;
            counterForShoot = 0;
        }
    }

    public void ApplyDamage(float damage)
    {
        if (HasShield)
        {
            shield -= damage;
        }

        else
        {
            life -= damage;
        }

        UpdateStats();

        if (life <= 0)
        {
            Die();
        }
    }

    private void GoToLoose()
    {
        SceneManager.LoadScene("Derrota");
    }
    private void Die()
    {
        alive = false;
        Invoke("GoToLoose", 0.8f);

    }

    private void UpdateStats()
    {
        sliderLife.ChangeActualValue(life);
        sliderShield.ChangeActualValue(shield);
        sliderCharacterSpeed.ChangeActualValue(characterSpeed);
        sliderBulletSpeed.ChangeActualValue(attackSpeed);
    }

    private void Movement()
    {
        rb.velocity = Vector2.zero;
        
        if (Alive)
        {
            Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            rb.velocity += movement * characterSpeed;

            float direccion = rb.velocity.x > 0 ? rightDir : leftDir;
            anim.transform.localScale = new Vector3(direccion, anim.transform.localScale.y, anim.transform.localScale.z);
        }
    }
    
    private void Shoot()
    {
        if (Alive && Input.GetMouseButton(0) && canShoot && canShootAllTime)
        {
            shootSound.Play();

            canShoot = false;
            counterForShoot = 0;

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;

            Vector2 shootDirection = (mousePosition - transform.position).normalized;

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();

            bulletRigidbody.gravityScale = 0f;
            bulletRigidbody.velocity = shootDirection * bulletSpeed;
        }
    }

    internal void PickUpItem(Stats stats, float points)
    {
        if (!Alive)
            return;

        switch (stats)
        {
            case Stats.LifePoints:
                this.life = Mathf.Clamp(this.life + points, 0, maxLife);
                break;

            case Stats.Shield:
                this.shield = Mathf.Clamp(this.shield + points, 0, maxShield); 
                break;

            case Stats.characterSpeed:
                this.characterSpeed = Mathf.Clamp(this.characterSpeed + points, 0, maxCharacterSpeed); 
                break;

            case Stats.attackSpeed:
                this.attackSpeed = Mathf.Clamp(this.attackSpeed + points, 0, maxAttackSpeed); 
                break;
        }

        UpdateStats();
    }
}
