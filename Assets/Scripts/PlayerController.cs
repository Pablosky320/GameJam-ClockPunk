using UnityEngine;
using UnityEngine.InputSystem; 
using System.Collections;
using Microlight.MicroBar; 

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 7f;
    private Animator anim;
    private Rigidbody rb;
    private Vector3 direccionFinal;

    [Header("Dash y Energía")]
    public MicroBar barraDashUI;       
    public float energiaMaxima = 100f;
    public float costeDash = 30f;      
    public float velocidadRegen = 15f; 
    private float energiaActual;
    public float fuerzaDash = 45f;      
    public float tiempoDash = 0.12f;    
    private bool estaHaciendoDash = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>(); 
        energiaActual = energiaMaxima;

        // ESTO ES CLAVE: Bloqueamos rotación física para que el código mande
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        if (barraDashUI != null) barraDashUI.Initialize(energiaMaxima);
    }

    void Update()
    {
        // 1. Energía
        if (energiaActual < energiaMaxima) {
            energiaActual += velocidadRegen * Time.deltaTime;
            if (barraDashUI != null) barraDashUI.UpdateBar(energiaActual);
        }

        if (estaHaciendoDash) return;

        // 2. Input de Teclado (WASD)
        float h = (Keyboard.current.dKey.isPressed ? 1 : 0) - (Keyboard.current.aKey.isPressed ? 1 : 0);
        float v = (Keyboard.current.wKey.isPressed ? 1 : 0) - (Keyboard.current.sKey.isPressed ? 1 : 0);
        
        Vector3 rawInput = new Vector3(h, 0, v).normalized;
        // Ajuste para tu cámara isométrica
        direccionFinal = Quaternion.Euler(0, -50.8f, 0) * rawInput;

        // 3. Animación (0 si quieto, 1 si mueve)
        if (anim != null) anim.SetFloat("Velocidad", rawInput.magnitude);

        // 4. ROTACIÓN AL RATÓN (Para que no patine)
        MirarAlRaton();

        // 5. Dash
        if (Keyboard.current.spaceKey.wasPressedThisFrame && energiaActual >= costeDash) {
            StartCoroutine(EjecutarDash());
        }
    }

    void FixedUpdate()
    {
        if (estaHaciendoDash) return;

        // Movimiento directo sin inercias raras
        if (direccionFinal.magnitude > 0.1f) {
            rb.linearVelocity = new Vector3(direccionFinal.x * velocidad, rb.linearVelocity.y, direccionFinal.z * velocidad);
        } else {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }
    }

    void MirarAlRaton()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit)) {
            Vector3 puntoTarget = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            transform.LookAt(puntoTarget);
        }
    }

    IEnumerator EjecutarDash()
    {
        estaHaciendoDash = true;
        energiaActual -= costeDash;
        if (barraDashUI != null) barraDashUI.UpdateBar(energiaActual);

        Vector3 dirDash = direccionFinal.magnitude > 0.1f ? direccionFinal : transform.forward;
        rb.linearVelocity = dirDash * fuerzaDash;
        yield return new WaitForSeconds(tiempoDash);
        estaHaciendoDash = false;
    }
}