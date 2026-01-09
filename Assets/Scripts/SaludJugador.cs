using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class SaludJugador : MonoBehaviour
{
    public float vidaMaxima = 100f;
    public float vidaActual;
    public Slider sliderVida; 

    void Start()
    {
        // Reiniciamos contador al empezar
        SaludEnemigo.contadorMuertesGlobal = 0; 

        vidaActual = vidaMaxima;
        if (sliderVida != null) 
        {
            sliderVida.maxValue = vidaMaxima;
            sliderVida.value = vidaActual;
        }
    }

    public void RecibirDanio(float cantidad)
    {
        vidaActual -= cantidad;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);
        
        if (sliderVida != null) sliderVida.value = vidaActual;

        if (vidaActual <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}