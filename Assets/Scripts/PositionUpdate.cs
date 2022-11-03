using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionUpdate : MonoBehaviour
{

    public GameObject player;
    PlayerMovement playerMovement;


    // Start is called before the first frame update
    void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = player.transform.position;
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Monster"))
        {

            Destroy(other.gameObject);
            Debug.Log("You are attacked");
            player.GetComponent<Health>().health--;
            playerMovement.StartCoroutine(playerMovement.waitSpawner());
            
        }
        
    }
}
