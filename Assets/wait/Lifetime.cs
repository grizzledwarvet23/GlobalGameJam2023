using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifetime : MonoBehaviour
{
    public float timeUntilDeathSeconds;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Die());
    }

    IEnumerator Die() {
        yield return new WaitForSeconds(timeUntilDeathSeconds);
        Destroy(gameObject);
    }

}
