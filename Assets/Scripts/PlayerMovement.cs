using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    private Vector3 input;
    private float speed;

    private void FixedUpdate()
    {
        Move();
    }

    void Update()
    {
        GatherInput();
        Look();
    }
    void GatherInput()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    void Move()
    {
        rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
    }
    
    void Look()
    {
        var relative = (transform.position + input) - transform.position;
        var rot = Quaternion.LookRotation(relative, Vector3.up);

        transform.rotation = rot;
    }
}
