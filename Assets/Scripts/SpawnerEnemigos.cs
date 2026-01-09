using UnityEngine;

public class SpawnerEnemigos : MonoBehaviour
{
    [Header("Configuración")]
    public GameObject prefabEnemigo; // Arrastra aquí tu esfera roja
    public float tiempoEntreEnemigos = 3f;
    public int limiteEnemigos = 10;
    public float radioSpawn = 10f; // <--- ESTA ES LA NUEVA VARIABLE

    private int enemigosActuales = 0;

    void Start()
    {
        // Corrutina o Invoke para ir soltándolos
        InvokeRepeating("GenerarEnemigo", 0.5f, tiempoEntreEnemigos);
    }

    void GenerarEnemigo()
    {
        if (enemigosActuales >= limiteEnemigos || prefabEnemigo == null) return;

        // CALCULAMOS POSICIÓN RANDOM
        Vector2 circulo = Random.insideUnitCircle * radioSpawn;
        Vector3 posFinal = new Vector3(transform.position.x + circulo.x, 1f, transform.position.z + circulo.y);

        // INSTANCIAR
        Instantiate(prefabEnemigo, posFinal, Quaternion.identity);
        enemigosActuales++;
    }

    // Dibuja el círculo en la Scene para que veas el área
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radioSpawn);
    }
}