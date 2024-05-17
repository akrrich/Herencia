using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    [SerializeField] private AudioSource shootSound;

    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private Animator anim;


    private float life = 5;

    private float shield = 0;

    private float characterSpeed = 10f;
    
    private float bulletSpeed = 15f;


    private float counterForShoot = 0;


    private bool canShoot = true;
    public bool canShootAllTime = true;

    private bool hasLife = true;
    private bool hasShield = false;


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

    public float Life
    {
        get
        {
            return life;
        }
    }


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        sliderLife.InitializeBarStat(life, 5);
        sliderShield.InitializeBarStat(shield, 5);
        sliderCharacterSpeed.InitializeBarStat(characterSpeed, 15);
        sliderBulletSpeed.InitializeBarStat(bulletSpeed, 20);
    }


    void Update()
    {
        counterForShoot += Time.deltaTime;

        if (counterForShoot > 0.2) 
        {
            canShoot = true;
        }

        DestroyMainCharacter();
        movements();
        Shoot();
        UpdateStatsAllTime();
        IncreaseStatValue();
        CheckIfHasLifeOrShield();
        pruebaDeSliders();
    }


    // funcion de prueba para la vida y escudo
    private void pruebaDeSliders()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            DamageReceived(1);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            life++;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            shield++;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            characterSpeed += 0.25f;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            bulletSpeed += 0.30f;
        }
    }


    private void DamageReceived(float damage)
    {
        if (shield > 0)
        {
            shield -= damage;
        }

        else
        {
            life -= damage;
        }
    }

    private void UpdateStatsAllTime()
    {
        sliderLife.ChangeActualValue(life);
        sliderLife.LimitValue(ref life, 5, 1, 0);

        sliderShield.ChangeActualValue(shield);
        sliderShield.LimitValue(ref shield, 5, 1, 0);

        sliderCharacterSpeed.ChangeActualValue(characterSpeed);
        sliderCharacterSpeed.LimitValue(ref characterSpeed, 15, 10, 10);

        sliderBulletSpeed.ChangeActualValue(bulletSpeed);
        sliderBulletSpeed.LimitValue(ref bulletSpeed, 20, 15, 15);
    }


    private void IncreaseStatValue()
    {
        itemLife.ChangeStatValue(ref life);
        itemShield.ChangeStatValue(ref shield);
        itemCharacterSpeed.ChangeStatValue(ref characterSpeed);
        itemBulletSpeed.ChangeStatValue(ref bulletSpeed);
    }

    private void DestroyMainCharacter()
    {
        if (life < 1)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void movements()
    {
        rb.velocity = Vector2.zero;

        Vector2 movement = new Vector2(Input.GetAxisRaw("Vertical"), Input.GetAxisRaw("Horizontal")).normalized;

        rb.velocity += movement.x * characterSpeed * Vector2.up;
        rb.velocity += movement.y * characterSpeed * Vector2.right;
    }
    
    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && canShoot == true && canShootAllTime == true)
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

    private void CheckIfHasLifeOrShield()
    {
        if (life > 0 && shield < 1)
        {
            hasLife = true;
            hasShield = false;
        }

        else if (life > 0 && shield > 0)
        {
            hasShield = true;
        }
    }
}
