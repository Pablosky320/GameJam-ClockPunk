using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking.PlayerConnection;

public class SaludJugador1 : MonoBehaviour
{
    public float vidaMaxima = 100f;
    public float vidaActual;
    // Borramos la referencia a BalaJugador porque causaba errores
    public Slider sliderVida;
    public GameObject escudo;
    public BalaDanio balaDanio;
    public GameObject bala;

    public bool escudoActivo = false;
    public float duracionEscudo = 5f;
    public float balas = 0f;
    void Start()
    {
        vidaActual = vidaMaxima;
        escudo.SetActive(false);
        balas = 0f;


        if (sliderVida != null)
        {
            sliderVida.maxValue = vidaMaxima;
            sliderVida.value = vidaActual;
        }
        escudoActivo = GetComponent<BalaDanio>();
    }
    private void Update()
    {
        if (balas >= 6f)
        {
            escudoActivo = false;
            escudo.SetActive(false);
            balas = 0f;
        }
    }

    public void RecibirDanio(float cantidad)
    {
        
        // SI EL ESCUDO ESTÁ ACTIVO, SALIMOS DE LA FUNCIÓN SIN HACER NADA
        if (escudoActivo)
        {
            Debug.Log("🛡️ ¡Ataque bloqueado!");
            balaDanio.danio = 0f;
            Destroy(bala);



        }

        // SI NO HAY ESCUDO, SE RESTA VIDA
        vidaActual -= cantidad;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);

        if (sliderVida != null)
            sliderVida.value = vidaActual;

        if (vidaActual <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ColeccionableEscudo"))
        {
            //other.gameObject.SetActive(false);
            escudoActivo = true;
            escudo.SetActive(true);
            Debug.Log("Escudo activado");
        }
        if (other.gameObject.CompareTag("Bala"))
        {
            if (escudoActivo)
            {
                balas = balas + 1;
                Debug.Log("me ha dado " + balas + " bala");

            }
        }
    }
}