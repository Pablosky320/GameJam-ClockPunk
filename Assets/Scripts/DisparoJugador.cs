using UnityEngine;
using TMPro;

public class DisparoJugador : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject balaPrefab;
    public Transform puntaPistola;
    public TextMeshProUGUI textoUI; 

    [Header("Configuración")]
    public float velocidadBala = 30f;
    public int balasMaximas = 6;
    public int balasActuales;
    public float tiempoRecarga = 1.5f;
    
    private float timer;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        balasActuales = balasMaximas;
        ActualizarUI();
    }

    void Update()
    {
        MirarAlRaton();

        // Disparar
        if (Input.GetButtonDown("Fire1") && balasActuales > 0)
        {
            Disparar();
        }

        // Recarga pasiva automática
        if (balasActuales < balasMaximas)
        {
            timer += Time.deltaTime;
            if (timer >= tiempoRecarga)
            {
                balasActuales++;
                timer = 0;
                ActualizarUI();
            }
        }
    }

    void Disparar()
    {
        balasActuales--;
        timer = 0;
        ActualizarUI();

        if (anim != null) anim.SetTrigger("Disparar");

        GameObject b = Instantiate(balaPrefab, puntaPistola.position, transform.rotation);
        Rigidbody rb = b.GetComponent<Rigidbody>();
        if (rb != null) rb.linearVelocity = transform.forward * velocidadBala;
        Destroy(b, 2f);
    }

    void ActualizarUI()
    {
        if (textoUI != null) textoUI.text = "Balas: " + balasActuales + "/" + balasMaximas;
    }

    void MirarAlRaton()
    {
        Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(rayo, out RaycastHit golpe))
        {
            Vector3 p = golpe.point;
            p.y = transform.position.y;
            transform.LookAt(p);
        }
    }
}