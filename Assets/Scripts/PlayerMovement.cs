using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    float dashSpeed = 20f;

    public Transform cameraTransform;

    private CharacterController controller;
    private float yVelocity;
    float h;
    float v;

    bool isDashing;

    private Vector3 moveDirection;
    private Vector3 dashDirection;


    void Start()
    {
        controller = GetComponent<CharacterController>();

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (cameraTransform == null)
            cameraTransform = GetComponent<Camera>().transform;
    }
    void Update()
    {

    }    
    private void FixedUpdate()
    {
        // Lee el input WASD

        Vector3 inputDir = new Vector3(h, 0f, v).normalized;

        MouseLook();

            //Determinando la posicion de la camara
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();
            
        Vector3 moveDir = (camForward * inputDir.z + camRight * inputDir.x).normalized;
        moveDirection = moveDir * moveSpeed;

        /* 3. No creo que haga falta la gravedad pero ya que esta voy a dejarla por si acaso
        if (controller.isGrounded)
        {
            yVelocity = -1f; // small downward push
        }
        else
        {
            yVelocity += gravity * Time.deltaTime;
        }
        */
        // Moviemento
        Vector3 velocity = moveDir * moveSpeed + Vector3.up * yVelocity;
        controller.Move(velocity * Time.deltaTime);
            
        if (isDashing == true)
            {
                StartCoroutine (dashing());


            }
    }

    // Esta funcion controla mirar con la camara
    void MouseLook()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(ray, out float distance))
        {
            Vector3 point = ray.GetPoint(distance);
            Vector3 lookDir = point - transform.position;
            lookDir.y = 0f;

            if (lookDir.sqrMagnitude > 0.01f)
                transform.rotation = Quaternion.LookRotation(lookDir);
        }
    }

    IEnumerator dashing()
    {
        dashDirection;
        controller.Move(dashDirection * dashSpeed * Time.deltaTime);
        
    }
}
