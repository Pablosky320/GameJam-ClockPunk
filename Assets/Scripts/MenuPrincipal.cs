using UnityEngine;
using UnityEngine.SceneManagement; // Imprescindible para cambiar de escena

public class MenuPrincipal : MonoBehaviour
{
    public void EmpezarJuego()
    {
        // Carga la escena 1 (tu nivel de juego)
        SceneManager.LoadScene(1);
    }

    public void SalirDelJuego()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}