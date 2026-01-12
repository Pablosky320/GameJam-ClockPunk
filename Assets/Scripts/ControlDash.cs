using UnityEngine;
using Microlight.MicroBar; // Necesario para la barra animada

public class ControlDash : MonoBehaviour
{
    public MicroBar barraDash; 
    public float energiaMaxima = 100f;
    private float energiaActual;

    [Header("Ajustes de Dash")]
    public float costeDash = 33f; // Gasta un tercio de barra cada vez
    public float regeneracion = 15f; // Cuánta energía recupera por segundo

    void Start()
    {
        energiaActual = energiaMaxima;
        if (barraDash != null) barraDash.Initialize(energiaMaxima);
    }

    void Update()
    {
        // 1. Regenerar energía poco a poco si no está llena
        if (energiaActual < energiaMaxima)
        {
            energiaActual += regeneracion * Time.deltaTime;
            if (barraDash != null) barraDash.UpdateBar(energiaActual);
        }

        // 2. Detectar si el jugador pulsa la tecla de Dash (ejemplo: Shift)
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            IntentarDash();
        }
    }

    void IntentarDash()
    {
        // Solo hacemos el dash si tenemos energía suficiente
        if (energiaActual >= costeDash)
        {
            energiaActual -= costeDash;
            if (barraDash != null) barraDash.UpdateBar(energiaActual);
            
            EjecutarMovimientoDash();
        }
        else
        {
            Debug.Log("¡Sin energía para el Dash!");
        }
    }

    void EjecutarMovimientoDash()
    {
        // AQUÍ pones tu lógica de movimiento rápido (ej: empujón con Rigidbody)
        Debug.Log("DASH EJECUTADO");
    }
}