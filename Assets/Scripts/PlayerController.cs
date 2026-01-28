using UnityEngine;
using UnityEngine.InputSystem; 
using System.Collections;
using Microlight.MicroBar; 

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 7f;
    public float suavizadoRotacion = 0.2f;

    [Header("Dash y Energía")]
    public MicroBar barraDashUI;       
    public float energiaMaxima = 100f;
    public float costeDash = 30f;      
    public float velocidadRegen = 15f; 
    private float energiaActual;
    
    public float fuerzaDash = 45f;      
    public float tiempoDash = 0.12f;    
    private bool estaHaciendoDash = false;

    [Header("Interacción")]
    public float radioInteraccion = 2.5f;

    private Rigidbody rb;
    private Animator anim; // <-- NUEVO: Referencia al Animator
    private Vector3 direccionFinal;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>(); // <-- NUEVO: Obtenemos el Animator
        rb.sleepThreshold = 0.0f;

        energiaActual = energiaMaxima;
        if (barraDashUI != null) barraDashUI.Initialize(energiaMaxima);
    }

    void Update()
    {
        if (energiaActual < energiaMaxima)
        {
            energiaActual += velocidadRegen * Time.deltaTime;
            if (barraDashUI != null) barraDashUI.UpdateBar(energiaActual);
        }

        if (estaHaciendoDash) return;

        // LÓGICA DE MOVIMIENTO
        Vector2 input = Vector2.zero;
        if (Keyboard.current.wKey.isPressed) input.y = 1;
        if (Keyboard.current.sKey.isPressed) input.y = -1;
        if (Keyboard.current.aKey.isPressed) input.x = -1;
        if (Keyboard.current.dKey.isPressed) input.x = 1;

        Vector3 movimientoRaw = new Vector3(input.x, 0, input.y).normalized;
        direccionFinal = Quaternion.Euler(0, -50.8f, 0) * movimientoRaw;

        // --- NUEVO: ANIMACIÓN DE MOVIMIENTO ---
        // Mandamos la magnitud (0 si está quieto, 1 si se mueve) al parámetro "Velocidad"
        anim.SetFloat("Velocidad", movimientoRaw.magnitude);

        // --- NUEVO: ANIMACIÓN DE DISPARO ---
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            anim.SetTrigger("Disparar");
        }

        // DASH
        if (Keyboard.current.spaceKey.wasPressedThisFrame && energiaActual >= costeDash)
        {
            Vector3 direccionParaDash = direccionFinal.magnitude > 0.1f ? direccionFinal : transform.forward;
            energiaActual -= costeDash;
            if (barraDashUI != null) barraDashUI.UpdateBar(energiaActual);
            StartCoroutine(EjecutarDash(direccionParaDash));
        }

        if (Keyboard.current.eKey.wasPressedThisFrame) RevisarInteraccion();

        // TEST DE MUERTE (Para que pruebes si las flechas funcionan)
        if (Keyboard.current.kKey.wasPressedThisFrame) Morir();
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

    // --- NUEVO: FUNCIONES DE ESTADO ---
    public void Morir()
    {
        anim.SetTrigger("Muerte");
        this.enabled = false; // Desactiva el script para que no se mueva más
    }

    public void Victoria()
    {
        anim.SetTrigger("Victoria");
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