using UnityEngine;
using Microlight.MicroBar;
using UnityEngine.SceneManagement;
using System.Collections;

public class SaludJugadorCanvas : MonoBehaviour
{
    public MicroBar barraVidaUI; 
    public float vidaMaxima = 100f;
    private float vidaActual;
    private bool estaMuerto = false;

    void Start()
    {
        vidaActual = vidaMaxima;
        if (barraVidaUI != null) barraVidaUI.Initialize(vidaMaxima);
    }

    public void RecibirDanio(float cantidad)
    {
        if (estaMuerto) return; // Si ya está muerto, no recibe más daño

        vidaActual -= cantidad;
        if (barraVidaUI != null) barraVidaUI.UpdateBar(vidaActual);

        if (vidaActual <= 0) 
        {
            StartCoroutine(SecuenciaMuerte());
        }
    }

    IEnumerator SecuenciaMuerte()
    {
        estaMuerto = true;
        
        // 1. Activar animación de muerte
        Animator anim = GetComponent<Animator>();
        if (anim != null) anim.SetTrigger("Muerte");

        // 2. Desactivar el control del jugador (Dash y Movimiento)
        PlayerController controller = GetComponent<PlayerController>();
        if (controller != null) controller.enabled = false;

        // 3. Esperar 2 segundos para que se vea al gato morir
        yield return new WaitForSeconds(2f);

        // 4. Reiniciar el nivel
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}