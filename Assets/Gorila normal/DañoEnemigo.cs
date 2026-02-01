using UnityEngine;

public class DañoEnemigo : MonoBehaviour
{
    public float cantidadDaño = 20f;

    private void OnTriggerEnter(Collider other)
    {
        // Solo hacemos daño si el objeto tiene el Tag "Player"
        if (other.CompareTag("Player"))
        {
            // Buscamos un script de Vida en el gato (ajusta el nombre si el tuyo es diferente)
            // VidaJugador scriptVida = other.GetComponent<VidaJugador>();
            
            // if (scriptVida != null)
            // {
            //    scriptVida.RecibirDaño(cantidadDaño);
            //    Debug.Log("¡El gorila ha golpeado al gato!");
            // }
            
            Debug.Log("Impacto con el jugador detectado");
        }
    }
}