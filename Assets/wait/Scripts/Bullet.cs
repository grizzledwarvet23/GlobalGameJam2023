using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public AudioSource damageSound;
    public AudioSource killSound;
    public float timeUntilDeathSeconds;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Die(timeUntilDeathSeconds));
    }

    IEnumerator Die(float time) {
        yield return new WaitForSeconds(time);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(1f); //wait for the sound to finish playing
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy") {
            if(other.gameObject.GetComponent<RabbitEnemy>().health == 1) {
                killSound.Play();
            } else {
                damageSound.Play();
            }
            other.gameObject.GetComponent<RabbitEnemy>().takeDamage(1);
            // damageSound.Play();
            StartCoroutine(Die(0.1f));
            
        }
        if(other.gameObject.tag == "Ground") {
            StartCoroutine(Die(0.05f));
        }
    }

}
