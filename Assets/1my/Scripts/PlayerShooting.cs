using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] GameObject _body;        // —сылка на оружие
    public GameObject Body { get { return _body; } }

    [SerializeField] int damagePerShot = 20;   // ”рон за выстрел
    [SerializeField] float timeBetweenBullets = 0.15f;
    [SerializeField] float range = 100f;

    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudioSource;
    Light gunLight;
    [SerializeField] Light faceLight;
    float effectsDisplayTime = 0.2f;

    HealthHelper _parent;

    private void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");

        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunLight = GetComponent<Light>();
        gunAudioSource = GetComponent<AudioSource>();

        _parent = GetComponentInParent<HealthHelper>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }

        //StartShoot();

    }


    public void DisableEffects()
    {
        gunLight.enabled = false;
        faceLight.enabled = false;
        gunLine.enabled = false;
    }

    public void StartShoot()
    {
        if (timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        timer = 0f;

        gunAudioSource.Play();

        gunLight.enabled = true;
        faceLight.enabled = true;

        gunParticles.Stop();
        gunParticles.Play();

        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if (Physics.Raycast(shootRay, out shootHit, range))
        {
            // Head hit
            if (/*shootHit.collider.GetComponentInParent<HealthHelper>() && */shootHit.collider.GetComponentInParent<HeadHelper>())
            {
                GameObject blood = Instantiate(Resources.Load("Blood"), shootHit.point, Quaternion.identity) as GameObject;
                Destroy(blood, 1);

                shootHit.collider.GetComponentInParent<HealthHelper>().GetDamage(101, _parent);
                //Debug.Log("HeadShot!");
            }

            // Body hit
            else if (shootHit.collider.GetComponentInParent<HealthHelper>())
            {
                //Blood effect
                GameObject blood = Instantiate(Resources.Load("Blood"), shootHit.point, Quaternion.identity) as GameObject;
                Destroy(blood, 1);
                shootHit.collider.GetComponentInParent<HealthHelper>().GetDamage( damagePerShot, _parent);
            }

            else if (shootHit.collider.GetComponent<Rigidbody>())
            {
                shootHit.collider.GetComponent<Rigidbody>().AddForce(transform.forward * 300);
            }

            gunLine.SetPosition(1, shootHit.point);
        }

        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }

    public void Drop()
    {
        StartCoroutine(WaitDrop());
    }

    IEnumerator WaitDrop()
    {
        yield return new WaitForSeconds(0.3f);

        _body.transform.SetParent(null);
        _body.GetComponent<Collider>().enabled = true;
        _body.GetComponent<Rigidbody>().isKinematic = false;
        _body.GetComponent<Rigidbody>().AddForce(Random.insideUnitSphere * 200);
    }
}
