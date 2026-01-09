using UnityEngine;

public class BotonMecanico : MonoBehaviour, IInteractuable
{
    public void Interactuar()
    {
        Debug.Log("¡Mecanismo activado! (Aquí puedes abrir una puerta)");
        // Cambiamos el color a verde para saber que funciona
        GetComponent<Renderer>().material.color = Color.green;
    }
}