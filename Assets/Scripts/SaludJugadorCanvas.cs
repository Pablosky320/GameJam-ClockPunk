using UnityEngine;
using Microlight.MicroBar;

public class SaludJugadorCanvas : MonoBehaviour
{
    public MicroBar barraUI; 
    public float vidaMaxima = 100f;
    private float vidaActual;

    [Header("Configuración Respawn")]
    public Vector3 puntoRespawn; // La posición inicial del jugador

    void Start()
    {
        vidaActual = vidaMaxima;
        // Guardamos la posición donde empieza el juego como punto de respawn
        puntoRespawn = transform.position;
        
        if (barraUI != null) barraUI.Initialize(vidaMaxima);
    }

    public void RecibirDanio(float cantidad)
    {
        vidaActual -= cantidad;
        if (barraUI != null) barraUI.UpdateBar(vidaActual);

        if (vidaActual <= 0) 
        {
            Respawn();
        }
    }

    void Respawn()
    {
        Debug.Log("RESPAWN: El jugador ha vuelto al inicio");
        
        // 1. Resetear la vida al máximo
        vidaActual = vidaMaxima;
        
        // 2. Actualizar la barra del Canvas para que vuelva a estar llena
        if (barraUI != null) barraUI.UpdateBar(vidaMaxima);

        // 3. Mover al jugador al punto de inicio
        transform.position = puntoRespawn;

        // Opcional: Si usas físicas (Rigidbody), es bueno resetear la velocidad
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null) rb.linearVelocity = Vector3.zero;
    }
}