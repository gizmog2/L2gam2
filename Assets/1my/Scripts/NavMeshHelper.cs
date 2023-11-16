using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshHelper : MonoBehaviour
{
    [SerializeField] Transform EndPoint;

    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        /*if (agent.isOnOffMeshLink)
        {            
        }*/
        GetComponent<Animator>().SetBool("OffMesh", agent.isOnOffMeshLink);

        agent.SetDestination(EndPoint.position);
    }
}
