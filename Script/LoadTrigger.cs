using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTrigger : MonoBehaviour
{
    public string LoadName;
    public string UnloadName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LoadName != "")
        {
            GameManager.Instance.Load(LoadName);
        }
        if (UnloadName != "")
        {
            StartCoroutine("UnloadScene");
        }
    }
    
    IEnumerator UnloadScene()
    {
        yield return new WaitForSeconds(.10f);
        GameManager.Instance.Unload(UnloadName);
    }
}
