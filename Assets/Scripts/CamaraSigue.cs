using UnityEngine;

public class CamaraSigue : MonoBehaviour
{
    public Transform objetivo; 
    public float suavizado = 0.125f;
    private Vector3 desajuste; 

    void Start()
    {
        if (objetivo != null)
        {
            // ESTO ES LO IMPORTANTE:
            // Guarda la distancia que tú has puesto a mano en el editor
            desajuste = transform.position - objetivo.position; 
        }
    }

    void LateUpdate()
    {
        if (objetivo != null)
        {
            // Se mueve manteniendo esa distancia que guardó al principio
            Vector3 posicionDeseada = objetivo.position + desajuste;
            transform.position = Vector3.Lerp(transform.position, posicionDeseada, suavizado);
        }
    }
}