using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportZone : MonoBehaviour
{
    [Header("Configuración del Nivel")]
    public string nombreDelSiguienteNivel; // Escribe aquí "Nivel2", etc.

    // Esto detecta cuando el Player entra en el área 3D
    private void OnTriggerEnter(Collider other)
    {
        // Importante: Tu personaje debe tener el Tag "Player"
        if (other.CompareTag("Player"))
        {
            Debug.Log("Cargando: " + nombreDelSiguienteNivel);
            SceneManager.LoadScene(nombreDelSiguienteNivel);
        }
    }
}