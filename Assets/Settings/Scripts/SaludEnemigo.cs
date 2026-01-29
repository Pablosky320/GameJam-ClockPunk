using UnityEngine;
using UnityEngine.UI;

public class SaludEnemigo : MonoBehaviour
{
    [Header("Ajustes de Vida")]
    public float vidaMaxima = 30f;
    public float vidaActual;

    [Header("Interfaz (UI)")]
    public Slider barraVida;

    [Header("Botín y Puerta")]
    public GameObject objetoDrop; 
    public static int contadorMuertesGlobal = 0; 

    void Start()
    {
        vidaActual = vidaMaxima;
        if (barraVida != null)
        {
            barraVida.maxValue = vidaMaxima;
            barraVida.value = vidaActual;
        }
    }

    public void RecibirDanio(float cantidad)
    {
        vidaActual -= cantidad;
        if (barraVida != null) barraVida.value = vidaActual;

        if (vidaActual <= 0)
        {
            Muerte();
        }
    }

    void Muerte()
    {
        if (objetoDrop != null)
        {
            // CORRECCIÓN: Spawneamos el objeto 2 unidades arriba para la nueva escala
            Vector3 posicionSpawn = transform.position + new Vector3(0, 2f, 0);
            GameObject clon = Instantiate(objetoDrop, posicionSpawn, Quaternion.identity);
            
            // ESCALADO DINÁMICO: Forzamos al engranaje a ser grande (escala 15)
            clon.transform.localScale = new Vector3(15f, 15f, 15f);
        }

        contadorMuertesGlobal++;
        Destroy(gameObject);
    }
}