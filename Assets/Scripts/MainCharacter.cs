using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainCharacter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private AudioSource shoot;

    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private Animator anim;


    private int life = 3;

    private float characterSpeed = 5f;
    private float bulletSpeed = 15f;
    private float counterForShoot = 0;

    private bool canShoot = true;


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

    public int Life
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
    }


    void Update()
    {
        counterForShoot += Time.deltaTime;

        if (counterForShoot > 0.2) 
        {
            canShoot = true;
        }

        movements();
        Shoot();
    }


    private void movements()
    {
        rb.velocity = Vector2.zero;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            float verticalInput = Input.GetKey(KeyCode.W) ? 1 : -1;
            rb.velocity += verticalInput * characterSpeed * Vector2.up;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            float horizontalInput = Input.GetKey(KeyCode.D) ? 1 : -1;
            rb.velocity += horizontalInput * characterSpeed * Vector2.right;
        }
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
}
