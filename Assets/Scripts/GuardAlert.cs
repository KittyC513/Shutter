using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardAlert : MonoBehaviour
{
    public bool isAlerted = false;
    public GameObject diamond;
    public GameObject diamondSlot;
    NavMeshAgent agent;

    public void Awake()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }
    public void Update()
    {
        // guard goes to diamond slot to check once it's stole
        if (isAlerted)
        {
            agent.SetDestination(diamondSlot.transform.position);

        }
    }
    
}
