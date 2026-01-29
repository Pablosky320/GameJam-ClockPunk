using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class SaludJugador : MonoBehaviour
{
    public float vidaMaxima = 100f;
    public float vidaActual;

    public Slider sliderVida;
    public GameObject escudo; // objeto visual

    private bool escudoActivo = false;
    public float duracionEscudo = 5f;
    private BalaDanio dano;

    void Start()
    {
        vidaActual = vidaMaxima;
        escudo.SetActive(false);

        if (sliderVida != null)
        {
            sliderVida.maxValue = vidaMaxima;
            sliderVida.value = vidaActual;
        }
    }

    // ---------------- DAÑO ----------------
    public void RecibirDanio(float cantidad)
    {
        if (!escudoActivo) // SOLO recibe daño si NO hay escudo
        {
            vidaActual -= cantidad;
            vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);

            if (sliderVida != null)
                sliderVida.value = vidaActual;

            if (vidaActual <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        else
        {
            Debug.Log("🛡️ Daño bloqueado por el escudo");
        }
    }

    // --------------- ESCUDO ---------------
    IEnumerator ActivarEscudoTemporal()
    {
        escudoActivo = true;
        escudo.SetActive(true);

        Debug.Log("Escudo ACTIVADO");

        yield return new WaitForSeconds(duracionEscudo);

        escudoActivo = false;
        escudo.SetActive(false);

        Debug.Log("Escudo DESACTIVADO");
    }

    // --------------- COLECCIONABLE ---------------
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ColeccionableEscudo"))
        {
            other.gameObject.SetActive(false);
            // Reinicia el temporizador si ya estaba activo
            StopAllCoroutines();
            StartCoroutine(ActivarEscudoTemporal());
        }
    }
}
