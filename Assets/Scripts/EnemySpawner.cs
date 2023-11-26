using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefab;

    [SerializeField] private float minimumSpawnTime;
    [SerializeField] private float maximumSpawnTime;
    private int random;

    private float timeUntilSpawn;
    void Awake()
    {
        SetTimeUntilSpawn();
        random = Random.Range(0, enemyPrefab.Length);
    }

    // Update is called once per frame
    void Update()
    {
        timeUntilSpawn -= Time.deltaTime;

        if(timeUntilSpawn <= 0)
        {
            Instantiate(enemyPrefab[random], transform.position, Quaternion.identity);
            SetTimeUntilSpawn();
        }
    }

    private void SetTimeUntilSpawn()
    {
        timeUntilSpawn = Random.Range(minimumSpawnTime, maximumSpawnTime);
    }
}
