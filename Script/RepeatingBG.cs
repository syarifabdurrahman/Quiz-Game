using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBG : MonoBehaviour
{
    private float length, startpos;
    public GameObject cam;
    public float ParallaxFX;

    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;

    }


    private void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - ParallaxFX));
        float distance = (cam.transform.position.x * ParallaxFX);

        transform.position = new Vector3(startpos + distance, transform.position.y, transform.position.z);

        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
