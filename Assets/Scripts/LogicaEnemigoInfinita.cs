using UnityEngine;
using System.Collections;

public class LogicaEnemigoInfinita : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject balaPrefab;
    public Transform puntaPistola;

    [Header("Configuración de Tiempos (Frames 24 FPS)")]
    // Frame 48 / 24 fps = 2.0s
    public float tiempoEntreDisparos = 2.0f; 
    // Frames 144 a 170 son 26 frames / 24 fps = 1.08s
    public float tiempoDeRecarga = 1.08f;    
    public float velocidadBala = 25f;

    private Animator anim;
    private Transform jugador;

    void Start()
    {
        anim = GetComponent<Animator>();
        
        // Buscar al jugador para apuntar
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) jugador = player.transform;

        // Iniciar el bucle infinito
        StartCoroutine(CicloDeAtaque());
    }

    void Update()
    {
        // Rotación suave hacia el jugador
        if (jugador != null)
        {
            Vector3 direccion = (jugador.position - transform.position).normalized;
            direccion.y = 0; 
            transform.rotation = Quaternion.LookRotation(direccion);
        }
    }

    IEnumerator CicloDeAtaque()
    {
        while (true)
        {
            // FASE DE DISPARO: 3 VECES (Frames 0 a 144)
            for (int i = 0; i < 3; i++)
            {
                // Usamos Play para asegurar que la animación empiece de cero cada vez
                anim.Play("Disparo", -1, 0f);
                
                // Frame 24: Es cuando el personaje dispara en tu video (1 segundo justo)
                yield return new WaitForSeconds(1.0f); 
                
                InstanciarBala();
                
                // Esperamos hasta el frame 48 para que termine de bajar el arma (1 segundo más)
                yield return new WaitForSeconds(1.0f); 
            }

            // FASE DE RECARGA: 1 VEZ (Frames 144 a 170)
            anim.Play("Recarga", -1, 0f);
            
            // Esperamos el tiempo de la recarga antes de volver a empezar el bucle
            yield return new WaitForSeconds(tiempoDeRecarga);
        }
    }

    void InstanciarBala()
    {
        if (balaPrefab != null && puntaPistola != null)
        {
            GameObject b = Instantiate(balaPrefab, puntaPistola.position, puntaPistola.rotation);
            if (b.TryGetComponent(out Rigidbody rb)) 
            {
                rb.linearVelocity = puntaPistola.forward * velocidadBala;
            }
        }
    }
}