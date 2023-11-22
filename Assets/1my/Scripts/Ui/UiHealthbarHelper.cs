using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiHealthbarHelper : MonoBehaviour
{
    Transform NPC; 

    Slider _slider;
    HealthHelper _healthHelper;
    public Transform SetNPC
    {
        get { return NPC; }
        set
        {
            NPC = value;
            _healthHelper = NPC.GetComponent<HealthHelper>();
            _slider = GetComponent<Slider>();
            _slider.maxValue = _healthHelper.getMaxHealth;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
        
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!NPC)
        {
            return;
        }

        Vector3 npcPos = new Vector3(NPC.position.x, NPC.position.y + 2f, NPC.position.z);
        GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(npcPos);

        _slider.value = _healthHelper.getHealth;
    }

}
