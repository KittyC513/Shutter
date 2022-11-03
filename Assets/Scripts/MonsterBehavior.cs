using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBehavior : MonoBehaviour
{
    BehaviourTree monsterTree;
    public GameObject itemPos01;
    public GameObject itemPos02;
    public GameObject itemPos03;
    public GameObject player;


    public float distance;

    public float dagerousRange;

    Vector3 playerPos;

    NavMeshAgent agent;

    //create action states of Patrolling and Chasing
    public enum ActionState { IDLE, WORKING }
    
    ActionState state = ActionState.IDLE;
    Node.Status treeStatus = Node.Status.RUNNING;

    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();

        monsterTree = new BehaviourTree();
        Sequence patrol = new Sequence("patrolling around items");

        //patrolling acion around items
        Leaf goToItem01 = new Leaf("Go To Iteam01", GoToItem01);
        Leaf goToItem02 = new Leaf("Go To Iteam02", GoToItem02);
        Leaf goToItem03 = new Leaf("Go To Iteam03", GoToItem03);
        /*Leaf goToPatrol = new Leaf("Go To Patrol", GoToPatrol);*/
        Leaf goToAttack = new Leaf("Go To Attack", GoToAttack);

        /*
        Selector selectAction = new Selector("Select action to do");

        selectAction.AddChild(goToAttack);
        selectAction.AddChild(goToPatrol);
        */
        patrol.AddChild(goToItem01);
        patrol.AddChild(goToItem02);
        patrol.AddChild(goToItem03);

        monsterTree.AddChild(patrol);

        monsterTree.PrintTree();
    }
    
    public Node.Status IfAttack(GameObject Location)
    {
        Node.Status s = GoToLocation(Location.transform.position);
        if(s == Node.Status.SUCCESS)
        {
            if (distance <= dagerousRange)
            {
                return Node.Status.SUCCESS;
            }
            return Node.Status.FAILURE;
        } else
            return s;
    }
    public Node.Status GoToAttack()
    {
        Debug.Log("Go To Attack");
        return IfAttack(player);
    }
     

    public Node.Status GoToItem01()
    {
        Debug.Log("Go To Item01");
        return GoToLocation(itemPos01.transform.position);
    }
    public Node.Status GoToItem02()
    {
        Debug.Log("Go To Item02");
        return GoToLocation(itemPos02.transform.position);
    }
    public Node.Status GoToItem03()
    {
        Debug.Log("Go To Item03");
        return GoToLocation(itemPos03.transform.position);
    }


    // Update is called once per frame
    void Update()
    {
        if (treeStatus == Node.Status.RUNNING)
            treeStatus = monsterTree.Process();

        player = GameObject.FindWithTag("Player");
        playerPos = GameObject.FindWithTag("Player").transform.position;
        distance = Vector3.Distance(this.transform.position, playerPos);
    }


    Node.Status GoToLocation(Vector3 destination)
    {
        // go to certain locations if the tree is processing
        float distanceToTarget = Vector3.Distance(destination, this.transform.position);
        if (state == ActionState.IDLE)
        {
            agent.SetDestination(destination);
            state = ActionState.WORKING;
        }
        else if (Vector3.Distance(agent.pathEndPosition, destination) >= 2)
        {
            state = ActionState.IDLE;
            return Node.Status.FAILURE;
        }
        else if (distanceToTarget < 2)
        {
            state = ActionState.IDLE;
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }
}