using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject[] TheEnemies;
    public float Count = 0;
    float randomNumber;
    public Transform EnemySpawn;
    public Transform Generation;
    // Start is called before the first frame update
    void Start()
    {
        randomNumber = Random.Range(3, 4);
    }

    // Update is called once per frame
    void Update()
    {      
        if (Count == randomNumber)
        {
            Count = 0;
            randomNumber = Random.Range(3, 4);
            Instantiate(TheEnemies[Random.Range(0, TheEnemies.Length)], EnemySpawn.position, EnemySpawn.rotation);
        }
    }

}