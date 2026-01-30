using UnityEngine;

public class BalaEnemigoDaño : MonoBehaviour
{
    public float danio = 10f;

    void OnTriggerEnter(Collider other)
    {
        // IMPORTANTE: El objeto del Gato debe tener el Tag "Player"
        if (other.CompareTag("Player"))
        {
            PlayerController jugador = other.GetComponent<PlayerController>();
            if (jugador != null)
            {
                jugador.RecibirDanio(danio);
            }
            Destroy(gameObject);
        }
    }
}