using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections; // Necesario para la secuencia estilo WarioWare

public class ControlProfesor : MonoBehaviour
{
    [Header("Interfaz y Flujo")]
    public GameObject panelInstrucciones;
    public GameObject textoWario;
    public Slider barraPaciencia;

    [Header("Configuración del Juego")]
    public float atencionActual = 100f;
    public float velocidadSueno = 15f; // Cuánto baja la atención por segundo
    public GameObject circuloPrefab;
    public float tiempoAparicion = 1f;

    private bool juegoActivo = false;

    void Start()
    {
        // Liberar el ratón
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Configurar barra inicial
        barraPaciencia.maxValue = 100f;
        barraPaciencia.value = atencionActual;

        // Mostrar instrucciones y ocultar el grito de Wario y la barra al inicio
        panelInstrucciones.SetActive(true);
        textoWario.SetActive(false);
        barraPaciencia.gameObject.SetActive(false);
    }

    // Esta función se llamará al pulsar el botón "Aceptar"
    public void BotonAceptar()
    {
        panelInstrucciones.SetActive(false);
        StartCoroutine(SecuenciaInicioWario());
    }

    IEnumerator SecuenciaInicioWario()
    {
        // 1. Mostrar "¡NO TE DUERMAS!" estilo WarioWare
        textoWario.SetActive(true);
        
        // Esperar 1.5 segundos
        yield return new WaitForSeconds(1.5f);
        
        // 2. Ocultar el texto, mostrar la barra y arrancar el minijuego
        textoWario.SetActive(false);
        barraPaciencia.gameObject.SetActive(true);
        juegoActivo = true;

        InvokeRepeating("GenerarCirculo", 0.5f, tiempoAparicion);
    }

    void Update()
    {
        // Si aún estamos leyendo las instrucciones, la barra no baja
        if (!juegoActivo) return;

        atencionActual -= velocidadSueno * Time.deltaTime;
        barraPaciencia.value = atencionActual;

        if (atencionActual <= 0)
        {
            Debug.Log("¡Te has dormido en clase! Volviendo al campus...");
            CancelInvoke("GenerarCirculo");
            SceneManager.LoadScene("Campus_uni");
        }
    }

    void GenerarCirculo()
    {
        float xAleatorio = Random.Range(-7f, 7f);
        float yAleatorio = Random.Range(-4f, 4f);
        Vector3 posicion = new Vector3(xAleatorio, yAleatorio, 0);

        Instantiate(circuloPrefab, posicion, Quaternion.identity);
    }

    public void CalmarProfesor(float cantidad)
    {
        atencionActual += cantidad;
        if (atencionActual > 100f) atencionActual = 100f;
    }
}