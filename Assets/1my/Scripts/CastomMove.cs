using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class CastomMove : MonoBehaviour
{
    [SerializeField] float speed = 3.0f;
    [SerializeField] float rotateSpeed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        CharacterController controller = GetComponent<CharacterController>();
        transform.Rotate(0, horizontal * rotateSpeed, 0);
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = speed * vertical;
        controller.SimpleMove(forward *  curSpeed);

        if (Input.GetButtonDown("Fire1"))
        {
            GetComponent<Animator>().SetBool("Aim", true);
            GetComponent<Animator>().SetBool("Idle", false);
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            GetComponent<Animator>().SetBool("Aim", false);
        }
        else if (Input.GetButtonDown("Vertical"))
        {
            GetComponent<Animator>().SetBool("Walk", true);
            GetComponent<Animator>().SetBool("Idle", false);
        }
        else if (Input.GetButtonUp("Vertical"))
        {
            GetComponent<Animator>().SetBool("Walk", false);
        }
        else
        {
            GetComponent<Animator>().SetBool("Idle", true);
        }
    }
}
