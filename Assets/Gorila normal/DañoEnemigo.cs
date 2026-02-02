using UnityEngine;

public class DañoEnemigo : MonoBehaviour
{
    public float cantidadDaño = 20f;
    private Collider miCollider;

    void Start()
    {
        miCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // El gato tiene que tener el Tag "Player"
        if (other.CompareTag("Player"))
        {
            // Buscamos tu script real del gato
            PlayerController player = other.GetComponent<PlayerController>();
            
            if (player != null)
            {
               // Llamamos a la función exacta de tu script (con 'n')
               player.RecibirDanio(cantidadDaño);
               Debug.Log("¡GOLPE! Vida restada al gato.");
               
               // Desactivamos el collider un momento para que no te quite vida 80 veces por segundo
               StartCoroutine(DesactivarTemporalmente());
            }
        }
    }

    System.Collections.IEnumerator DesactivarTemporalmente()
    {
        miCollider.enabled = false;
        yield return new WaitForSeconds(1f); // Espera 1 segundo para poder volver a pegar
        miCollider.enabled = true;
    }
}