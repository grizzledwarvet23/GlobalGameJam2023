using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitEnemy : MonoBehaviour
{
    public int health = 5;
    public float speed;

    public bool movingRight;
    public float leftBoundary;
    public float rightBoundary;

    public Roots root;

    bool startedAttacking = false;

    public Animator bloodAnimatorOne;
    public Animator bloodAnimatorTwo;

    public Transform touchDamageCheck;
    public float touchDamageWidth;
    public float touchDamageHeight;
    public LayerMask whatIsPlayer;
    Vector2 touchDamageBotLeft;
    Vector2 touchDamageTopRight;
    
    void Start() {
        //stop the blood animations from playing
        bloodAnimatorOne.enabled = false;
        bloodAnimatorTwo.enabled = false;
        root = GameObject.Find("Roots").GetComponent<Roots>();
    }

    // Update is called once per frame
    void Update()
    {
        checkPlayerCollision();
        //move the enemy towards the x direction it is facing
        if(movingRight && transform.position.x < leftBoundary || !movingRight && transform.position.x > rightBoundary) {
            transform.position += transform.right * speed * Time.deltaTime;    
        } else if(!startedAttacking) {
            //this means the enemy has reached the boundary, and is attacking the root
            StartCoroutine(attackRoot());
        }
        
    }

    void checkPlayerCollision() {
        touchDamageBotLeft.Set(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
        touchDamageTopRight.Set(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));
        Collider2D hit = Physics2D.OverlapArea(touchDamageBotLeft, touchDamageTopRight, whatIsPlayer);
        if(hit != null) {
            hit.GetComponent<CharacterController2D>().takeDamage(1);
        }

    }

    IEnumerator attackRoot() {
        startedAttacking = true;
        while(true) {
            root.takeDamage(1);
            yield return new WaitForSeconds(1);
        }
    }

    public void takeDamage(int damage) {
        health--;
        bloodAnimatorOne.enabled = true;
        bloodAnimatorTwo.enabled = true;
        //randomly choose which blood animation to play
        if(Random.Range(0, 2) == 0) {
            bloodAnimatorOne.Play("bloodsplatter");
        } else {
            bloodAnimatorTwo.Play("bloodsplatter");
        }

        if (health <= 0) {
            Destroy(gameObject);
        }

    }

    public void OnDrawGizmos()
    {
        touchDamageBotLeft.Set(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
        Vector2 botLeft = new Vector2(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
        Vector2 botRight = new Vector2(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
        Vector2 topRight = new Vector2(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));
        Vector2 topLeft = new Vector2(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));

 
        Gizmos.DrawLine(botLeft, botRight);
        Gizmos.DrawLine(botRight, topRight);
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, botLeft);

    }
}
