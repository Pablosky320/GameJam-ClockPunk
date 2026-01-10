using UnityEngine;

public class BalaDanio : MonoBehaviour 
{
    public float danio = 10f;

    void OnTriggerEnter(Collider other)
    {
        // 1. Comprueba si el objeto tocado es el Player
        if (other.CompareTag("Player"))
        {
            // 2. Buscamos el script SaludJugadorCanvas en el Player
            SaludJugadorCanvas salud = other.GetComponent<SaludJugadorCanvas>();
            
            if (salud != null)
            {
                // 3. Llamamos a la función para restar vida y actualizar el Canvas
                salud.RecibirDanio(danio);
            }
            
            // La bala desaparece al impactar al jugador
            Destroy(gameObject);
        }
        
        // Destruir si toca el suelo o paredes para no saturar el juego
        if (other.CompareTag("Suelo") || other.gameObject.layer == LayerMask.NameToLayer("Obstaculos")) 
        {
            Destroy(gameObject);
        }
    }
}