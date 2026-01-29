using UnityEngine;
using UnityEngine.InputSystem; 
using System.Collections;
using Microlight.MicroBar; 
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("UI - Arrastra las Barras aquí")]
    public MicroBar barraVidaUI; 
    public MicroBar barraDashUI; 
    public Transform miCamara; 

    [Header("Ajustes de Salud")]
    public float vidaMaxima = 100f;
    private float vidaActual;

    [Header("Ajustes de Dash (4 Cargas)")]
    public float energiaMaxima = 100f;
    public float costeDash = 25f;      // 25 * 4 = 100
    public float velocidadRegen = 125f; // 25 puntos / 125 = 0.2s de recarga
    private float energiaActual;
    public float fuerzaDash = 45f;      
    public float tiempoDash = 0.12f;    

    [Header("Movimiento y Arma")]
    public float velocidad = 7f;
    public float suavizadoRotacion = 20f; 
    public Vector3 offsetCamara = new Vector3(0, 10, -10);
    public float suavizadoCamara = 10f; 
    public GameObject balaPrefab;
    public Transform puntaPistola;
    
    private bool estaHaciendoDash = false;
    private bool estaMuerto = false; 
    private Rigidbody rb;
    private Animator anim;
    private Vector3 direccionFinal;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        vidaActual = vidaMaxima;
        energiaActual = energiaMaxima;

        // Forzamos la inicialización manual para que la UI despierte
        if (barraVidaUI != null) {
            barraVidaUI.Initialize(vidaMaxima);
            barraVidaUI.UpdateBar(vidaActual); 
        }
        
        if (barraDashUI != null) barraDashUI.Initialize(energiaMaxima);

        if (miCamara == null) miCamara = Camera.main.transform;
    }

    void Update()
    {
        if (estaMuerto) return;
        ControlarRegenEnergia();
        if (estaHaciendoDash) return;

        float h = (Keyboard.current.dKey.isPressed ? 1 : 0) - (Keyboard.current.aKey.isPressed ? 1 : 0);
        float v = (Keyboard.current.wKey.isPressed ? 1 : 0) - (Keyboard.current.sKey.isPressed ? 1 : 0);
        Vector3 input = new Vector3(h, 0, v); // Quitamos normalized aquí para el Idle
        
        direccionFinal = Quaternion.Euler(0, -50.8f, 0) * input.normalized;

        // Si input.magnitude es 0, entra en Idle
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

    public void RecibirDanio(float cantidad)
    {
        if (estaMuerto) return;
        vidaActual -= cantidad;
        
        // REVISIÓN: Forzamos el Update de la barra
        if (barraVidaUI != null) {
            barraVidaUI.UpdateBar(vidaActual);
        }

        if (vidaActual <= 0) StartCoroutine(SecuenciaMuerte());
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
            rb.angularVelocity = Vector3.zero; // Evita el giro infinito al quedarse quieto
        }
    }

    void LateUpdate() 
    {
        if (miCamara != null && !estaMuerto)
        {
            Vector3 posicionDeseada = transform.position + offsetCamara;
            miCamara.position = Vector3.Lerp(miCamara.position, posicionDeseada, suavizadoCamara * Time.smoothDeltaTime);
        }
    }

    void ControlarRegenEnergia()
    {
        if (energiaActual < energiaMaxima && !estaHaciendoDash)
        {
            energiaActual = Mathf.MoveTowards(energiaActual, energiaMaxima, velocidadRegen * Time.deltaTime);
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

    IEnumerator SecuenciaMuerte()
    {
        estaMuerto = true;
        anim.SetTrigger("Muerte");
        rb.linearVelocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezeAll; // Bloqueo total al morir
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
}