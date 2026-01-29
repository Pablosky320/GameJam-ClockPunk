using UnityEngine;

public class DeteccionDanio : MonoBehaviour
{
    public float puntosDeDanio = 20f;

    void OnTriggerEnter(Collider other)
    {
        // Si chocamos con el enemigo
        if (other.CompareTag("Enemigo"))
        {
            // Buscamos tu script GestorBarras en el caballo
            GestorBarras gestor = other.GetComponent<GestorBarras>();
            
            if (gestor != null)
            {
                gestor.RecibirDanio(puntosDeDanio);
            }

            // Si es una bala, la destruimos al impactar
            if (gameObject.name.Contains("Bala")) Destroy(gameObject);
        }
    }
}