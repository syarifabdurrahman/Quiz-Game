using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject[] ThePlatform;
    public Transform Generation;
    public float DistanceBetween;
    public EnemyGenerator enemyGenerator;
    private float platformWidth;

    private void Start()
    {
        for (int i = 0; i < ThePlatform.Length; i++)
        {
            platformWidth = ThePlatform[i].GetComponent<BoxCollider2D>().size.x;

        }
    }

    private void Update()
    {
        if (transform.position.x < Generation.position.x)
        {
            transform.position = new Vector3(transform.position.x + platformWidth + DistanceBetween, transform.position.y, transform.position.z);
            Instantiate(ThePlatform[Random.Range(0, ThePlatform.Length)], transform.position, transform.rotation);
            enemyGenerator.Count++;
        }
        
    }
}
