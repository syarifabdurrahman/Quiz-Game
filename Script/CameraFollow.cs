using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public PlayerController Theplayer;
    private Vector3 Lastplayerpossition;
    private Transform Target;
    private float distanceofmove;
    private float distanceOfJumpforce;

    // Start is called before the first frame update
    void Start()
    {
        Theplayer = FindObjectOfType<PlayerController>();
        Lastplayerpossition = Theplayer.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        distanceofmove = Theplayer.transform.position.x - Lastplayerpossition.x;
        transform.position = new Vector3(transform.position.x + distanceofmove, transform.position.y + distanceOfJumpforce, transform.position.z);
        Lastplayerpossition = Theplayer.transform.position;
    } 
}
