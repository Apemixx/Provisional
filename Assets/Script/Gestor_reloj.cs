using UnityEngine;
using TMPro; // Necesario para controlar textos modernos

public class GestorReloj : MonoBehaviour
{
    // 'static' guarda el valor en la memoria global. 
    // Así la hora no se resetea al cambiar a la escena del minijuego y volver.
    // 480 minutos = 8:00 AM
    public static float tiempoEnMinutos = 480f; 
    
    // Cuántos minutos del juego pasan por cada segundo de la vida real
    public float velocidadReloj = 2f; 

    public TMP_Text textoHora;

    void Update()
    {
        // Avanzamos el reloj de forma constante
        tiempoEnMinutos += velocidadReloj * Time.deltaTime;

        // Evitar que pase de las 24:00 (1440 minutos en un día)
        if (tiempoEnMinutos >= 1440f) 
        {
            tiempoEnMinutos = 0f; // Reinicia a las 00:00 si llegan a la medianoche
        }

        // Convertir los minutos totales a formato de horas y minutos
        int horas = Mathf.FloorToInt(tiempoEnMinutos / 60f);
        int minutos = Mathf.FloorToInt(tiempoEnMinutos % 60f);

        // Actualizar el texto en pantalla asegurando que siempre tenga dos dígitos (ej: 08:05)
        if (textoHora != null)
        {
            textoHora.text = string.Format("{0:00}:{1:00}", horas, minutos);
        }
    }
}