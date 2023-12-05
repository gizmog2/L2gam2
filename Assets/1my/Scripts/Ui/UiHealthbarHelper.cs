using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiHealthbarHelper : MonoBehaviour
{
    Transform NPC;

    Slider _slider;
    HealthHelper _healthHelper;
    TMP_Text _kills;

    public Transform SetNPC
    {
        get { return NPC; }
        set
        {
            NPC = value;
            _healthHelper = NPC.GetComponent<HealthHelper>();
            _slider = GetComponentInChildren<Slider>();
            _slider.maxValue = _healthHelper.getMaxHealth;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _kills = transform.Find("Kills").GetComponent<TMP_Text>();
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

        if (_slider)
        {
            _slider.value = _healthHelper.getHealth;

        }
        _kills.text = _healthHelper.Kills.ToString();
    }

    public void DisableSlider()
    {
        Destroy(_slider.gameObject);
    }

}
