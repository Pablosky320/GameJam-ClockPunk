using UnityEngine;

public class BalaDanio : MonoBehaviour 
{
    public float danio = 10f;

    // Cambiado a Trigger porque es mucho más fiable para balas rápidas
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Usamos el script que me pasaste
            other.GetComponent<SaludJugador>().RecibirDanio(danio);
            Destroy(gameObject); // La bala desaparece al tocarte
        }
        
        // Destruir si toca el suelo para que no se acumulen
        if (other.CompareTag("Suelo")) 
        {
            Destroy(gameObject);
        }
    }
}