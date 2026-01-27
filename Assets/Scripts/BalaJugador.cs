using UnityEngine;

public class BalaJugador : MonoBehaviour 
{
    public float danio = 20f; 

    void OnTriggerEnter(Collider other)
    {
        // 1. IMPORTANTE: El enemigo DEBE tener el Tag "Enemigo"
        if (other.CompareTag("Enemigo"))
        {
            // 2. Buscamos tu script de salud
            GestorBarras salud = other.GetComponent<GestorBarras>();
            
            if (salud != null) 
            {
                salud.RecibirDanio(danio);
                Debug.Log("¡Le diste! Vida restada.");
            }
            Destroy(gameObject); 
        }
    }
}