using UnityEngine;

public class Botin : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Aquí puedes sumar puntos o vida
            Debug.Log("Objeto recogido");
            Destroy(gameObject);
        }
    }
}