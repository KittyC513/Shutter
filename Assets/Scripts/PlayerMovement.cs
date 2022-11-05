using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement: MonoBehaviour
{

    public float groundDrag;


    Rigidbody rb;
    public Transform orientation;
    float horizontalInput;
    float verticalInput;
    public float speed;

    Vector3 moveDirection;

    GameObject respawn;

    public GameObject[] monsterPrefab;


    public float radius;

    public bool isSpawned = false;

    public float spawnTime;
    public float spawnDelay;

    public int startWait;
    public float spawnWait;

    public float minTime;
    public float maxTime;

    Vector3 randomPos;
    int randMonster;

    public int spawnCount;

    public int amountOfMonster;

    public AudioSource audioCamera;
    public AudioSource audioGhost;

    public GameObject monster;








    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        StartCoroutine(waitSpawner());
    }

    private void MovementInput()
    {
        //player movement
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");


    }

    // Update is called once per frame
    void Update()
    {
        MovementInput();
        rb.drag = groundDrag;

        respawn = GameObject.FindWithTag("Respawn");
        spawnWait = Random.Range(minTime, maxTime);

        if (Input.GetKeyDown(KeyCode.F))
        {
            audioCamera.Play();
            Debug.Log("Sounded");
        }

        monster = GameObject.FindWithTag("Monster");


    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        //calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * speed, ForceMode.Force);
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("Monster") && Input.GetKey(KeyCode.F))
        {
           
            Destroy(other.gameObject);

            //amountOfMonster--;
            Debug.Log("You Stun a Monster");
            //this.GetComponent<Health>().health--;
            StartCoroutine(waitSpawner());

            /*if (amountOfMonster <= 0)
            {
                StartCoroutine(waitSpawner());
            } */
        }


    }

    private void SpawnFunction()
    {

        randomPos = Random.insideUnitSphere * radius;
        randomPos += this.transform.position;
        randomPos.y = 0.3f;

        Vector3 direction = randomPos - this.transform.position;
        direction.Normalize();

        float dotProduct = Vector3.Dot(this.transform.forward, direction);
        float dotProductAngle = Mathf.Acos(dotProduct / this.transform.forward.magnitude * direction.magnitude);

        randomPos.x = Mathf.Cos(dotProductAngle) * radius + this.transform.position.x;
        randomPos.z = Mathf.Sin(dotProductAngle * (Random.value > 0.5 ? 1f : -1f)) * radius + this.transform.position.z;

    }


    public IEnumerator waitSpawner()
    {

        yield return new WaitForSeconds(startWait);
        SpawnFunction();

        for (int count = spawnCount; count > 0; count--)
        {
            GameObject monster = Instantiate(monsterPrefab[randMonster], randomPos, Quaternion.identity);
            monster.transform.position = randomPos;
            audioGhost.Play();
            StartCoroutine(SelfDestruct());
            //amountOfMonster++;
            //isSpawned = true;
            yield return new WaitForSeconds(spawnWait);
        }

    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(5f);

        //playerMovement.StartCoroutine(playerMovement.waitSpawner());

        Destroy(monster.gameObject);
        StartCoroutine(waitSpawner());
    }
}
