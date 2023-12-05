//using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class NPCHealper : MonoBehaviour
{
    [SerializeField] float FireRange = 10;
    [SerializeField] float angleZ = 0;

    HealthHelper target;
    HealthHelper healthHelper;
    PlayerShooting gun;

    public HealthHelper Target { get { return target; } }

    // Start is called before the first frame update
    void Start()
    {
        target = GetComponent<HealthHelper>();
        healthHelper = GetComponent<HealthHelper>();
        gun = GetComponentInChildren<PlayerShooting>();
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        HealthHelper[] targets = GameObject.FindObjectsOfType<HealthHelper>().Where(p => p.getGroup != healthHelper.getGroup && !p.Dead).ToArray();

        if (targets.Length == 0)
        {
            /*yield return new WaitForSeconds(1);
            StartCoroutine(Timer());*/
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<Animator>().SetTrigger("Winner");
        }
        else
        {
            target = targets[Random.Range(0, targets.Length)];

            if (!healthHelper.Dead)
            {
                yield return new WaitForSeconds(5);
                StartCoroutine(Timer());
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (!target || healthHelper.Dead || target.Dead)
        {
            return;
        }

        if (FireRange > Vector3.Distance(transform.position, target.transform.position))
        {
            Vector3 targetPos = new Vector3(target.transform.position.x, 0, target.transform.position.z);
            transform.LookAt(targetPos);
            float height = target.GetComponent<CapsuleCollider>().height;
            Vector3 firePosition = new Vector3(target.transform.position.x, height, target.transform.position.z);

            Vector3 randomeSphere = Random.insideUnitSphere * 1.5f;

            gun.Body.transform.LookAt(firePosition + randomeSphere);
            gun.StartShoot();
        }
    }
}
