using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SublevelController : MonoBehaviour
{
    public GameObject[] Sublevel;
    public GameManager GameManager;
    public Animator Anim;

    private int Index=1;

    private void Start()
    {
        
    }

    private void Update()
    {
        Level();
    }

    void Level()
    {
        while (GameManager.changeLevel)
        {
            //StartCoroutine(FadeEffect());
            Instantiate(Sublevel[Index],transform);
            Index++;
        }
    }

    //IEnumerator FadeEffect()
    //{
    //    //Anim.SetTrigger();

    //}
}
