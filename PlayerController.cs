using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 8-directional movement
public class PlayerController : MonoBehaviour {
    public float velocity = 5.0f;
    public float turnSpeed = 10.0f;

    private Vector3 input;
    private float angle;

    private Quaternion targetRotation;
    private Transform cam;

    public Animator anim;

    void Start()
    {
        cam = Camera.main.transform;
    }

    void FixedUpdate()
    {
        GetInput();

        if(Mathf.Abs(input.x) < 1 && Mathf.Abs(input.z) < 1) return;

        CalculationDirection();
        Rotate();
        Move();
    }

    //Input based on Horizontal (a, d, <, >) and Vertical (w, s, ^, v) keys
    void GetInput()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.z = Input.GetAxisRaw("Vertical");

        if (input.x != 0 || input.z != 0) anim.SetBool("run", true);
        else anim.SetBool("run", false);
    }

    //Direction relative to the camera's rotation
    void CalculationDirection()
    {
        angle = Mathf.Atan2(input.x, input.z);
        angle = Mathf.Rad2Deg * angle;
        angle += cam.eulerAngles.y;
    }

    //Rotate toward the calculated angle
    void Rotate()
    {
        targetRotation = Quaternion.Euler(0, angle, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    //This player only moves along its own forward axis
    void Move()
    {
        transform.position += transform.forward * velocity * Time.deltaTime;
    }
}
