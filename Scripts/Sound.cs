using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip hit_8bit;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if(audioSource == null)
        {

        }
        else
        {
            audioSource.clip = hit_8bit;
        }
    }


    public void EnemyGetHit()
    {
        audioSource.Play();
    }
}
