using UnityEngine;

public class CamaraSigue : MonoBehaviour
{
    public Transform objetivo; // Tu Player
    public float suavizado = 0.125f;
    private Vector3 desajuste; 

    void Start()
    {
        if (objetivo != null)
        {
            // CALCULA LA DISTANCIA ACTUAL (IMPORTANTE)
            desajuste = transform.position - objetivo.position; 
        }
    }

    void LateUpdate()
    {
        if (objetivo != null)
        {
            // Mantiene esa distancia exacta siempre para que no salga volando
            Vector3 posicionDeseada = objetivo.position + desajuste;
            transform.position = Vector3.Lerp(transform.position, posicionDeseada, suavizado);
        }
    }
}