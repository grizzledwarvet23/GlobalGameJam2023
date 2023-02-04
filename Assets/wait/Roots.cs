using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roots : MonoBehaviour
{
    public int health = 100;
    private const int maxHealth = 100;
    public int progress = 0;


    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<CharacterController2D>().carryingNutrient) {
            progress += 5;
            other.gameObject.GetComponent<CharacterController2D>().DropNutrient();
        }
    }


}
