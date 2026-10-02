using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float mouseSensitivity = 0.5f;
    public Transform playerCamera; // Referencia a la cámara

    private CharacterController controller;
    private Animator animator;
    private InputAction moveAction;
    private InputAction lookAction;
    
    private float xRotation = 0f; // Controla la rotación vertical

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        // Oculta y bloquea el cursor del ratón en el centro de la pantalla
        Cursor.lockState = CursorLockMode.Locked;

        // Configuración de movimiento WASD
        moveAction = new InputAction("Move");
        moveAction.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");
        moveAction.Enable();

        // Configuración de vista con el ratón
        lookAction = new InputAction("Look", binding: "<Mouse>/delta");
        lookAction.Enable();
    }

    void OnDisable()
    {
        moveAction.Disable();
        lookAction.Disable();
    }

    void Update()
    {
        // 1. ROTACIÓN CON RATÓN
        Vector2 lookInput = lookAction.ReadValue<Vector2>();
        float mouseX = lookInput.x * mouseSensitivity;
        float mouseY = lookInput.y * mouseSensitivity;

        // Calculamos la rotación vertical y la limitamos entre -90 y 90 grados
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Aplicamos la rotación vertical a la cámara
        if (playerCamera != null)
        {
            playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }

        // Aplicamos la rotación horizontal al personaje completo
        transform.Rotate(Vector3.up * mouseX);

        // 2. MOVIMIENTO WASD
        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = transform.right * input.x + transform.forward * input.y;
        controller.SimpleMove(move * speed);

        // 3. ANIMACIÓN
        if (animator != null)
        {
            animator.SetFloat("Speed", input.magnitude);
        }
    }
}