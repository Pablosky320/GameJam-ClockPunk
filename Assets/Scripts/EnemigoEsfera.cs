using UnityEngine;

public class EnemigoEsfera : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject balaPrefab;
    private Transform jugador;

    [Header("Configuración")]
    public float cadenciaFuego = 1.5f;
    public float velocidadBala = 25f;
    public float alturaDelTiro = 0.4f; 

    private float tiempoSiguienteDisparo;

    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) jugador = p.transform;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }
        
        // Evita que dispare en el segundo 0
        tiempoSiguienteDisparo = Time.time + cadenciaFuego;
    }

    void Update()
    {
        if (jugador == null) return;

        // Mirar al jugador
        Vector3 targetPos = new Vector3(jugador.position.x, transform.position.y, jugador.position.z);
        transform.LookAt(targetPos);

        // Disparar solo si ha pasado el tiempo necesario
        if (Time.time >= tiempoSiguienteDisparo)
        {
            // IMPORTANTE: Actualizamos el tiempo ANTES de disparar para evitar duplicados
            tiempoSiguienteDisparo = Time.time + cadenciaFuego;
            Disparar();
        }
    }

    void Disparar()
    {
        if (balaPrefab == null) return;

        Vector3 puntoObjetivo = jugador.position + Vector3.up * alturaDelTiro;
        Vector3 direccion = (puntoObjetivo - transform.position).normalized;

        // Spawn alejado para seguridad
        Vector3 spawnPos = transform.position + direccion * 1.5f;
        
        // Creamos la bala
        GameObject nuevaBala = Instantiate(balaPrefab, spawnPos, Quaternion.LookRotation(direccion));

        Rigidbody rbBala = nuevaBala.GetComponent<Rigidbody>();
        if (rbBala != null)
        {
            rbBala.isKinematic = false;
            rbBala.useGravity = false;
            rbBala.linearVelocity = direccion * velocidadBala;
        }

        // Destrucción garantizada para que no se acumulen
        Destroy(nuevaBala, 3f);
    }
}