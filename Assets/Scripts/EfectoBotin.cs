using UnityEngine;

public class EfectoBotin : MonoBehaviour
{
    public float velocidadGiro = 100f;
    public float amplitudFlote = 0.5f;
    public float velocidadFlote = 2f;

    private Vector3 posicionInicial;

    void Start()
    {
        posicionInicial = transform.position;
    }

    void Update()
    {
        // Hacer que gire
        transform.Rotate(Vector3.up * velocidadGiro * Time.deltaTime);

        // Hacer que flote arriba y abajo
        float nuevoY = posicionInicial.y + Mathf.Sin(Time.time * velocidadFlote) * amplitudFlote;
        transform.position = new Vector3(transform.position.x, nuevoY, transform.position.z);
    }
}