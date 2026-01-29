using UnityEngine;

public class PlayerJam : MonoBehaviour
{
    public float velocidad = 10f;
    private Rigidbody rb;
    private Vector3 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Esto asegura que el Rigidbody no se duerma
        rb.sleepThreshold = 0.0f;
    }

    void Update()
    {
        // Leemos las flechas o WASD
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        
        // Creamos el vector de movimiento
        moveInput = new Vector3(h, 0, v).normalized;

        // DEBUG: Esto dibujará una línea roja en la escena para ver hacia dónde intentas moverte
        Debug.DrawRay(transform.position, moveInput * 2, Color.red);
    }

    void FixedUpdate()
    {
        // Aplicamos velocidad directa al Rigidbody
        rb.linearVelocity = moveInput * velocidad; // En Unity 6 se usa linearVelocity en lugar de velocity
    }
}