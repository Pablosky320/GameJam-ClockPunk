using UnityEngine;
using UnityEngine.InputSystem; 
using System.Collections;
using Microlight.MicroBar; 

public class PlayerController : MonoBehaviour
{
    [Header("Referencias")]
    public MicroBar barraDashUI; 
    public Transform miCamara; 
    public GameObject balaPrefab;
    public Transform puntaPistola;

    [Header("Ajustes Cámara")]
    public Vector3 offsetCamara = new Vector3(0, 10, -10);
    public float suavizadoCamara = 10f; 

    [Header("Movimiento")]
    public float velocidad = 7f;
    public float suavizadoRotacion = 20f; 

    [Header("Dash y Energía")]
    public float energiaMaxima = 100f;
    public float costeDash = 30f;      
    public float velocidadRegen = 15f; 
    private float energiaActual;
    public float fuerzaDash = 45f;      
    public float tiempoDash = 0.12f;    
    
    private bool estaHaciendoDash = false;
    private bool estaMuerto = false; 

    private Rigidbody rb;
    private Animator anim;
    private Vector3 direccionFinal;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        
        // FISICA: Esto es lo que quita la vibración junto con la cámara
        rb.interpolation = RigidbodyInterpolation.Interpolate; 
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        energiaActual = energiaMaxima;
        if (barraDashUI != null) barraDashUI.Initialize(energiaMaxima);
        if (miCamara == null) miCamara = Camera.main.transform;
    }

    void Update()
    {
        if (estaMuerto) return;

        ControlarEnergia();

        if (estaHaciendoDash) return;

        // Movimiento
        float h = (Keyboard.current.dKey.isPressed ? 1 : 0) - (Keyboard.current.aKey.isPressed ? 1 : 0);
        float v = (Keyboard.current.wKey.isPressed ? 1 : 0) - (Keyboard.current.sKey.isPressed ? 1 : 0);
        
        Vector3 input = new Vector3(h, 0, v).normalized;
        direccionFinal = Quaternion.Euler(0, -50.8f, 0) * input;

        anim.SetFloat("Velocidad", input.magnitude);

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            MirarAlRaton();
            anim.SetTrigger("Disparar");
            Disparar();
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame && energiaActual >= costeDash)
        {
            StartCoroutine(EjecutarDash(direccionFinal.magnitude > 0.1f ? direccionFinal : transform.forward));
        }
    }

    void FixedUpdate()
    {
        if (estaMuerto || estaHaciendoDash) return;

        if (direccionFinal.magnitude > 0.1f)
        {
            rb.linearVelocity = new Vector3(direccionFinal.x * velocidad, rb.linearVelocity.y, direccionFinal.z * velocidad);
            Quaternion rotacionObjetivo = Quaternion.LookRotation(direccionFinal);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, suavizadoRotacion * Time.fixedDeltaTime);
        }
        else
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }

        // CÁMARA: Moverla aquí evita que vibre al caminar
        if (miCamara != null)
        {
            Vector3 posicionDeseada = transform.position + offsetCamara;
            miCamara.position = Vector3.Lerp(miCamara.position, posicionDeseada, suavizadoCamara * Time.fixedDeltaTime);
        }
    }

    // FUNCIÓN DE MUERTE: La llama el script de salud
    public void ActivarMuerte()
    {
        if (estaMuerto) return;
        estaMuerto = true;
        anim.SetTrigger("Muerte"); // Asegúrate de que el Trigger se llame Muerte en el Animator
        rb.linearVelocity = Vector3.zero;
        this.enabled = false; 
    }

    void MirarAlRaton()
    {
        Ray rayo = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(rayo, out RaycastHit golpe))
        {
            Vector3 punto = golpe.point;
            punto.y = transform.position.y;
            transform.LookAt(punto);
        }
    }

    void Disparar()
    {
        if (puntaPistola != null && balaPrefab != null)
        {
            GameObject nuevaBala = Instantiate(balaPrefab, puntaPistola.position, transform.rotation);
            Rigidbody rbBala = nuevaBala.GetComponent<Rigidbody>();
            if (rbBala != null) rbBala.linearVelocity = transform.forward * 30f;
        }
    }

    void ControlarEnergia()
    {
        if (energiaActual < energiaMaxima)
        {
            energiaActual += velocidadRegen * Time.deltaTime;
            if (barraDashUI != null) barraDashUI.UpdateBar(energiaActual);
        }
    }

    IEnumerator EjecutarDash(Vector3 direccion)
    {
        estaHaciendoDash = true;
        energiaActual -= costeDash;
        if (barraDashUI != null) barraDashUI.UpdateBar(energiaActual);
        rb.linearVelocity = direccion * fuerzaDash;
        yield return new WaitForSeconds(tiempoDash);
        estaHaciendoDash = false;
    }
}