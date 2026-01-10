using UnityEngine;
using Microlight.MicroBar; // Esta es la línea que permite usar la barra

public class GestorBarras : MonoBehaviour
{
    [Header("Configuracion de la Barra")]
    public MicroBar barraVida; // Aquí es donde conectarás el objeto
    public float vidaMaxima = 100f;
    private float vidaActual;

    void Start()
    {
        // Al empezar el juego, la vida está al máximo
        vidaActual = vidaMaxima;
        
        // Inicializamos la barra visualmente
        if (barraVida != null) 
        {
            barraVida.Initialize(vidaMaxima);
        }
    }

    // Esta función quita vida y actualiza la barra roja
    public void RecibirDanio(float cantidad)
    {
        vidaActual -= cantidad;
        
        // Actualizamos la barra con el nuevo valor
        if (barraVida != null) 
        {
            barraVida.UpdateBar(vidaActual);
        }

        // Si la vida es 0 o menos, el enemigo se destruye
        if (vidaActual <= 0) 
        {
            Destroy(gameObject);
        }
    }
}