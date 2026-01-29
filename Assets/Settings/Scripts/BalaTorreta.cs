using UnityEngine;

public class BalaTorreta : MonoBehaviour
{
    public float danioBala = 15f; // Cantidad de vida que quita cada bala

    private void OnCollisionEnter(Collision collision)
    {
        // Comprobamos si chocamos con el Player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Buscamos el script de SaludJugador en el objeto que tocamos
            SaludJugador salud = collision.gameObject.GetComponent<SaludJugador>();

            if (salud != null)
            {
                salud.RecibirDanio(danioBala);
            }
        }

        // La bala siempre se destruye al chocar contra algo
        Destroy(gameObject);
    }
}