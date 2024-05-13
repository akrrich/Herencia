using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainCharacter : MonoBehaviour
{
    [SerializeField] private HudAllSliders sliderLife;

    [SerializeField] private HudAllSliders sliderShield;

    [SerializeField] private HudAllSliders sliderCharacterSpeed;

    [SerializeField] private HudAllSliders sliderBulletSpeed;


    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private AudioSource shoot;

    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private Animator anim;


    private float life = 5;

    private float shield = 0;

    private float characterSpeed = 10f;
    
    private float bulletSpeed = 15f;


    private float counterForShoot = 0;


    private bool canShoot = true;

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
        sliderShield.InitializeBarStat(shield, 4);
        sliderCharacterSpeed.InitializeBarStat(characterSpeed, 9);
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
            sliderLife.ChangeActualValue(life);
            sliderLife.LimitValue(ref life, 5, 1, 0);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            shield++;
            sliderShield.ChangeActualValue(shield);
            sliderShield.LimitValue(ref shield, 5, 1, 0);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            characterSpeed += 0.25f;
            sliderCharacterSpeed.ChangeActualValue(characterSpeed);
            sliderCharacterSpeed.LimitValue(ref characterSpeed, 9, 5, 5);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            bulletSpeed += 0.30f;
            sliderBulletSpeed.ChangeActualValue(bulletSpeed);
            sliderBulletSpeed.LimitValue(ref bulletSpeed, 20, 15, 15);
        }
    }

    // funcion de prueba para la vida y escudo
    private void DamageReceived(float damage)
    {
        if (shield > 0)
        {
            shield -= damage;
            sliderShield.ChangeActualValue(shield);
        }

        else
        {
            life -= damage;
            sliderLife.ChangeActualValue(life);
        }
    }


    private void DestroyMainCharacter()
    {
        if (life <= 0)
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
        if (Input.GetMouseButtonDown(0) && canShoot == true)
        {
            shoot.Play();

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

    public string tergo(string message)
    {
        return message;
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
