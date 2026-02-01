using UnityEngine;
using TMPro;
using System.Collections;

public class InteraccionCiervo : MonoBehaviour
{
    [Header("Configuración de Diálogo")]
    public string[] frases;
    public float velocidadTexto = 0.05f;

    [Header("Referencias UI")]
    public GameObject panelTexto;
    public TextMeshProUGUI textoDialogo;

    [Header("Configuración de Cámara")]
    public float zoomFOV = 30f; // Campo de visión al acercarse
    private float fovOriginal;
    private Camera camaraPrincipal;
    public float velocidadZoom = 5f;

    private bool jugadorCerca = false;
    private int indiceFrase = 0;
    private bool escribiendo = false;
    private bool hablando = false;

    void Start()
    {
        camaraPrincipal = Camera.main;
        fovOriginal = camaraPrincipal.fieldOfView;

        // ESTO ARREGLA QUE EL TEXTO SALGA AL PRINCIPIO
        if (panelTexto != null) 
            panelTexto.SetActive(false); 
    }

    void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E))
        {
            if (escribiendo)
            {
                // Saltarse la animación y mostrar todo de golpe
                StopAllCoroutines();
                textoDialogo.text = frases[indiceFrase - 1];
                escribiendo = false;
            }
            else
            {
                if (indiceFrase < frases.Length)
                {
                    hablando = true;
                    StartCoroutine(EscribirFrase(frases[indiceFrase]));
                    indiceFrase++;
                }
                else
                {
                    FinalizarDialogo();
                }
            }
        }

        // Manejo del Zoom de la cámara
        float fovObjetivo = hablando ? zoomFOV : fovOriginal;
        camaraPrincipal.fieldOfView = Mathf.Lerp(camaraPrincipal.fieldOfView, fovObjetivo, Time.deltaTime * velocidadZoom);
    }

    IEnumerator EscribirFrase(string frase)
    {
        escribiendo = true;
        panelTexto.SetActive(true);
        textoDialogo.text = "";

        foreach (char letra in frase.ToCharArray())
        {
            textoDialogo.text += letra;
            yield return new WaitForSeconds(velocidadTexto);
        }
        escribiendo = false;
    }

    void FinalizarDialogo()
    {
        panelTexto.SetActive(false);
        indiceFrase = 0;
        hablando = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) jugadorCerca = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
            StopAllCoroutines();
            FinalizarDialogo();
        }
    }
}