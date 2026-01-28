using UnityEngine;

public class BalaEnemigoDaño : MonoBehaviour
{
    public float danio = 10f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SaludJugadorCanvas salud = other.GetComponent<SaludJugadorCanvas>();
            if (salud != null)
            {
                salud.RecibirDanio(danio);
            }
            Destroy(gameObject);
        }
    }
}