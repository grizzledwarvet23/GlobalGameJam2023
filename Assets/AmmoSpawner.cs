using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSpawner : MonoBehaviour
{
    public GameObject ammoPackPrefab;
    private bool isSpawning = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnAmmoPack(0));
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.childCount == 0 && !isSpawning) {
            StartCoroutine(SpawnAmmoPack(10));
        }
    }

    IEnumerator SpawnAmmoPack(float inTimeSeconds) {
        isSpawning = true;
        yield return new WaitForSeconds(inTimeSeconds);
        Instantiate(ammoPackPrefab, transform.position, Quaternion.identity, transform);
        isSpawning = false;
    }
}
