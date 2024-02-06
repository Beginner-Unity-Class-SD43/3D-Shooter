using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject zombiePrefab; // The zombie gameobject that we spawn

    [SerializeField]
    float spawnInterval = 5f; // The amount of time between each zombie spawn

    Enemy[] enemies; // List of enemies

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        enemies = FindObjectsOfType<Enemy>(); // Update the list of current enemies
    }

    IEnumerator SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(zombiePrefab, transform.position, Quaternion.identity); // Instantiate the zombie
        yield return new WaitForSeconds(spawnInterval); // Wait for seconds
        yield return new WaitUntil(() => enemies.Length < 25);
        StartCoroutine(SpawnEnemy()); // Start the code all over again
    }
}
