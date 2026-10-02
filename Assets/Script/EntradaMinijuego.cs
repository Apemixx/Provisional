using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escenas

public class EntradaMinijuego : MonoBehaviour
{
    public string nombreEscenaDestino = "Juego_OSU";

    // Esta función se activa automáticamente cuando algo entra en el Trigger
    void OnTriggerEnter(Collider other)
    {
        // Comprobamos si el que ha entrado es el jugador
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(nombreEscenaDestino);
        }
    }
}