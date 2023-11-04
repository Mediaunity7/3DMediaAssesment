using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform Camera;

    [Range(0,30)] public float speed=6f;
    CharacterController controller;
    float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal,0, vertical).normalized;//.normilise -- this doesnt allow the player to go double speed
        //when 2 keys are pressed together

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z)*Mathf.Rad2Deg+Camera.eulerAngles.y;
            //atan2--gives us the angle we are looking at in radiant, and we change it later on to degrees
            //and now we add the angle of the camera to that equasion so we can use the camera for direction as well

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 movedirection = Quaternion.Euler(0f,targetAngle,0f)*Vector3.forward;
            controller.Move(movedirection.normalized*speed*Time.deltaTime);
        }
    }
}
