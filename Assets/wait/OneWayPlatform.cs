using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    bool playerOnPlatform;
    PlatformEffector2D effector;
    public LayerMask withoutPlayer; 
    private LayerMask defaultMask;

    //check if the player is on the platform

    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
        defaultMask = effector.colliderMask;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = true;
        }
    }

    void Update() {
        //if the player is on the platform and the player presses down, the player layer mask will be ignored
        if (Input.GetKeyDown(KeyCode.S) && playerOnPlatform)
        {
            playerOnPlatform = false;
            StartCoroutine(passPlayerThrough());
        }
    }

    IEnumerator passPlayerThrough() {
        effector.colliderMask = withoutPlayer;
        tag = "Untagged";
        yield return new WaitForSeconds(0.5f);
        effector.colliderMask = defaultMask;
        tag = "Ground";
    }


}
