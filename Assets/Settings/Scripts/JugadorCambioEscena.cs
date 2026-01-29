using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class JugadorCambioEscena : MonoBehaviour
{
    public CambioEscena cambioescena;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TeleportEscena"))
        {
            cambioescena.PasarEscena();
        }
    }
}
