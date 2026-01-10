using UnityEngine;

public class CamaraSigue : MonoBehaviour
{
    public Transform jugador; // Arrastra aquí a tu Player
    private Vector3 offset;    // La distancia que ya tienes configurada

    void Start()
    {
        // Esto guarda la distancia actual para que no se mueva el ángulo
        offset = transform.position - jugador.position;
    }

    void LateUpdate()
    {
        // La cámara sigue al jugador manteniendo la distancia inicial
        transform.position = jugador.position + offset;
    }
}