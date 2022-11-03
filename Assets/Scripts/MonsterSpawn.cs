using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterSpawn : MonoBehaviour
{
    public GameObject[] monsterPrefab;
    public GameObject player;

    public float radius;

    public bool isSpawned;

    public float spawnTime;
    public float spawnDelay;

    public int startWait;
    public float spawnWait;

    public float minTime;
    public float maxTime;

    Vector3 randomPos;
    int randMonster;

    public int spawnCount;


    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine(waitSpawner());
    }

    // Update is called once per frame
    void Update()
    {

        spawnWait = Random.Range(minTime, maxTime);

    }


    private void SpawnFunction()
    {

        randomPos = Random.insideUnitSphere * radius;
        randomPos += player.transform.position;
        randomPos.y = 0.3f;

        Vector3 direction = randomPos - player.transform.position;
        direction.Normalize();

        float dotProduct = Vector3.Dot(player.transform.forward, direction);
        float dotProductAngle = Mathf.Acos(dotProduct / player.transform.forward.magnitude * direction.magnitude);

        randomPos.x = Mathf.Cos(dotProductAngle) * radius + player.transform.position.x;
        randomPos.z = Mathf.Sin(dotProductAngle * (Random.value > 0.5 ? 1f : -1f)) * radius + player.transform.position.z;

    }



    IEnumerator waitSpawner()
    {

        yield return new WaitForSeconds(startWait);
        SpawnFunction();
       
        for (int count = spawnCount; count > 0; count--)
        {
            GameObject monster = Instantiate(monsterPrefab[randMonster], randomPos, Quaternion.identity);
            monster.transform.position = randomPos;
            yield return new WaitForSeconds(spawnWait);
        }

    }
}
    

