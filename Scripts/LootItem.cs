using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootItem : MonoBehaviour
{
    bool trackPlayer;
    public int trackArea = 3;
    bool PlayerInTrackArea;
    const float trackDelateTime = 1f;
    int trackSpeed = 3;
    GameObject Player;


    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(SpawnCorpseCoroutine());
    }

    private void Update()
    {
        if(gameObject.CompareTag("Gold") || gameObject.CompareTag("Exp"))
        {
            if (Vector3.Distance(Player.transform.position, transform.position) < trackArea && trackPlayer)
            {
                Vector3 destination = Player.transform.position;
                Vector3 source = transform.position;

                Vector3 direction = destination - source;
                direction.Normalize();

                transform.position += direction * trackSpeed * Time.deltaTime;
            }
        }
    }


    IEnumerator SpawnCorpseCoroutine()
    {
        yield return new WaitForSeconds(trackDelateTime);
        trackPlayer = true;
    }

}
