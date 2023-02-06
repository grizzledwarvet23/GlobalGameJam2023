using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Roots : MonoBehaviour
{
    public int health = 100;
    private const float maxHealth = 100.0f;
    public int progress = 0;
    public Slider progressBar;
    public Slider healthBar;
    public CharacterController2D player;

    private Animator animator;


    void Start() {
        animator = GetComponent<Animator>();
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.P)) {
            progress += 25;
            if(progress == 25) {
                animator.Play("growth1");
            }
            else if(progress == 50) {
                animator.Play("growth2");
            }
            else if (progress == 75) {
                animator.Play("growth3");
            }
        }
    }

    public void takeDamage(int damage) {
        animator = GetComponent<Animator>();
        health -= damage;
        healthBar.value = health/maxHealth;
        if (health <= 0) {
            player.Die();
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<CharacterController2D>().carryingNutrient) {
            progress += 5;
            progressBar.value = progress/100.0f;
            other.gameObject.GetComponent<CharacterController2D>().DepositNutrient();

            if(progress == 25) {
                animator.Play("growth1");
            }
            else if(progress == 50) {
                animator.Play("growth2");
            }
            else if (progress == 75) {
                animator.Play("growth3");
            }

            if(progress >= 100) {
                //win
                WinGame();
            }
        }
    }

    public void WinGame() {
        //set scene to main menu for now
        UnityEngine.SceneManagement.SceneManager.LoadScene("WinScreen");
    }


}
