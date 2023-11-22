using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHelper : MonoBehaviour
{
    [SerializeField] float MaxHealth = 100;
    [SerializeField] float Health = 100;

    [SerializeField] bool DynamicHealthBarCreate = true;

    private bool _dead;

    public bool Dead
    {
        get { return _dead; }
        set { _dead = value; }
    }

    public int Kills { get; private set; }

    UiHealthbarHelper _healthBarHelper;

    public float getMaxHealth { get {return MaxHealth; } }
    public float getHealth { get { return Health; } }

    public void GetDamage(int damage, HealthHelper killer)
    {
        if (Dead)
        {
            return;
        }

        Health -= damage;

        if (Health <= 0)
        {
            Dead = true;
            killer.Kills = 1;
            GetComponentInChildren<PlayerShooting>().Drop();
            GetComponent<Animator>().SetBool("Dead", true);
        }

    }



    // Start is called before the first frame update
    void Start()
    {
        if (DynamicHealthBarCreate)
        {
            GameObject healthbar = Instantiate(Resources.Load("Healthbar"), Vector3.zero, Quaternion.identity) as GameObject;
            healthbar.transform.SetParent(GameObject.Find("Canvas").transform);

            _healthBarHelper = healthbar.GetComponent<UiHealthbarHelper>();
            _healthBarHelper.SetNPC = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
