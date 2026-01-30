using UnityEngine;
using UnityEngine.InputSystem; 
using System.Collections;
using Microlight.MicroBar; 
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Cámara")]
    public Transform miCamara; 
    public Vector3 offsetCamara = new Vector3(0, 10, -10); // Ajusta esto para ver al gato
    public float suavizadoCamara = 5f;

    [Header("UI")]
    public MicroBar barraVidaUI; 
    public MicroBar barraDashUI; 

    [Header("Salud")]
    public float vidaMaxima = 100f;
    private float vidaActual;
    private bool estaMuerto = false;

    [Header("Dash (4 Cargas)")]
    public float energiaActual;
    public float energiaMaxima = 100f;
    public float costeDash = 25f;      
    public float velocidadRegen = 125f; 
    public float fuerzaDash = 45f;      
    public float tiempoDash = 0.12f;    
    private bool estaHaciendoDash = false;

    [Header("Movimiento y Arma")]
    public float velocidad = 7f;
    public float suavizadoRotacion = 20f; 
    public GameObject balaPrefab;
    public Transform puntaPistola;
    public GameObject modeloPistolaMano; // Arrastra la pistola aquí

    private Rigidbody rb;
    private Animator anim;
    private Vector3 direccionFinal;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        vidaActual = vidaMaxima;
        energiaActual = energiaMaxima;

        if (barraVidaUI) { barraVidaUI.Initialize(vidaMaxima); barraVidaUI.UpdateBar(vidaActual); }
        if (barraDashUI) barraDashUI.Initialize(energiaMaxima);
        if (modeloPistolaMano) modeloPistolaMano.SetActive(false); 
    }

    void Update()
    {
        if (estaMuerto) return;
        
        // Regeneración de energía
        if (energiaActual < energiaMaxima && !estaHaciendoDash) {
            energiaActual = Mathf.MoveTowards(energiaActual, energiaMaxima, velocidadRegen * Time.deltaTime);
            if (barraDashUI) barraDashUI.UpdateBar(energiaActual);
        }

        // Movimiento básico
        float h = (Keyboard.current.dKey.isPressed ? 1 : 0) - (Keyboard.current.aKey.isPressed ? 1 : 0);
        float v = (Keyboard.current.wKey.isPressed ? 1 : 0) - (Keyboard.current.sKey.isPressed ? 1 : 0);
        Vector3 input = new Vector3(h, 0, v);
        direccionFinal = Quaternion.Euler(0, -50.8f, 0) * input.normalized;

        anim.SetFloat("Velocidad", input.magnitude);

        if (Mouse.current.leftButton.wasPressedThisFrame) {
            MirarAlRaton();
            anim.SetTrigger("Disparar");
            Disparar();
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame && energiaActual >= costeDash && !estaHaciendoDash) {
            StartCoroutine(EjecutarDash());
        }
    }

    void LateUpdate() // La cámara se mueve después del personaje
    {
        if (miCamara != null) {
            Vector3 posicionDeseada = transform.position + offsetCamara;
            miCamara.position = Vector3.Lerp(miCamara.position, posicionDeseada, suavizadoCamara * Time.deltaTime);
        }
    }

    // EVENTOS DE ANIMACIÓN
    public void MostrarPistola() { if(modeloPistolaMano) modeloPistolaMano.SetActive(true); }
    public void OcultarPistola() { if(modeloPistolaMano) modeloPistolaMano.SetActive(false); }

    public void RecibirDanio(float d) {
        if (estaMuerto) return;
        vidaActual -= d;
        if (barraVidaUI) barraVidaUI.UpdateBar(vidaActual);
        if (vidaActual <= 0) StartCoroutine(SecuenciaMuerte());
    }

    void FixedUpdate() {
        if (estaMuerto || estaHaciendoDash) return;
        rb.linearVelocity = new Vector3(direccionFinal.x * velocidad, rb.linearVelocity.y, direccionFinal.z * velocidad);
        if (direccionFinal.magnitude > 0.1f) {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direccionFinal), suavizadoRotacion * Time.fixedDeltaTime);
        } else { rb.angularVelocity = Vector3.zero; }
    }

    IEnumerator EjecutarDash() {
        estaHaciendoDash = true;
        energiaActual -= costeDash;
        if (barraDashUI) barraDashUI.UpdateBar(energiaActual);
        rb.linearVelocity = (direccionFinal.magnitude > 0.1f ? direccionFinal : transform.forward) * fuerzaDash;
        yield return new WaitForSeconds(tiempoDash);
        estaHaciendoDash = false;
    }

    void Disparar() {
        if (puntaPistola) {
            GameObject b = Instantiate(balaPrefab, puntaPistola.position, transform.rotation);
            b.GetComponent<Rigidbody>().linearVelocity = transform.forward * 30f;
        }
    }

    void MirarAlRaton() {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit)) {
            transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
        }
    }

    IEnumerator SecuenciaMuerte() {
        estaMuerto = true;
        anim.SetTrigger("Muerte");
        rb.constraints = RigidbodyConstraints.FreezeAll;
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}