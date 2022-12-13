using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyNumber : MonoBehaviour
{
    bool ReadyToDestory = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyPopupCoroutine());
        
    }

    private void Update()
    {
        if(ReadyToDestory)
        {
            Destroy(gameObject);
        }
    }   

    IEnumerator DestroyPopupCoroutine()
    {
        yield return new WaitForSeconds(2f);
        ReadyToDestory = true;
        
    }
}
