using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NutrientSpawner : MonoBehaviour
{
    public GameObject[] nutrientPrefabs;
    public GameObject activeNutrients;
    public CharacterController2D player;

    private bool isSpawning = false;

    public float spawnTime = 3f;
    
    void Start()
    {
        //randomly choose a nutrient prefab to spawn
        int random = Random.Range(0, 4);
        GameObject instance = Instantiate(nutrientPrefabs[random], transform.position, Quaternion.identity);
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
        //randomly choose a nutrient prefab to spawn
        int random = Random.Range(0, 4);
        GameObject instance = Instantiate(nutrientPrefabs[random], transform.position, Quaternion.identity);
        //make the parent of the nutrient the active nutrients object
        instance.transform.parent = activeNutrients.transform;
        isSpawning = false;
    }

    IEnumerator spawnNutrient(int index) {
        isSpawning = true;
        yield return new WaitForSeconds(spawnTime);
        spawnTime = Mathf.Min(spawnTime + 0.5f, 7f);
        //randomly choose a nutrient prefab to spawn
        GameObject instance = Instantiate(nutrientPrefabs[index], transform.position, Quaternion.identity);
        //make the parent of the nutrient the active nutrients object
        instance.transform.parent = activeNutrients.transform;
        isSpawning = false;
    }
}
