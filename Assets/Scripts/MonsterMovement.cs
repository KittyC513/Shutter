using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterMovement: MonoBehaviour
{

    public Transform Player;

    private NavMeshAgent agent;
    PlayerMovement playerMovement;



    //AudioSource bushAudio;
    // Start is called before the first frame update
    void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();
        Player = GameObject.FindWithTag("Player").transform;
        //bushAudio = this.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(Player.position);


    }


    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag.Equals("Frame") && Input.GetKeyDown(KeyCode.F))
        {
            Destroy(this.gameObject);
           

        }

        /*if (other.gameObject.tag.Equals("Walls"))
        {
            bushAudio.Play();
            Debug.Log("Bush Sounds");
        }*/

        
    }


    /*IEnumerator SelfDestruct()
    {
        
        yield return new WaitForSeconds(5f);
        
        //playerMovement.StartCoroutine(playerMovement.waitSpawner());

        Destroy(gameObject);

    }
    */

}
