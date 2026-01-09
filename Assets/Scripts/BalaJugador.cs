using UnityEngine;

public class BalaJugador : MonoBehaviour 
{
    public float danio = 10f; // El daño que le hará a la esfera

    void OnCollisionEnter(Collision collision)
    {
        // Importante: Comprueba si lo que golpeamos tiene el Tag "Enemigo"
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            // Buscamos el script de salud en la esfera roja
            SaludEnemigo salud = collision.gameObject.GetComponent<SaludEnemigo>();
            if (salud != null) 
            {
                salud.RecibirDanio(danio);
            }
        }
        
        // La bala se destruye al tocar cualquier cosa (suelo, paredes o enemigo)
        Destroy(gameObject);
    }
}