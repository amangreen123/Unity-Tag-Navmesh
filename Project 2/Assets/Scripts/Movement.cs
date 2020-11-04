using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.NetworkInformation;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public CharacterController controller;
    public Transform cam;
    public float speed = 6f;
    public float turnTime = 0.1f;
    public float jumpSpeed = 8f;
    public float gravity = 20f;
    float turnVel;
    public Animator anim;
    public Camera Cam;
    public float range = 20f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // if (controller.isGrounded)
       // {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            anim.SetFloat("Speed", vertical);
            anim.SetBool("isJumping", false);

            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)

            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVel, turnTime); //smooth numbers at angles
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }
             if (Input.GetButtonDown("Fire1"))
             {
                 Tag();
             }

            if (Input.GetButton("Jump"))
            {
                anim.SetBool("isJumping", true);
                direction.y = jumpSpeed;
            }

            direction.y -= gravity * Time.deltaTime;

            if (vertical != 0 || horizontal != 0)
            {
                anim.SetBool("isMoving", true);
            }
            else
            {
                anim.SetBool("isMoving", false);
            }
        //  }


    }

    private void Tag()
    {
        RaycastHit hit;
      if(  Physics.Raycast(Cam.transform.position, Cam.transform.forward, out hit,range))
        {
            UnityEngine.Debug.Log(hit.transform.name);
        }
    }

}
