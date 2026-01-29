using UnityEngine;

public class ObjetoBotin : MonoBehaviour
{
    public float velocidadGiro = 100f;

    void Update()
    {
        // El objeto gira constantemente
        transform.Rotate(Vector3.up * velocidadGiro * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detecta si el jugador toca el objeto
        if (other.CompareTag("Player"))
        {
            Debug.Log("¡Objeto recogido!");
            Destroy(gameObject); // El objeto desaparece
        }
    }
}