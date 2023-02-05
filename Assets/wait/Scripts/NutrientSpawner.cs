using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NutrientSpawner : MonoBehaviour
{
    public GameObject nutrientPrefab;
    public GameObject activeNutrients;
    public CharacterController2D player;

    private bool isSpawning = false;

    public float spawnTime = 3f;
    
    void Start()
    {
        GameObject instance = Instantiate(nutrientPrefab, transform.position, Quaternion.identity);
        //make the parent of the nutrient the active nutrients object
        instance.transform.parent = activeNutrients.transform;

    }

    // Update is called once per frame
    void Update()
    {
        //count if active nutrients has 0 children
        if(activeNutrients.transform.childCount == 0 && !player.carryingNutrient && !isSpawning) {
            StartCoroutine(spawnNutrient());
        }
    }

    IEnumerator spawnNutrient() {
        isSpawning = true;
        yield return new WaitForSeconds(spawnTime);
        spawnTime = Mathf.Min(spawnTime + 0.5f, 7f);
        GameObject instance = Instantiate(nutrientPrefab, transform.position, Quaternion.identity);
        //make the parent of the nutrient the active nutrients object
        instance.transform.parent = activeNutrients.transform;
        isSpawning = false;
    }
}
