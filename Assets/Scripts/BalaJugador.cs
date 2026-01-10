using UnityEngine;

public class BalaJugador : MonoBehaviour 
{
    public float danio = 20f; // Cuánta vida quita cada bala

    void OnCollisionEnter(Collision collision)
    {
        // 1. El caballo DEBE tener el Tag "Enemigo" puesto en el Inspector
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            // 2. Buscamos tu script real que es GestorBarras
            GestorBarras salud = collision.gameObject.GetComponent<GestorBarras>();
            
            if (salud != null) 
            {
                salud.RecibirDanio(danio);
            }
        }
        
        // La bala desaparece al chocar
        Destroy(gameObject);
    }
}