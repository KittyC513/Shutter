using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solution : MonoBehaviour
{
    public GameObject[] obstacleCars;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("GENERADORDEOBASATUCULOS", 1f, 5f);
    }

    // Update is called once per frame
    private void GENERADORDEOBASATUCULOS()
    {
        int randomNum = Random.Range(0, 2);
        if(randomNum == 0)
        {
            Instantiate(obstacleCars[Random.Range(0, obstacleCars.Length)], new Vector3(-4, 31f, 0f), Quaternion.identity);
        }
        else
        {
            Instantiate(obstacleCars[Random.Range(0, obstacleCars.Length)], new Vector3(4, 31f, 0f), Quaternion.identity);
        }
    }
}
