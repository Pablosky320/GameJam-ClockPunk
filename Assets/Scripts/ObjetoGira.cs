using UnityEngine;

public class ObjetoGira : MonoBehaviour
{
    public float velocidadGiro = 100f;

    void Update()
    {
        // Esto hace que el objeto rote solo
        transform.Rotate(Vector3.up * velocidadGiro * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Si el jugador lo toca, desaparece
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}