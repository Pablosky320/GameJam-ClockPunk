using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    float dashSpeed = 40f;
    float dashDuration = 0.1f;
    float dashCooldown = 0.5f;

    public Transform cameraTransform;

    private CharacterController controller;
    private float yVelocity;

    float h;
    float v;

    public bool isDashing;
    bool canDash;

    private Vector3 moveDirection;
    private Vector3 dashDirection;
    private Vector3 inputDir;


    void Start()
    {
        controller = GetComponent<CharacterController>();

        isDashing = false;
        canDash = true;

        if (cameraTransform == null)
            cameraTransform = GetComponent<Camera>().transform;
    }
    void Update()
    {        
        // Lee el input WASD


        if (!isDashing)
        {
            Moving();
        }
        MouseLook();

        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dashing());
        }

        controller.Move((moveDirection + Vector3.up * yVelocity) * Time.deltaTime);
    }
    private void FixedUpdate()
    {

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

    void Moving()
    {
        inputDir = new Vector3(h, 0f, v).normalized;

        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

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
    }

    IEnumerator Dashing()
    {
        canDash = false;
        isDashing = true;


        // guarda la direcci�n en la que el personaje se mueve
        dashDirection = moveDirection.normalized;
        
        if (dashDirection.magnitude == 0)
        {
            //si esta quieto se mueve en la direcci�n en la que mira
            dashDirection = transform.forward;
        }

        float elapsed = 0f;

        while (elapsed < dashDuration)
        {
            controller.Move(dashDirection * dashSpeed * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null; // espera un frame
        }

        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;

    }
}
