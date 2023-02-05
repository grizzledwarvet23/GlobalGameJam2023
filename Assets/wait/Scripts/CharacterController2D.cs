using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    [System.NonSerialized]
    public bool canDrop;

    
    private int ammo = 50;
    private const int maxAmmo = 50;
    public TextMeshProUGUI ammoText;

    public LayerMask groundMask;

    public float shootDelay = 0.25f;
    public Transform firePoint;
    public GameObject bullet;

    public GameObject nutrient;
    public SpriteRenderer redNutrientRenderer;

    public GameObject activeNutrients;

    public AudioSource gunSound;

    private Animator animator;

    private bool facingRight;

    public Transform handSpriteAxis;
    public SpriteRenderer gunSprite;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canShoot = true;
        carryingNutrient = false;
        facingRight = true;
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        if(horizontal != 0) {
            animator.SetBool("isRunning", true);
        } else {
            animator.SetBool("isRunning", false);
        }
        if(carryingNutrient) {
            Vector2 movement = new Vector2(horizontal * carrySpeed, rb.velocity.y);
            rb.velocity = movement;
        } else {
            Vector2 movement = new Vector2(horizontal * speed, rb.velocity.y);
            rb.velocity = movement;
        }

        if (horizontal > 0 && !facingRight) {
            Flip();
        } else if (horizontal < 0 && facingRight) {
            Flip();
        }
        

    }

    void Flip() {
        facingRight = !facingRight;
        //then rotate instead of using scale
        transform.Rotate(0f, 180f, 0f);
    }

    void Update() {
        if (isGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

        Vector2 aimDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        //do aim direction, but just flip if the gun is facing left


        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        handSpriteAxis.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        if(angle >= 90  || angle <= -90) {
            handSpriteAxis.transform.eulerAngles = new Vector3(180, 0, -handSpriteAxis.transform.eulerAngles.z);
        } else {
            handSpriteAxis.transform.eulerAngles = new Vector3(0, 0, handSpriteAxis.transform.eulerAngles.z);
        }

        if(handSpriteAxis.transform.localEulerAngles.y >= 178 && handSpriteAxis.transform.localEulerAngles.y <= 182) {
            //behind the back
            gunSprite.sortingOrder = 0;
        } else {
            gunSprite.sortingOrder = 2;
        }



        if ( (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Z)) && canShoot && !carryingNutrient && ammo > 0)
        {
            canShoot = false;
            isShooting = true;
            Vector2 shootDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
            Shoot(shootDirection);
            StartCoroutine(enableShoot());
        }

        if(carryingNutrient && canDrop && (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(1))) {
            DropNutrient();
        }
    }

    void Shoot(Vector2 direction)
    {
        GameObject projectile = Instantiate(bullet, firePoint.position, firePoint.rotation);
        gunSound.Play();
        projectile.GetComponent<Rigidbody2D>().AddForce(direction.normalized * shootForce, ForceMode2D.Impulse);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        ammo--;
        ammoText.text = ammo.ToString();
    }

    IEnumerator enableShoot() {
        yield return new WaitForSeconds(shootDelay);
        canShoot = true;
    }

    bool isGrounded() {
        //create raycast to check if the player is grounded, if the raycast hits something with tag "Ground", the player is grounded. use the ground layer mask
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 2.5f, groundMask);
        if (hit.collider != null && hit.collider.CompareTag("Ground"))
        {
            animator.SetBool("isJumping", false);
            return true;
        }
        animator.SetBool("isJumping", true);
        return false;
    }

    public void Die() {

    }

    public void Reload() {
        ammo = maxAmmo;
        ammoText.text = ammo.ToString();
    }


    public void CarryNutrient() {
        canDrop = false;
        carryingNutrient = true;
        redNutrientRenderer.enabled = true;
        gunSprite.enabled = false;
        StartCoroutine(setDroppable());
    }

    IEnumerator setDroppable() {
        yield return new WaitForSeconds(1f);
        canDrop = true;
    }

    public void DropNutrient() {
        carryingNutrient = false;
        redNutrientRenderer.enabled = false;
        GameObject instance = Instantiate(nutrient, transform.position, Quaternion.identity);
        gunSprite.enabled = true;
        instance.transform.parent = activeNutrients.transform;
        
    }

    public void DepositNutrient() {
        carryingNutrient = false;
        redNutrientRenderer.enabled = false;
        gunSprite.enabled = true;
    }

    //on draw gizmos to show ground check raycast
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - 2.5f));
    }
}
