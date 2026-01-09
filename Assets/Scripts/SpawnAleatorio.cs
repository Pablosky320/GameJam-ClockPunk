using UnityEngine;

public class SpawnAleatorio : MonoBehaviour
{
    [Header("Configuración")]
    public GameObject enemigoPrefab; // DEBE SER EL PREFAB (el de la carpeta Assets)
    public int cantidadInicial = 3;
    public float radioSpawn = 15f; 
    public float tiempoEntreNuevos = 5f;

    void Start()
    {
        // Crea los enemigos al empezar
        for (int i = 0; i < cantidadInicial; i++)
        {
            GenerarEnemigo();
        }

        // Sigue creando enemigos
        InvokeRepeating("GenerarEnemigo", tiempoEntreNuevos, tiempoEntreNuevos);
    }

    void GenerarEnemigo()
    {
        if (enemigoPrefab == null) return;

        // Punto aleatorio
        Vector2 circulo = Random.insideUnitCircle * radioSpawn;
        Vector3 posFinal = new Vector3(transform.position.x + circulo.x, 1f, transform.position.z + circulo.y);

        Instantiate(enemigoPrefab, posFinal, Quaternion.identity);
    }

    // Esto dibuja el área en la ventana Scene para que la veas
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radioSpawn);
    }
}