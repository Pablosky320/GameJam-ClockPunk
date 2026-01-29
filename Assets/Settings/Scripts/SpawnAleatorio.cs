using UnityEngine;

public class SpawnAleatorio : MonoBehaviour
{
    [Header("Configuración del Prefab")]
    public GameObject enemigoPrefab; // Arrastra aquí el prefab del caballo

    [Header("Ajustes de Spawn")]
    public int cantidadInicial = 3;
    public float radioSpawn = 15f; 
    public float tiempoEntreNuevos = 5f;
    public int maxEnemigos = 10; // Límite para que no explote el PC

    void Start()
    {
        // Crea los enemigos iniciales
        for (int i = 0; i < cantidadInicial; i++)
        {
            GenerarEnemigo();
        }

        // Llama a la función repetidamente cada X segundos
        InvokeRepeating("GenerarEnemigo", tiempoEntreNuevos, tiempoEntreNuevos);
    }

    void GenerarEnemigo()
    {
        if (enemigoPrefab == null) return;

        // Comprobamos cuántos enemigos hay en la escena para no pasarnos del límite
        int enemigosActuales = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemigosActuales >= maxEnemigos) return;

        // Calculamos punto aleatorio en el círculo
        Vector2 circulo = Random.insideUnitCircle * radioSpawn;
        
        // CORRECCIÓN: Ahora usa la 'y' del objeto Empty para que no salgan en el suelo
        Vector3 posFinal = new Vector3(
            transform.position.x + circulo.x, 
            transform.position.y, 
            transform.position.z + circulo.y
        );

        Instantiate(enemigoPrefab, posFinal, Quaternion.identity);
    }

    // Dibuja el círculo azul en la ventana Scene
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radioSpawn);
    }
}