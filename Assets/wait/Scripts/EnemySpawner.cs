using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject enemyPrefab;
    public GameObject activeEnemies;

    public float spawnDelay = 5;

    private bool canSpawn = true;

    private int activeEnemyMax = 2;

    public Roots treeRoots;
    
    void Start()
    {
        SpawnEnemy();
    }

    void Update()
    {
        if (canSpawn && activeEnemies.transform.childCount < activeEnemyMax) {
            StartCoroutine(SpawnEnemy());
        }
    }

    public IEnumerator SpawnEnemy() {
        canSpawn = false;
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        yield return new WaitForSeconds(2);
        // GameObject instance = Instantiate(enemyPrefab, spawnPoints[spawnPointIndex].position, Quaternion.identity);
        //instantiate it and also consider the spawn points rotation
        GameObject instance = Instantiate(enemyPrefab, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        if(spawnPointIndex < 2) {
            instance.GetComponent<RabbitEnemy>().movingRight = true;
        } else {
            instance.GetComponent<RabbitEnemy>().movingRight = false;
        }

        if(treeRoots.progress >= 25) {
            activeEnemyMax = 3;
        }
        if(treeRoots.progress >= 50) {
            activeEnemyMax = 4;
        }
        if(treeRoots.progress >= 75) {
            //increase rabbit speed
            instance.GetComponent<RabbitEnemy>().speed = 2;
        }
         
        //make the parent of the enemy the active enemies object
        instance.transform.parent = activeEnemies.transform;
        StartCoroutine(setCanSpawn(true));
    }

    IEnumerator setCanSpawn(bool canSpawn = true) {
        yield return new WaitForSeconds(spawnDelay);
        this.canSpawn = canSpawn;
    }
}
