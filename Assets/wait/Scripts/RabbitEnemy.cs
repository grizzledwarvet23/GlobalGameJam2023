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
    
    void Start() {
        //stop the blood animations from playing
        bloodAnimatorOne.enabled = false;
        bloodAnimatorTwo.enabled = false;
        root = GameObject.Find("Roots").GetComponent<Roots>();
    }

    // Update is called once per frame
    void Update()
    {
        //move the enemy towards the x direction it is facing
        if(movingRight && transform.position.x < leftBoundary || !movingRight && transform.position.x > rightBoundary) {
            transform.position += transform.right * speed * Time.deltaTime;    
        } else if(!startedAttacking) {
            //this means the enemy has reached the boundary, and is attacking the root
            StartCoroutine(attackRoot());
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
}
