using UnityEngine;
using UnityEngine.UI; // Necesario para la barra de vida

public class VidaEnemigo : MonoBehaviour
{
    public float vidaMaxima = 100f;
    private float vidaActual;
    public Slider barraDeVida; // Arrastra el Slider aquí en el Inspector

    void Start()
    {
        vidaActual = vidaMaxima;
        if (barraDeVida != null)
        {
            barraDeVida.maxValue = vidaMaxima;
            barraDeVida.value = vidaActual;
        }
    }

    public void RecibirDanio(float cantidad)
    {
        vidaActual -= cantidad;
        
        if (barraDeVida != null)
            barraDeVida.value = vidaActual;

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        // Aquí puedes poner efectos de sonido o partículas
        Destroy(gameObject);
    }
}