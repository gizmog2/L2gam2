using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshHelper : MonoBehaviour
{
    [SerializeField] Transform EndPoint;
    


    NavMeshAgent agent;
    HealthHelper healthHelper;
    NPCHealper nPCHealper;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        healthHelper = GetComponent<HealthHelper>();
        nPCHealper = GetComponent<NPCHealper>();
    }

    // Update is called once per frame
    void Update()
    {
        if (healthHelper.Dead || !nPCHealper.Target  || nPCHealper.Target.Dead)
        {
            return;
        }
        /*if (agent.isOnOffMeshLink)
        {            
        }*/
        GetComponent<Animator>().SetBool("OffMesh", agent.isOnOffMeshLink);

        agent.SetDestination(nPCHealper.Target.transform.position);
    }
}
