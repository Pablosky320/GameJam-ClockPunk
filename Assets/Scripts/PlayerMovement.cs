using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float gravity = -9.81f;

    public Transform cameraTransform;

    private CharacterController controller;
    private float yVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (cameraTransform == null)
            cameraTransform = GetComponent<Camera>().transform;
    }
    void Update()
    {

    }    
    private void FixedUpdate()
    {
        // Lee el input WASD
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
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

    void Dashing()
    {

    }
}
