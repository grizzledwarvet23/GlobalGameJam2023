using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nutrient : MonoBehaviour
{
    bool playerColliding = false;

    bool canBePickedUp = false;

    void Start() {
        StartCoroutine(SetCanPickUp());
    }

    IEnumerator SetCanPickUp() {
        yield return new WaitForSeconds(1f);
        canBePickedUp = true;
    }


    void Update() {
        if (playerColliding) {
            if ( canBePickedUp && (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(1)) ) {
                //make the player carry the nutrient
                GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController2D>().CarryNutrient();
                Destroy(gameObject);
            }
        }
    }
    //do an on trigger enter to check if the player is colliding with the nutrient, if so make the player carry the nutrient, thats it
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            playerColliding = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            playerColliding = false;
        }
    }
}
