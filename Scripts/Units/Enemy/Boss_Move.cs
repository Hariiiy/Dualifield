using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Move : MonoBehaviour
{
    public Animator Anim;
    [SerializeField] Enemy_Behavior boss;
    [SerializeField] GameObject Bloody;
    [SerializeField] GameObject Skull;

    [SerializeField] float spawnDistance = 5f;

    void Start()
    {
        StartCoroutine(BossFight());
        playRangerAttack();
    }


    void Update()
    {
        
    }

    private void playSummon()
    {
        boss.InDizzy = true;
        Anim.Play("Summon");
        StartCoroutine(bossSkillDizzy());
        SummonBloods();
    }

    void SummonBloods()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector3 spawnPosition = transform.position;
            Vector2 spawnCircle = Random.insideUnitCircle;
            spawnCircle.Normalize();
            spawnCircle *= spawnDistance;

            spawnPosition.x = spawnCircle.x;
            spawnPosition.y = spawnCircle.y;

            Instantiate(Bloody, spawnPosition, Quaternion.identity);
        }   
    }

    IEnumerator SummonSkulls()
    {
        for (int i = 0; i < 6; i++)
        {
            yield return new WaitForSeconds(0.3f);
            Vector3 spawnPosition = transform.position;

            float randomx = Random.Range(-1.5f,1.5f);
            float randomy = Random.Range(-1.5f, 1.5f);
            spawnPosition.x = spawnPosition.x + randomx;
            spawnPosition.y = spawnPosition.y + randomy;

            Instantiate(Skull, spawnPosition, Quaternion.identity);
        }
    }


    private void playRangerAttack()
    {
        boss.InDizzy = true;
        Anim.Play("RangerAttack");
        StartCoroutine(bossSkillDizzy());
        boss.speed = 0;
        StartCoroutine( SummonSkulls());
    }

    IEnumerator bossSkillDizzy()
    {
        yield return new WaitForSeconds(2f);
        boss.InDizzy = false;
        boss.speed = 0.5f;
    }

    IEnumerator BossFight()
    {
        yield return new WaitForSeconds(10f);
        playSummon();

        yield return new WaitForSeconds(5f);
        playRangerAttack();

        yield return new WaitForSeconds(10f);
        playRangerAttack();

        yield return new WaitForSeconds(5f);
        playRangerAttack();

        yield return new WaitForSeconds(5f);
        playSummon();

        yield return new WaitForSeconds(15f);
        playSummon();

        yield return new WaitForSeconds(5f);
        playRangerAttack();

        yield return new WaitForSeconds(5f);
        playSummon();

        StartCoroutine(BossFight());
    }

}
