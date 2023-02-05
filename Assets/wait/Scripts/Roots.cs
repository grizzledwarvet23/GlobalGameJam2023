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


    public void takeDamage(int damage) {
        health -= damage;
        healthBar.value = health/maxHealth;
        if (health <= 0) {
        //    Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<CharacterController2D>().carryingNutrient) {
            progress += 5;
            progressBar.value = progress/100.0f;
            other.gameObject.GetComponent<CharacterController2D>().DepositNutrient();
        }
    }


}