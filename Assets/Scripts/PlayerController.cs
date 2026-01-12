using UnityEngine;
using UnityEngine.InputSystem; 
using System.Collections;
using Microlight.MicroBar; // Añadido para controlar la barra

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 7f;
    public float suavizadoRotacion = 0.2f;

    [Header("Dash y Energía")]
    public MicroBar barraDashUI;       // Arrastra aquí la barra amarilla del Canvas
    public float energiaMaxima = 100f;
    public float costeDash = 30f;      // Cuánto quita cada dash
    public float velocidadRegen = 15f; // Energía por segundo
    private float energiaActual;
    
    public float fuerzaDash = 45f;      
    public float tiempoDash = 0.12f;    
    private bool estaHaciendoDash = false;

    [Header("Interacción")]
    public float radioInteraccion = 2.5f;

    private Rigidbody rb;
    private Vector3 direccionFinal;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.sleepThreshold = 0.0f;

        // Inicializar energía y barra
        energiaActual = energiaMaxima;
        if (barraDashUI != null) barraDashUI.Initialize(energiaMaxima);
    }

    void Update()
    {
        // 1. Regeneración de Energía
        if (energiaActual < energiaMaxima)
        {
            energiaActual += velocidadRegen * Time.deltaTime;
            if (barraDashUI != null) barraDashUI.UpdateBar(energiaActual);
        }

        if (estaHaciendoDash) return;

        // 2. LEER INPUTS
        Vector2 input = Vector2.zero;
        if (Keyboard.current.wKey.isPressed) input.y = 1;
        if (Keyboard.current.sKey.isPressed) input.y = -1;
        if (Keyboard.current.aKey.isPressed) input.x = -1;
        if (Keyboard.current.dKey.isPressed) input.x = 1;

        Vector3 movimientoRaw = new Vector3(input.x, 0, input.y).normalized;
        direccionFinal = Quaternion.Euler(0, -132f, 0) * movimientoRaw;

        // 3. LÓGICA DEL DASH (Solo si tiene energía suficiente)
        if (Keyboard.current.spaceKey.wasPressedThisFrame && energiaActual >= costeDash)
        {
            Vector3 direccionParaDash = direccionFinal.magnitude > 0.1f ? direccionFinal : transform.forward;
            
            // GASTAR ENERGÍA
            energiaActual -= costeDash;
            if (barraDashUI != null) barraDashUI.UpdateBar(energiaActual);

            StartCoroutine(EjecutarDash(direccionParaDash));
        }

        if (Keyboard.current.eKey.wasPressedThisFrame) RevisarInteraccion();
    }

    void FixedUpdate()
    {
        if (estaHaciendoDash) return;

        if (direccionFinal.magnitude > 0.1f)
        {
            rb.linearVelocity = direccionFinal * velocidad;
            Quaternion rotacionObjetivo = Quaternion.LookRotation(direccionFinal);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, suavizadoRotacion);
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
        }
    }

    IEnumerator EjecutarDash(Vector3 direccion)
    {
        estaHaciendoDash = true;
        transform.rotation = Quaternion.LookRotation(direccion);
        rb.linearVelocity = direccion * fuerzaDash;
        yield return new WaitForSeconds(tiempoDash);
        rb.linearVelocity = Vector3.zero;
        estaHaciendoDash = false;
    }

    void RevisarInteraccion()
    {
        Collider[] colisiones = Physics.OverlapSphere(transform.position, radioInteraccion);
        foreach (Collider col in colisiones)
        {
            if (col.TryGetComponent(out IInteractuable objeto))
            {
                objeto.Interactuar();
                break; 
            }
        }
    }
}