using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public float speed = 10f;
    public float carrySpeed = 7f;
    public float jumpForce = 10f;
    public float shootForce = 10f;
    private Rigidbody2D rb;
    private bool isJumping;
    private bool isShooting;
    private bool canShoot;
    [System.NonSerialized]
    public bool carryingNutrient;

    public LayerMask groundMask;

    public float shootDelay = 0.25f;
    public Transform firePoint;
    public GameObject bullet;

    public SpriteRenderer redNutrientRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canShoot = true;
        carryingNutrient = false;
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        if(carryingNutrient) {
            Vector2 movement = new Vector2(horizontal * carrySpeed, rb.velocity.y);
            rb.velocity = movement;
        } else {
            Vector2 movement = new Vector2(horizontal * speed, rb.velocity.y);
            rb.velocity = movement;
        }
        

    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

        if (Input.GetButton("Fire1") && canShoot && !carryingNutrient)
        {
            canShoot = false;
            isShooting = true;
            Vector2 shootDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
            Shoot(shootDirection);
            StartCoroutine(enableShoot());
        }
    }

    void Shoot(Vector2 direction)
    {
        GameObject projectile = Instantiate(bullet, firePoint.position, firePoint.rotation);
        projectile.GetComponent<Rigidbody2D>().AddForce(direction.normalized * shootForce, ForceMode2D.Impulse);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    IEnumerator enableShoot() {
        yield return new WaitForSeconds(shootDelay);
        canShoot = true;
    }

    bool isGrounded() {
        //create raycast to check if the player is grounded, if the raycast hits something with tag "Ground", the player is grounded. use the ground layer mask
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.5f, groundMask);
        if (hit.collider != null && hit.collider.CompareTag("Ground"))
        {
            return true;
        }
        return false;
    }


    public void CarryNutrient() {
        carryingNutrient = true;
        redNutrientRenderer.enabled = true;
    }

    public void DropNutrient() {
        carryingNutrient = false;
        redNutrientRenderer.enabled = false;
    }
}
