using UnityEngine;
using UnityEngine.InputSystem; // Requerido para el nuevo sistema de Unity 6
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 7f;
    public float suavizadoRotacion = 0.2f;

    [Header("Dash")]
    public float fuerzaDash = 45f;      // Aumentado para que sea más largo
    public float tiempoDash = 0.12f;    // Menos tiempo para que sea instantáneo
    private bool estaHaciendoDash = false;

    [Header("Interacción")]
    public float radioInteraccion = 2.5f;
    public KeyCode teclaInteractuar = KeyCode.E;

    private Rigidbody rb;
    private Vector3 direccionFinal;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        // Configuramos el Rigidbody para que no se duerma y use el nuevo sistema de Unity 6
        rb.sleepThreshold = 0.0f;
    }

    void Update()
    {
        // No permitimos nuevos inputs si ya estamos en medio de un dash
        if (estaHaciendoDash) return;

        // 1. LEER INPUTS DEL TECLADO (New Input System)
        Vector2 input = Vector2.zero;
        if (Keyboard.current.wKey.isPressed) input.y = 1;
        if (Keyboard.current.sKey.isPressed) input.y = -1;
        if (Keyboard.current.aKey.isPressed) input.x = -1;
        if (Keyboard.current.dKey.isPressed) input.x = 1;

        // 2. CÁLCULO DE DIRECCIÓN (Alineado con tu cámara a -132 grados)
        Vector3 movimientoRaw = new Vector3(input.x, 0, input.y).normalized;
        direccionFinal = Quaternion.Euler(0, -132f, 0) * movimientoRaw;

        // 3. LÓGICA DEL DASH (Espacio)
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Vector3 direccionParaDash;

            // Si nos estamos moviendo, dashea hacia donde pulsamos
            if (direccionFinal.magnitude > 0.1f)
            {
                direccionParaDash = direccionFinal;
            }
            // SI ESTAMOS QUIETOS: Dashea hacia donde mira el personaje actualmente
            else
            {
                direccionParaDash = transform.forward;
            }

            StartCoroutine(EjecutarDash(direccionParaDash));
        }

        // 4. INTERACCIÓN (E)
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            RevisarInteraccion();
        }
    }

    void FixedUpdate()
    {
        // Durante el dash, la física la controla el Coroutine, no el FixedUpdate normal
        if (estaHaciendoDash) return;

        if (direccionFinal.magnitude > 0.1f)
        {
            // Aplicar velocidad (usando linearVelocity para Unity 6)
            rb.linearVelocity = direccionFinal * velocidad;

            // Rotación suave hacia la dirección del movimiento
            Quaternion rotacionObjetivo = Quaternion.LookRotation(direccionFinal);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, suavizadoRotacion);
        }
        else
        {
            // Frenado cuando no hay input
            rb.linearVelocity = Vector3.zero;
        }
    }

    IEnumerator EjecutarDash(Vector3 direccion)
    {
        estaHaciendoDash = true;

        // Orientar al personaje instantáneamente hacia donde va a dashear
        transform.rotation = Quaternion.LookRotation(direccion);

        // Aplicamos la fuerza bruta
        rb.linearVelocity = direccion * fuerzaDash;

        // Esperamos el tiempo del dash
        yield return new WaitForSeconds(tiempoDash);

        // Frenazo en seco al terminar (da mejor sensación de control en Game Jams)
        rb.linearVelocity = Vector3.zero;
        
        estaHaciendoDash = false;
    }

    void RevisarInteraccion()
    {
        // Detectar objetos con interfaz IInteractuable
        Collider[] colisiones = Physics.OverlapSphere(transform.position, radioInteraccion);
        foreach (Collider col in colisiones)
        {
            if (col.TryGetComponent(out IInteractuable objeto))
            {
                objeto.Interactuar();
                break; // Solo interactúa con el primer objeto que encuentre
            }
        }
    }

    // Dibujamos el radio de interacción en el Editor para que puedas ajustarlo
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radioInteraccion);
    }
}