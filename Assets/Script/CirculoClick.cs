using UnityEngine;
using UnityEngine.InputSystem;

public class CirculoClick : MonoBehaviour
{
    public float puntosRecuperados = 10f;
    public float tiempoDeVida = 2f; 
    private ControlProfesor gestor;

    void Start()
    {
        gestor = FindFirstObjectByType<ControlProfesor>();
        Destroy(gameObject, tiempoDeVida);
    }

    void Update()
    {
        // Comprueba si hemos hecho clic izquierdo en este fotograma exacto
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            // 1. Obtenemos la posición en píxeles del ratón en la pantalla
            Vector2 posicionPantalla = Mouse.current.position.ReadValue();
            
            // 2. Convertimos esa posición de la pantalla al mundo 2D de nuestro juego
            Vector2 posicionMundo = Camera.main.ScreenToWorldPoint(posicionPantalla);
            
            // 3. Comprobamos si en ese punto exacto hay un Collider 2D
            Collider2D hit = Physics2D.OverlapPoint(posicionMundo);
            
            // Si hemos tocado algo, y ese algo es este mismo círculo:
            if (hit != null && hit.gameObject == gameObject)
            {
                if (gestor != null)
                {
                    gestor.CalmarProfesor(puntosRecuperados);
                }
                
                Destroy(gameObject);
            }
        }
    }
}