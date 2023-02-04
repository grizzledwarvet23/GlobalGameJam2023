using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nutrient : MonoBehaviour
{

    //do an on trigger enter to check if the player is colliding with the nutrient, if so make the player carry the nutrient, thats it
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<CharacterController2D>().CarryNutrient();
            Destroy(gameObject);
        }
    }
}
