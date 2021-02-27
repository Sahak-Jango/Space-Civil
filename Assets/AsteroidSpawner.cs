using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    const float OFFSET_FROM_SCREEN = 10f;

    [SerializeField]
    GameObject asteroidPrefab;  

    float spawn_axis_y;
    
    [SerializeField]
    float spawnTime;

    [SerializeField]
    float curTime;

    void Start()
    {
        SetSpawnAxis_Y();
        curTime = spawnTime;
    }

    void SetSpawnAxis_Y()
    {
        spawn_axis_y = Camera.main.orthographicSize + OFFSET_FROM_SCREEN;
    }

    void Update() 
    {
        RandomSpawn();
    }

    void RandomSpawn()
    {
        if(curTime >= spawnTime)
        {
            curTime -= spawnTime;

            float spawn_axis_x = Random.Range(-Camera.main.orthographicSize, Camera.main.orthographicSize);

            Instantiate(asteroidPrefab, new Vector3(spawn_axis_x, spawn_axis_y, 0f), Quaternion.identity);
        }

        curTime += Time.deltaTime;
    }



}
