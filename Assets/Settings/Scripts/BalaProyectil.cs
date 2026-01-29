using UnityEngine;

public class BalaProyectil : MonoBehaviour
{
    public float danio = 10f;

    private void OnCollisionEnter(Collision collision)
    {
        // Si la bala toca al Player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Buscamos el script de salud que creamos antes
            SaludJugador salud = collision.gameObject.GetComponent<SaludJugador>();
            if (salud != null)
            {
                salud.RecibirDanio(danio);
            }
        }
        
        // La bala se destruye al chocar con cualquier cosa (suelo, paredes o jugador)
        Destroy(gameObject);
    }
}