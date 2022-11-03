using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobberBehaviour : MonoBehaviour
{
    BehaviourTree tree;
    public GameObject guard;
    public GameObject diamond;
    public GameObject car;
    public GameObject door;
    public GameObject hidingPoint;
    NavMeshAgent agent;

    public enum ActionState { IDLE, WORKING};
    ActionState state = ActionState.IDLE;

    Node.Status treeStatus = Node.Status.RUNNING;


    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();

        tree = new BehaviourTree(); // create new behavior tree
        Sequence steal = new Sequence("Steall something"); // add Steal action of sequence
        // add leaves of Steal Sequence
        Leaf goToDoor = new Leaf("Go To Door", GoToDoor); 
        Leaf goToDiamond = new Leaf("Go To Diamond", GoToDiamond);
        Leaf goToHidingPoint = new Leaf("Go To Hiding Point", GoToHidingPoint);
        Leaf goToCar = new Leaf("Go To Car", GoToCar);
        Selector ifNeedsHide = new Selector("Go To Hiding Point");

        //create Needhide Selector

        ifNeedsHide.AddChild(goToDoor);
        ifNeedsHide.AddChild(goToHidingPoint);

        //list the sequence of steal
        steal.AddChild(goToDiamond);
        steal.AddChild(ifNeedsHide);
        steal.AddChild(goToCar);

        tree.AddChild(steal);

        tree.PrintTree();
        
    }

    //add go to door function
    public Node.Status GoToDoor()
    {
        Debug.Log("Go to door");
        return IfNeedsHide(door);
    }

    //add go to diamond funciton
    public Node.Status GoToDiamond()
    {
        //robber takes diamond once he reaches diamond's position
        Debug.Log("Go to diamond");
        Node.Status s = GoToLocation(diamond.transform.position);
        if( s == Node.Status.SUCCESS)
        {
            diamond.transform.parent = this.gameObject.transform;
            //once diamond is stole, alert system will be active. 
            guard.GetComponent<GuardAlert>().isAlerted = true;
        }
        return s;
    }

    //add go to hiding point function
    public Node.Status GoToHidingPoint()
    {
        Debug.Log("Go to hiding point");
        return IfNeedsHide(hidingPoint);
    }

    //add go to car function
    public Node.Status GoToCar()
    {
        Debug.Log("Go to car");
        return GoToLocation(car.transform.position);
    }


    public Node.Status IfNeedsHide(GameObject location)
    {
        Node.Status s = GoToLocation(location.transform.position);
        if (s == Node.Status.SUCCESS)
        {
            if (!guard.GetComponent<GuardAlert>().isAlerted)
            {
                guard.SetActive(false);
                return Node.Status.SUCCESS;
            }
            return Node.Status.FAILURE;
        }
        else
            return s;
    }
    

    //create go to location function
    Node.Status GoToLocation(Vector3 destination)
    {
        float distanceToTarget = Vector3.Distance(destination, this.transform.position);
        if(state == ActionState.IDLE)
        {
            agent.SetDestination(destination);
            state = ActionState.WORKING;
        }
        else if (Vector3.Distance(agent.pathEndPosition, destination) >=2) 
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



    public void Update()
    {
        if(treeStatus == Node.Status.RUNNING)
            treeStatus = tree.Process();
    }




}

