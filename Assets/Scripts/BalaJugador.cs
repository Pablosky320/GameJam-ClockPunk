using UnityEngine;

public class BalaJugador : MonoBehaviour 
{
    public float danio = 25f; // Quitará 25 de vida

    // Cambiamos a OnTriggerEnter para que la bala no "rebote"
    private void OnTriggerEnter(Collider other)
    {
        // 1. El Gorila debe tener el Tag "Enemigo"
        if (other.CompareTag("Enemigo"))
        {
            // 2. Buscamos el script de vida del Gorila
            VidaGorila salud = other.GetComponent<VidaGorila>();
            
            if (salud != null) 
            {
                salud.RecibirDanioGorila(danio);
                Debug.Log("¡Impacto en Gorila!");
            }
            
            // 3. La bala desaparece al tocar al enemigo
            Destroy(gameObject);
        }
    }
}