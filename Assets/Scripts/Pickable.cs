using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerStay(Collider other)
    {
        //Input.GetKeyDown(KeyCode.F)
        if (other.gameObject.tag.Equals("Items") && Input.GetKey(KeyCode.F))
        {
            player.GetComponent<Collection>().collection--;
            Destroy(other.gameObject);  
            Debug.Log("Collection");

        }

    }
}
