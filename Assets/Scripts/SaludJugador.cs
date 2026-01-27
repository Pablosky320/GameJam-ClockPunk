using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class SaludJugador : MonoBehaviour
{
    public float vidaMaxima = 100f;
    public float vidaActual;
    public Slider sliderVida;
    public GameObject escudo;

    void Start()
    {
        // Reiniciamos contador al empezar
        SaludEnemigo.contadorMuertesGlobal = 0;
        escudo.gameObject.SetActive(false);
        vidaActual = vidaMaxima;
        if (sliderVida != null) 
        {
            sliderVida.maxValue = vidaMaxima;
            sliderVida.value = vidaActual;
        }
    }

    public void RecibirDanio(float cantidad)
    {
        if (escudo== false) {
            {
                vidaActual -= cantidad;
                vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);

                if (sliderVida != null) sliderVida.value = vidaActual;

                if (vidaActual <= 0)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        if (escudo == true)
            {
                Debug.Log("Daño bloqueado por el escudo");
            }
        }
    }
    //si tocamos el coleccionable, tendremos el escudo activado
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ColeccionableEscudo"))
        {
            escudo.gameObject.SetActive(true);
        }
        else
        {
            escudo.gameObject.SetActive(false);
        }
    }
}