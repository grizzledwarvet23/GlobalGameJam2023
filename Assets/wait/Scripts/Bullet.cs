using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float timeUntilDeathSeconds;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Die(timeUntilDeathSeconds));
    }

    IEnumerator Die(float time) {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy") {
            other.gameObject.GetComponent<RabbitEnemy>().takeDamage(1);
            StartCoroutine(Die(0.1f));
        }
        if(other.gameObject.tag == "Ground") {
            StartCoroutine(Die(0.05f));
        }
    }

}
